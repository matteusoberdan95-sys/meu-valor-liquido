(function () {
  "use strict";

  const formatCurrencyFromCents = (digits) => {
    if (!digits) {
      return "";
    }

    const cents = parseInt(digits, 10);
    if (Number.isNaN(cents)) {
      return "";
    }

    return (cents / 100).toLocaleString("pt-BR", {
      minimumFractionDigits: 2,
      maximumFractionDigits: 2,
    });
  };

  const parseBrazilianNumber = (value) => {
    if (!value) {
      return 0;
    }

    const normalized = String(value).trim().replace(/\s/g, "");
    if (!normalized) {
      return 0;
    }

    if (normalized.includes(",")) {
      const parsed = Number(normalized.replace(/\./g, "").replace(",", "."));
      return Number.isNaN(parsed) ? 0 : parsed;
    }

    const parsed = Number(normalized.replace(/[^\d.-]/g, ""));
    return Number.isNaN(parsed) ? 0 : parsed;
  };

  const onlyDigits = (value) => value.replace(/\D/g, "");

  const applyCurrencyMask = (input) => {
    const digits = onlyDigits(input.value);
    input.dataset.cents = digits;
    input.value = formatCurrencyFromCents(digits);
  };

  const setCurrencyFromAmount = (input, amount) => {
    if (!amount || amount <= 0) {
      input.dataset.cents = "";
      input.value = "";
      return;
    }

    const digits = Math.round(amount * 100).toString();
    input.dataset.cents = digits;
    input.value = formatCurrencyFromCents(digits);
  };

  const initCurrencyInput = (input) => {
    setCurrencyFromAmount(input, parseBrazilianNumber(input.value));
  };

  const applyIntegerMask = (input) => {
    const maxLength = input.dataset.maxLength ? parseInt(input.dataset.maxLength, 10) : null;
    let digits = onlyDigits(input.value);
    if (maxLength) {
      digits = digits.slice(0, maxLength);
    }

    input.value = digits;
  };

  const initIntegerInput = (input) => {
    if (input.value) {
      applyIntegerMask(input);
    }
  };

  const applyDecimalMask = (input) => {
    let raw = input.value.replace(/[^\d,]/g, "");
    const parts = raw.split(",");
    if (parts.length > 2) {
      raw = parts[0] + "," + parts.slice(1).join("");
    }

    if (parts.length === 2) {
      raw = parts[0] + "," + parts[1].slice(0, 2);
    }

    input.value = raw;
  };

  const initDecimalInput = (input) => {
    const existing = parseBrazilianNumber(input.value);
    if (existing > 0) {
      input.value = String(existing).replace(".", ",");
    }
  };

  const prepareForSubmit = (input) => {
    const mask = input.dataset.mask;
    if (mask === "currency") {
      const cents = parseInt(input.dataset.cents || onlyDigits(input.value) || "0", 10);
      const amount = cents / 100;
      input.value = amount.toFixed(2).replace(".", ",");
      return;
    }

    if (mask === "integer") {
      input.value = onlyDigits(input.value);
      return;
    }

    if (mask === "decimal") {
      const parsed = parseBrazilianNumber(input.value);
      input.value = Number.isInteger(parsed)
        ? String(parsed)
        : parsed.toFixed(2).replace(".", ",");
    }
  };

  const onCurrencyPaste = (input, event) => {
    const text = event.clipboardData?.getData("text");
    if (!text) {
      return;
    }

    event.preventDefault();
    setCurrencyFromAmount(input, parseBrazilianNumber(text));
  };

  const bindMask = (input) => {
    const mask = input.dataset.mask;
    if (!mask) {
      return;
    }

    input.setAttribute("inputmode", mask === "currency" || mask === "decimal" ? "decimal" : "numeric");
    input.setAttribute("autocomplete", "off");

    if (mask === "currency") {
      initCurrencyInput(input);
      input.addEventListener("input", () => applyCurrencyMask(input));
      input.addEventListener("paste", (event) => onCurrencyPaste(input, event));
      input.addEventListener("blur", () => applyCurrencyMask(input));
      input.addEventListener("focus", () => input.select());
      return;
    }

    if (mask === "integer") {
      initIntegerInput(input);
      input.addEventListener("input", () => applyIntegerMask(input));
      return;
    }

    if (mask === "decimal") {
      initDecimalInput(input);
      input.addEventListener("input", () => applyDecimalMask(input));
    }
  };

  const initCalculatorMasks = () => {
    if (window.jQuery?.validator) {
      window.jQuery.validator.setDefaults({
        ignore: ":hidden, [data-mask]",
      });
    }

    document.querySelectorAll("[data-mask]").forEach(bindMask);

    const maskedForms = new Set();
    document.querySelectorAll("[data-mask]").forEach((input) => {
      const form = input.closest("form");
      if (form) {
        maskedForms.add(form);
      }
    });

    maskedForms.forEach((form) => {
      if (form.dataset.maskSubmitBound === "true") {
        return;
      }

      form.dataset.maskSubmitBound = "true";
      form.addEventListener(
        "submit",
        () => {
          form.querySelectorAll("[data-mask]").forEach(prepareForSubmit);
        },
        true
      );
    });
  };

  if (document.readyState === "loading") {
    document.addEventListener("DOMContentLoaded", initCalculatorMasks);
  } else {
    initCalculatorMasks();
  }
})();
