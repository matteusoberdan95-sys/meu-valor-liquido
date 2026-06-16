(() => {
  const STORAGE_KEY = "mvl-local-panel-v1";
  const MAX_ITEMS = 25;

  const readStore = () => {
    try {
      const raw = window.localStorage.getItem(STORAGE_KEY);
      if (!raw) {
        return { version: 1, items: [] };
      }

      const parsed = JSON.parse(raw);
      if (!parsed || !Array.isArray(parsed.items)) {
        return { version: 1, items: [] };
      }

      return parsed;
    } catch {
      return { version: 1, items: [] };
    }
  };

  const writeStore = (store) => {
    try {
      window.localStorage.setItem(STORAGE_KEY, JSON.stringify(store));
      return true;
    } catch {
      return false;
    }
  };

  const normalizeShareUrl = (shareUrl) => {
    if (!shareUrl) {
      return "";
    }

    try {
      const url = new URL(shareUrl, window.location.origin);
      return `${url.pathname}${url.search}`;
    } catch {
      return shareUrl;
    }
  };

  const formatSavedAt = (iso) => {
    try {
      return new Intl.DateTimeFormat("pt-BR", {
        dateStyle: "short",
        timeStyle: "short",
      }).format(new Date(iso));
    } catch {
      return "";
    }
  };

  const createId = () => {
    if (window.crypto?.randomUUID) {
      return window.crypto.randomUUID();
    }

    return `mvl-${Date.now()}-${Math.random().toString(16).slice(2)}`;
  };

  const getItems = () => readStore().items;

  const saveItem = (entry) => {
    const sharePath = normalizeShareUrl(entry.shareUrl);
    if (!sharePath) {
      return { ok: false, reason: "missing-url" };
    }

    const store = readStore();
    const now = new Date().toISOString();
    const existingIndex = store.items.findIndex(
      (item) => normalizeShareUrl(item.shareUrl) === sharePath,
    );

    const payload = {
      id: existingIndex >= 0 ? store.items[existingIndex].id : createId(),
      slug: entry.slug,
      calculatorName: entry.calculatorName,
      summary: entry.summary,
      netAmount: entry.netAmount,
      shareUrl: sharePath,
      savedAt: now,
    };

    if (existingIndex >= 0) {
      store.items[existingIndex] = payload;
    } else {
      store.items.unshift(payload);
    }

    store.items = store.items
      .sort((a, b) => new Date(b.savedAt) - new Date(a.savedAt))
      .slice(0, MAX_ITEMS);

    if (!writeStore(store)) {
      return { ok: false, reason: "storage-full" };
    }

    return { ok: true, item: payload, total: store.items.length };
  };

  const removeItem = (id) => {
    const store = readStore();
    store.items = store.items.filter((item) => item.id !== id);
    writeStore(store);
    return store.items.length;
  };

  const clearAll = () => {
    writeStore({ version: 1, items: [] });
  };

  const updateBadges = () => {
    const count = getItems().length;
    document.querySelectorAll("[data-local-panel-count]").forEach((badge) => {
      badge.textContent = String(count);
      badge.hidden = count === 0;
    });
  };

  const showFeedback = (root, message) => {
    const feedback = root?.querySelector("[data-share-feedback]");
    if (!feedback) {
      return;
    }

    feedback.textContent = message;
    feedback.hidden = false;
    window.setTimeout(() => {
      feedback.hidden = true;
    }, 3000);
  };

  const bindSaveButtons = () => {
    document.querySelectorAll("[data-local-panel-save]").forEach((button) => {
      if (button.dataset.localPanelBound === "true") {
        return;
      }

      button.dataset.localPanelBound = "true";
      button.addEventListener("click", () => {
        const root = button.closest("[data-share-root], .valora-share-actions");
        const result = saveItem({
          slug: button.dataset.slug ?? "",
          calculatorName: button.dataset.calculatorName ?? "Calculadora",
          summary: button.dataset.summary ?? "",
          netAmount: button.dataset.netAmount ?? "",
          shareUrl: button.dataset.shareUrl ?? "",
        });

        if (!result.ok) {
          showFeedback(
            root,
            result.reason === "storage-full"
              ? "Não foi possível salvar. Verifique o espaço do navegador."
              : "Não foi possível salvar esta simulação.",
          );
          return;
        }

        updateBadges();
        showFeedback(root, "Salvo no seu painel local.");
        window.MvlMetrics?.collect("panel_save", button.dataset.slug ?? null);
      });
    });
  };

  const renderPanelPage = () => {
    const page = document.querySelector("[data-local-panel-page]");
    if (!page) {
      return;
    }

    const list = page.querySelector("[data-local-panel-list]");
    const empty = page.querySelector("[data-local-panel-empty]");
    const clearButton = page.querySelector("[data-local-panel-clear]");
    const countLabel = page.querySelector("[data-local-panel-count-label]");
    const items = getItems();

    if (countLabel) {
      countLabel.textContent =
        items.length === 0
          ? "Nenhuma simulação salva neste navegador."
          : items.length === 1
            ? "1 simulação salva."
            : `${items.length} simulações salvas.`;
    }

    if (!list || !empty) {
      return;
    }

    list.innerHTML = "";
    if (items.length === 0) {
      list.hidden = true;
      empty.hidden = false;
      if (clearButton) {
        clearButton.hidden = true;
      }
      return;
    }

    empty.hidden = true;
    list.hidden = false;
    if (clearButton) {
      clearButton.hidden = false;
      clearButton.onclick = () => {
        if (window.confirm("Remover todas as simulações salvas neste navegador?")) {
          clearAll();
          updateBadges();
          renderPanelPage();
        }
      };
    }

    items.forEach((item) => {
      const li = document.createElement("li");
      li.className = "valora-local-panel-item";
      li.innerHTML = `
        <div class="valora-local-panel-item-main">
          <span class="valora-badge valora-badge-trabalhista">${item.calculatorName}</span>
          <strong class="valora-local-panel-item-title">${item.summary}</strong>
          <span class="valora-local-panel-item-net">${item.netAmount}</span>
          <span class="valora-text-muted valora-text-sm">${formatSavedAt(item.savedAt)}</span>
        </div>
        <div class="valora-local-panel-item-actions">
          <a class="valora-btn valora-btn-primary valora-btn-sm" href="${item.shareUrl}">Reabrir</a>
          <button type="button" class="valora-btn valora-btn-outline valora-btn-sm" data-remove-id="${item.id}">Remover</button>
        </div>
      `;

      li.querySelector("[data-remove-id]")?.addEventListener("click", () => {
        removeItem(item.id);
        updateBadges();
        renderPanelPage();
      });

      list.appendChild(li);
    });
  };

  const init = () => {
    bindSaveButtons();
    updateBadges();
    renderPanelPage();
  };

  window.MvlLocalPanel = {
    getItems,
    saveItem,
    removeItem,
    clearAll,
    updateBadges,
    renderPanelPage,
  };

  document.addEventListener("DOMContentLoaded", init);
})();
