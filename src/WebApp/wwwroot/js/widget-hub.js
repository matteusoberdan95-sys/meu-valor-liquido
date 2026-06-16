document.addEventListener("DOMContentLoaded", () => {
  document.querySelectorAll("[data-copy-widget]").forEach((button) => {
    button.addEventListener("click", async () => {
      const slug = button.getAttribute("data-copy-widget");
      const field = document.getElementById(`code-${slug}`);
      if (!field?.value) {
        return;
      }

      try {
        if (navigator.clipboard?.writeText) {
          await navigator.clipboard.writeText(field.value);
        } else {
          field.select();
          document.execCommand("copy");
        }

        const original = button.textContent;
        button.textContent = "Copiado!";
        window.setTimeout(() => {
          button.textContent = original;
        }, 2000);
      } catch {
        field.focus();
        field.select();
      }
    });
  });
});
