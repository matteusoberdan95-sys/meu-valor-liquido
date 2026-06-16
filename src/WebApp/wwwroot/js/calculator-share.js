document.addEventListener("DOMContentLoaded", () => {
  const root = document.querySelector("[data-share-root], .valora-share-actions");
  if (!root) {
    return;
  }

  const feedback = root.querySelector("[data-share-feedback]");
  const urlField = document.getElementById("calculator-share-url");
  const textField = document.getElementById("calculator-share-text");

  const showFeedback = (message) => {
    if (!feedback) {
      return;
    }

    feedback.textContent = message;
    feedback.hidden = false;
    window.setTimeout(() => {
      feedback.hidden = true;
    }, 3000);
  };

  const copyValue = async (value, successMessage) => {
    if (!value) {
      return;
    }

    try {
      if (navigator.clipboard?.writeText) {
        await navigator.clipboard.writeText(value);
      } else {
        const helper = document.createElement("textarea");
        helper.value = value;
        helper.setAttribute("readonly", "");
        helper.style.position = "absolute";
        helper.style.left = "-9999px";
        document.body.appendChild(helper);
        helper.select();
        document.execCommand("copy");
        document.body.removeChild(helper);
      }

      showFeedback(successMessage);
    } catch {
      showFeedback("Não foi possível copiar. Tente selecionar o texto manualmente.");
    }
  };

  const recordShare = () => {
    const slug = root.getAttribute("data-share-calculator-slug");
    window.MvlMetrics?.collect("share_copy", slug);
  };

  root.querySelectorAll("[data-share-copy]").forEach((button) => {
    button.addEventListener("click", async () => {
      const mode = button.getAttribute("data-share-copy");
      if (mode === "link") {
        await copyValue(urlField?.value ?? "", "Link copiado.");
        recordShare();
        return;
      }

      if (mode === "text") {
        await copyValue(textField?.value ?? "", "Texto copiado.");
        recordShare();
      }
    });
  });

  if (navigator.share && urlField?.value && textField?.value) {
    const nativeButton = document.createElement("button");
    nativeButton.type = "button";
    nativeButton.className = "valora-btn valora-btn-outline";
    nativeButton.innerHTML = '<span class="material-symbols-outlined" style="font-size: 1.125rem;">ios_share</span> Compartilhar';
    nativeButton.addEventListener("click", async () => {
      try {
        await navigator.share({
          title: "Meu Valor Líquido",
          text: textField.value,
          url: urlField.value,
        });
      } catch {
        // Usuário cancelou ou API indisponível.
      }
    });

    const container = root.querySelector(".valora-share-buttons");
    container?.appendChild(nativeButton);
  }
});
