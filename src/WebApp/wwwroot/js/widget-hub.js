document.addEventListener("DOMContentLoaded", () => {
  const hub = document.querySelector("[data-widget-hub]");
  if (!hub) {
    return;
  }

  const catalogNode = document.getElementById("mvl-widget-catalog");
  if (!catalogNode?.textContent) {
    return;
  }

  let catalog;
  try {
    catalog = JSON.parse(catalogNode.textContent);
  } catch {
    return;
  }

  const bySlug = new Map(catalog.map((item) => [item.slug, item]));
  const preview = hub.querySelector("[data-widget-preview]");
  const codeField = hub.querySelector("[data-widget-code]");
  const summary = hub.querySelector("[data-widget-summary]");
  const codeLabel = hub.querySelector("[data-widget-code-label]");
  const fullLink = hub.querySelector("[data-widget-full-link]");
  const copyButton = hub.querySelector("[data-copy-widget]");
  const defaultSlug = hub.getAttribute("data-default-slug") ?? catalog[0]?.slug;

  const setActiveChip = (slug) => {
    hub.querySelectorAll("[data-widget-chip]").forEach((chip) => {
      const isActive = chip.getAttribute("data-widget-chip") === slug;
      chip.classList.toggle("valora-stitch-widget-chip--active", isActive);
      chip.setAttribute("aria-selected", isActive ? "true" : "false");
    });
  };

  const selectWidget = (slug) => {
    const widget = bySlug.get(slug);
    if (!widget || !preview || !codeField) {
      return;
    }

    preview.src = widget.widgetPath;
    preview.height = String(widget.recommendedHeight);
    preview.title = `${widget.name} — Meu Valor Líquido`;
    codeField.value = widget.iframeCode;

    if (summary) {
      summary.textContent = widget.summary;
    }

    if (codeLabel) {
      codeLabel.textContent = `Snippet para ${widget.name}`;
    }

    if (fullLink) {
      fullLink.href = `/calculadoras/${widget.slug}`;
    }

    setActiveChip(slug);
  };

  hub.querySelectorAll("[data-widget-chip]").forEach((chip) => {
    chip.addEventListener("click", () => {
      selectWidget(chip.getAttribute("data-widget-chip"));
    });
  });

  hub.querySelectorAll("[data-widget-jump]").forEach((button) => {
    button.addEventListener("click", () => {
      const slug = button.getAttribute("data-widget-jump");
      selectWidget(slug);
      hub.querySelector("[data-widget-builder-title]")?.scrollIntoView({ behavior: "smooth", block: "start" });
    });
  });

  if (copyButton && codeField) {
    copyButton.addEventListener("click", async () => {
      try {
        if (navigator.clipboard?.writeText) {
          await navigator.clipboard.writeText(codeField.value);
        } else {
          codeField.select();
          document.execCommand("copy");
        }

        const original = copyButton.textContent;
        copyButton.textContent = "Copiado!";
        window.setTimeout(() => {
          copyButton.textContent = original;
        }, 2000);
      } catch {
        codeField.focus();
        codeField.select();
      }
    });
  }

  if (defaultSlug) {
    selectWidget(defaultSlug);
  }
});
