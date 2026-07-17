(() => {
  const STORAGE_KEY = "mvl-local-panel-v1";
  const MAX_ITEMS = 25;
  const MAX_COMPARE = 2;
  const personalizationAllowed = () =>
    window.MvlCookieConsent?.allows("personalization") === true;

  const readStore = () => {
    if (!personalizationAllowed()) {
      return { version: 1, items: [] };
    }

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
    if (!personalizationAllowed()) {
      return false;
    }

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

  const escapeHtml = (value) =>
    String(value ?? "")
      .replace(/&/g, "&amp;")
      .replace(/</g, "&lt;")
      .replace(/>/g, "&gt;")
      .replace(/"/g, "&quot;");

  const parseMoney = (value) => {
    if (typeof value === "number" && Number.isFinite(value)) {
      return value;
    }

    const raw = String(value ?? "")
      .replace(/[^\d,-]/g, "")
      .replace(/\./g, "")
      .replace(",", ".");
    const parsed = Number.parseFloat(raw);
    return Number.isFinite(parsed) ? parsed : null;
  };

  const formatMoney = (value) =>
    new Intl.NumberFormat("pt-BR", {
      style: "currency",
      currency: "BRL",
    }).format(value);

  const resolveNetAmount = (item) => {
    if (typeof item.netAmountValue === "number" && Number.isFinite(item.netAmountValue)) {
      return item.netAmountValue;
    }

    return parseMoney(item.netAmount);
  };

  const createId = () => {
    if (window.crypto?.randomUUID) {
      return window.crypto.randomUUID();
    }

    return `mvl-${Date.now()}-${Math.random().toString(16).slice(2)}`;
  };

  const getItems = () => readStore().items;

  const saveItem = (entry) => {
    if (!personalizationAllowed()) {
      return { ok: false, reason: "consent-required" };
    }

    const sharePath = normalizeShareUrl(entry.shareUrl);
    if (!sharePath) {
      return { ok: false, reason: "missing-url" };
    }

    const store = readStore();
    const now = new Date().toISOString();
    const existingIndex = store.items.findIndex(
      (item) => normalizeShareUrl(item.shareUrl) === sharePath,
    );
    const netAmountValue =
      typeof entry.netAmountValue === "number"
        ? entry.netAmountValue
        : parseMoney(entry.netAmount);

    const payload = {
      id: existingIndex >= 0 ? store.items[existingIndex].id : createId(),
      slug: entry.slug,
      calculatorName: entry.calculatorName,
      summary: entry.summary,
      netAmount: entry.netAmount,
      netAmountValue,
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
    compareSelection.delete(id);
    return store.items.length;
  };

  const clearAll = () => {
    writeStore({ version: 1, items: [] });
    compareSelection.clear();
  };

  const compareSelection = new Set();

  const toggleCompareSelection = (id) => {
    if (compareSelection.has(id)) {
      compareSelection.delete(id);
      return { ok: true };
    }

    if (compareSelection.size >= MAX_COMPARE) {
      return { ok: false, reason: "max-selected" };
    }

    compareSelection.add(id);
    return { ok: true };
  };

  const clearCompareSelection = () => {
    compareSelection.clear();
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
          if (result.reason === "consent-required") {
            showFeedback(root, "Ative Personalização para salvar no painel.");
            window.MvlCookieConsent?.manage();
            return;
          }

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

  const buildCompareVerdict = (left, right, leftNet, rightNet) => {
    const sameCalculator = left.slug === right.slug;
    const diff = rightNet - leftNet;
    const absDiff = Math.abs(diff);
    const isGain = diff > 0;
    const isLoss = diff < 0;
    const verdictClass = isGain
      ? "valora-stitch-panel-compare-verdict--gain"
      : isLoss
        ? "valora-stitch-panel-compare-verdict--loss"
        : "valora-stitch-panel-compare-verdict--neutral";

    let title;
    if (leftNet === null || rightNet === null) {
      title = "Comparativo entre simulações salvas";
    } else if (Math.abs(diff) < 0.01) {
      title = "Valores estimados equivalentes";
    } else {
      title = isGain
        ? `Cenário B entrega ${formatMoney(absDiff)} a mais`
        : `Cenário B entrega ${formatMoney(absDiff)} a menos`;
    }

    const lead = sameCalculator
      ? `Mesma calculadora (${escapeHtml(left.calculatorName)}). Use os links para revisar os parâmetros completos.`
      : `Calculadoras diferentes: ${escapeHtml(left.calculatorName)} vs ${escapeHtml(right.calculatorName)}. Compare apenas como referência.`;

    return { verdictClass, title, lead, diff, leftNet, rightNet };
  };

  const renderCompareCard = (item, label, netValue, barWidth) => `
    <section class="valora-stitch-panel-compare-card">
      <p class="valora-stitch-panel-compare-card-kicker">${escapeHtml(label)}</p>
      <span class="valora-badge valora-badge-trabalhista">${escapeHtml(item.calculatorName)}</span>
      <p class="valora-stitch-panel-compare-card-summary">${escapeHtml(item.summary)}</p>
      <p class="valora-stitch-panel-compare-card-net">${escapeHtml(item.netAmount)}</p>
      <p class="valora-stitch-panel-compare-card-net-label">Valor estimado</p>
      ${
        netValue !== null
          ? `<div class="valora-stitch-panel-compare-bar" aria-hidden="true">
              <span class="valora-stitch-panel-compare-bar-fill" style="width: ${barWidth}%"></span>
            </div>`
          : ""
      }
      <p class="valora-text-muted valora-text-sm">${escapeHtml(formatSavedAt(item.savedAt))}</p>
      <a class="valora-btn valora-btn-outline valora-btn-sm" href="${escapeHtml(item.shareUrl)}">Reabrir</a>
    </section>
  `;

  const renderPanelCompare = () => {
    const page = document.querySelector("[data-local-panel-page]");
    const compareRoot = page?.querySelector("[data-local-panel-compare]");
    const hint = page?.querySelector("[data-local-panel-compare-hint]");
    const content = page?.querySelector("[data-local-panel-compare-content]");
    const clearButton = page?.querySelector("[data-local-panel-compare-clear]");

    if (!compareRoot || !hint || !content) {
      return;
    }

    const items = getItems();
    compareRoot.hidden = items.length < 2;

    if (items.length < 2) {
      content.hidden = true;
      hint.hidden = false;
      hint.textContent =
        "Salve ao menos 2 simulações para comparar cenários lado a lado.";
      if (clearButton) {
        clearButton.hidden = true;
      }
      return;
    }

    const selectedIds = [...compareSelection];
    if (selectedIds.length < MAX_COMPARE) {
      content.hidden = true;
      hint.hidden = false;
      hint.textContent =
        selectedIds.length === 0
          ? "Marque 2 simulações na lista acima para ver o comparativo lado a lado."
          : "Selecione mais 1 simulação para comparar.";
      if (clearButton) {
        clearButton.hidden = selectedIds.length === 0;
      }
      return;
    }

    const left = items.find((item) => item.id === selectedIds[0]);
    const right = items.find((item) => item.id === selectedIds[1]);
    if (!left || !right) {
      compareSelection.clear();
      renderPanelCompare();
      return;
    }

    const leftNet = resolveNetAmount(left);
    const rightNet = resolveNetAmount(right);
    const maxNet =
      leftNet !== null && rightNet !== null
        ? Math.max(Math.abs(leftNet), Math.abs(rightNet), 0.01)
        : null;
    const leftBar =
      maxNet === null || leftNet === null ? 0 : Math.min(100, Math.round((Math.abs(leftNet) / maxNet) * 100));
    const rightBar =
      maxNet === null || rightNet === null ? 0 : Math.min(100, Math.round((Math.abs(rightNet) / maxNet) * 100));
    const verdict = buildCompareVerdict(left, right, leftNet, rightNet);

    hint.hidden = true;
    content.hidden = false;
    if (clearButton) {
      clearButton.hidden = false;
    }

    content.innerHTML = `
      <div class="valora-stitch-panel-compare-verdict ${verdict.verdictClass}">
        <div class="valora-stitch-panel-compare-verdict-badge">
          <span class="material-symbols-outlined valora-icon-filled" aria-hidden="true">compare_arrows</span>
          Comparativo
        </div>
        <h4 class="valora-stitch-panel-compare-verdict-title">${escapeHtml(verdict.title)}</h4>
        <p class="valora-stitch-panel-compare-verdict-lead">${verdict.lead}</p>
        ${
          verdict.leftNet !== null && verdict.rightNet !== null && Math.abs(verdict.diff) >= 0.01
            ? `<div class="valora-stitch-panel-compare-verdict-highlight">
                <span>Diferença estimada</span>
                <strong>${verdict.diff > 0 ? "+" : verdict.diff < 0 ? "-" : ""}${formatMoney(Math.abs(verdict.diff))}</strong>
              </div>`
            : ""
        }
      </div>
      <div class="valora-stitch-panel-compare-grid">
        ${renderCompareCard(left, "Cenário A", leftNet, leftBar)}
        ${renderCompareCard(right, "Cenário B", rightNet, rightBar)}
      </div>
    `;

    window.MvlMetrics?.collect("panel_compare", left.slug ?? null);
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
    const compareClearButton = page.querySelector("[data-local-panel-compare-clear]");
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
      renderPanelCompare();
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

    if (compareClearButton && compareClearButton.dataset.localPanelCompareBound !== "true") {
      compareClearButton.dataset.localPanelCompareBound = "true";
      compareClearButton.addEventListener("click", () => {
        clearCompareSelection();
        list.querySelectorAll("[data-compare-select]").forEach((input) => {
          input.checked = false;
        });
        renderPanelCompare();
      });
    }

    items.forEach((item) => {
      const li = document.createElement("li");
      li.className = "valora-local-panel-item";
      const isSelected = compareSelection.has(item.id);
      li.innerHTML = `
        <label class="valora-local-panel-compare-select">
          <input type="checkbox" data-compare-select data-item-id="${escapeHtml(item.id)}" ${isSelected ? "checked" : ""} ${items.length < 2 ? "disabled" : ""} />
          <span>Comparar</span>
        </label>
        <div class="valora-local-panel-item-main">
          <span class="valora-badge valora-badge-trabalhista">${escapeHtml(item.calculatorName)}</span>
          <strong class="valora-local-panel-item-title">${escapeHtml(item.summary)}</strong>
          <span class="valora-local-panel-item-net">${escapeHtml(item.netAmount)}</span>
          <span class="valora-text-muted valora-text-sm">${escapeHtml(formatSavedAt(item.savedAt))}</span>
        </div>
        <div class="valora-local-panel-item-actions">
          <a class="valora-btn valora-btn-primary valora-btn-sm" href="${escapeHtml(item.shareUrl)}">Reabrir</a>
          <button type="button" class="valora-btn valora-btn-outline valora-btn-sm" data-remove-id="${escapeHtml(item.id)}">Remover</button>
        </div>
      `;

      const checkbox = li.querySelector("[data-compare-select]");
      checkbox?.addEventListener("change", () => {
        if (checkbox.checked) {
          const result = toggleCompareSelection(item.id);
          if (!result.ok) {
            checkbox.checked = false;
            const hint = page.querySelector("[data-local-panel-compare-hint]");
            if (hint) {
              hint.hidden = false;
              hint.textContent = "Selecione no máximo 2 simulações por vez.";
            }
            return;
          }
        } else {
          compareSelection.delete(item.id);
        }

        renderPanelCompare();
      });

      li.querySelector("[data-remove-id]")?.addEventListener("click", () => {
        removeItem(item.id);
        updateBadges();
        renderPanelPage();
      });

      list.appendChild(li);
    });

    renderPanelCompare();
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
    parseMoney,
    resolveNetAmount,
    compareSelection,
    toggleCompareSelection,
    clearCompareSelection,
    renderPanelCompare,
  };

  if (document.readyState === "loading") {
    document.addEventListener("DOMContentLoaded", init);
  } else {
    init();
  }
})();
