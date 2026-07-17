(() => {
  const STORAGE_KEY = "mvl-rescisao-checklist-v1";
  const personalizationAllowed = () =>
    window.MvlCookieConsent?.allows("personalization") === true;

  const readStore = () => {
    if (!personalizationAllowed()) {
      return { version: 1, checked: [] };
    }

    try {
      const raw = window.localStorage.getItem(STORAGE_KEY);
      if (!raw) {
        return { version: 1, checked: [] };
      }

      const parsed = JSON.parse(raw);
      if (!parsed || !Array.isArray(parsed.checked)) {
        return { version: 1, checked: [] };
      }

      return parsed;
    } catch {
      return { version: 1, checked: [] };
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

  const initChecklist = (root) => {
    const toggles = root.querySelectorAll("[data-rescisao-checklist-toggle]");
    const progressEl = root.querySelector("[data-rescisao-checklist-progress]");
    const resetButton = root.querySelector("[data-rescisao-checklist-reset]");
    const total = toggles.length;

    const syncUi = (checkedIds) => {
      toggles.forEach((input) => {
        const id = input.getAttribute("data-rescisao-checklist-toggle");
        input.checked = checkedIds.includes(id);
        const item = root.querySelector(`[data-rescisao-checklist-item="${id}"]`);
        item?.classList.toggle("valora-stitch-rescisao-checklist-item--done", input.checked);
      });

      if (progressEl) {
        progressEl.textContent = String(checkedIds.length);
      }

      if (resetButton) {
        resetButton.hidden = checkedIds.length === 0;
      }
    };

    let store = readStore();
    syncUi(store.checked);

    toggles.forEach((input) => {
      input.addEventListener("change", () => {
        if (!personalizationAllowed()) {
          syncUi(store.checked);
          window.MvlCookieConsent?.manage();
          return;
        }

        const id = input.getAttribute("data-rescisao-checklist-toggle");
        if (!id) {
          return;
        }

        const checked = new Set(store.checked);
        if (input.checked) {
          checked.add(id);
        } else {
          checked.delete(id);
        }

        store = { version: 1, checked: [...checked] };
        writeStore(store);
        syncUi(store.checked);
      });
    });

    resetButton?.addEventListener("click", () => {
      store = { version: 1, checked: [] };
      writeStore(store);
      syncUi([]);
    });
  };

  document.querySelectorAll("[data-rescisao-checklist]").forEach(initChecklist);
})();
