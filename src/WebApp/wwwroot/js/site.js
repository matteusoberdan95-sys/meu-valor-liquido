document.addEventListener("DOMContentLoaded", () => {
  const loadScriptOnce = (id, src) => {
    if (document.getElementById(id)) {
      return;
    }

    const script = document.createElement("script");
    script.id = id;
    script.src = src;
    script.defer = true;
    document.body.appendChild(script);
  };

  if (
    document.querySelector(
      "[data-local-panel-save], [data-local-panel-page], [data-local-panel-count]",
    )
  ) {
    loadScriptOnce("mvl-local-panel-script", "/js/local-panel.js");
  }

  const toggle = document.querySelector("[data-nav-toggle]");
  const mobileNav = document.querySelector("[data-nav-mobile]");

  if (toggle && mobileNav) {
    toggle.addEventListener("click", () => {
      const isOpen = mobileNav.classList.toggle("open");
      toggle.setAttribute("aria-expanded", isOpen ? "true" : "false");
    });
  }

  const assistantLauncher = document.querySelector("[data-assistant-launcher]");
  const assistantLauncherToggle = document.querySelector("[data-assistant-launcher-toggle]");
  const assistantLauncherPanel = document.querySelector("[data-assistant-launcher-panel]");

  if (assistantLauncher && assistantLauncherToggle && assistantLauncherPanel) {
    assistantLauncherToggle.addEventListener("click", () => {
      const isOpen = assistantLauncherPanel.hasAttribute("hidden");
      assistantLauncherPanel.toggleAttribute("hidden", !isOpen);
      assistantLauncher.classList.toggle("valora-assistant-launcher--open", isOpen);
      assistantLauncherToggle.setAttribute("aria-expanded", isOpen ? "true" : "false");
    });

    document.addEventListener("click", (event) => {
      if (!assistantLauncher.contains(event.target)) {
        assistantLauncherPanel.setAttribute("hidden", "");
        assistantLauncher.classList.remove("valora-assistant-launcher--open");
        assistantLauncherToggle.setAttribute("aria-expanded", "false");
      }
    });
  }

  const assistantChat = document.querySelector("[data-assistant-chat]");
  const assistantForm = document.querySelector("[data-assistant-chat-form]");
  const assistantInput = document.querySelector("[data-assistant-input]");
  const assistantLog = document.querySelector("[data-assistant-log]");

  if (assistantChat && assistantForm && assistantInput && assistantLog) {
    const answers = {
      inss: {
        text: "O INSS é calculado por faixas progressivas. A forma mais segura é informar o salário bruto na calculadora para ver cada faixa e o desconto total.",
        title: "Calcular INSS",
        href: "/calculadoras/inss"
      },
      irrf: {
        text: "O IRRF considera salário após INSS, dependentes e deduções permitidas. Use a calculadora para separar imposto, base de cálculo e salário líquido.",
        title: "Calcular IRRF",
        href: "/calculadoras/irrf"
      },
      "clt-pj": {
        text: "CLT vs PJ depende do líquido, impostos, benefícios, férias, 13º e risco. Compare o salário CLT com o faturamento PJ antes de decidir.",
        title: "Comparar CLT vs PJ",
        href: "/calculadoras/pj-vs-clt"
      },
      rescisao: {
        text: "A rescisão pode envolver saldo de salário, aviso prévio, férias, 13º, FGTS e multa. O tipo de desligamento muda bastante o resultado.",
        title: "Simular rescisão",
        href: "/calculadoras/rescisao-clt"
      },
      holerite: {
        text: "Para conferir holerite, compare o INSS e IRRF informados com o esperado para o salário bruto, dependentes e descontos.",
        title: "Conferir holerite",
        href: "/conferir-holerite"
      },
      default: {
        text: "Posso ajudar com temas do Meu Valor Líquido: salário líquido, INSS, IRRF, férias, 13º, rescisão, FGTS, holerite e CLT vs PJ.",
        title: "Ver dúvidas populares",
        href: "/duvidas"
      }
    };

    const detectIntent = (text) => {
      const normalized = text.toLowerCase();
      if (normalized.includes("inss")) return "inss";
      if (normalized.includes("irrf") || normalized.includes("imposto")) return "irrf";
      if (normalized.includes("pj") || normalized.includes("clt")) return "clt-pj";
      if (normalized.includes("rescis") || normalized.includes("demiss")) return "rescisao";
      if (normalized.includes("holerite") || normalized.includes("contracheque")) return "holerite";
      return "default";
    };

    const appendMessage = (role, text, answer) => {
      const article = document.createElement("article");
      article.className = `valora-assistant-message valora-assistant-message--${role}`;

      if (role === "ai") {
        const avatar = document.createElement("div");
        avatar.className = "valora-assistant-avatar";
        avatar.setAttribute("aria-hidden", "true");
        avatar.innerHTML = '<span class="material-symbols-outlined">smart_toy</span>';
        article.appendChild(avatar);
      }

      const bubble = document.createElement("div");
      bubble.className = "valora-assistant-bubble";
      const paragraph = document.createElement("p");
      paragraph.textContent = text;
      bubble.appendChild(paragraph);

      if (answer) {
        const card = document.createElement("div");
        card.className = "valora-assistant-answer-card";
        card.innerHTML = '<span class="material-symbols-outlined" aria-hidden="true">north_east</span>';

        const cardText = document.createElement("div");
        const strong = document.createElement("strong");
        strong.textContent = answer.title;
        const span = document.createElement("span");
        span.textContent = "Abrir ferramenta relacionada";
        cardText.appendChild(strong);
        cardText.appendChild(span);

        const link = document.createElement("a");
        link.href = answer.href;
        link.textContent = "Abrir";

        card.appendChild(cardText);
        card.appendChild(link);
        bubble.appendChild(card);
      }

      article.appendChild(bubble);

      if (role === "ai") {
        const disclaimer = document.createElement("p");
        disclaimer.className = "valora-assistant-disclaimer";
        disclaimer.textContent = "Conteúdo educativo. Não substitui orientação profissional.";
        article.appendChild(disclaimer);
      }

      assistantLog.appendChild(article);
      assistantLog.scrollTop = assistantLog.scrollHeight;
    };

    assistantChat.querySelectorAll("[data-assistant-prompt]").forEach((button) => {
      button.addEventListener("click", () => {
        assistantInput.value = button.dataset.assistantPrompt || "";
        assistantInput.focus();
      });
    });

    assistantForm.addEventListener("submit", (event) => {
      event.preventDefault();
      const question = assistantInput.value.trim();
      if (!question) return;

      const intent = detectIntent(question);
      const answer = answers[intent] || answers.default;
      appendMessage("user", question);
      appendMessage("ai", answer.text, answer);
      assistantInput.value = "";
    });
  }
});
