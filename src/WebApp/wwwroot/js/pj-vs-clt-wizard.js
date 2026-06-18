(function () {
    const form = document.querySelector('[data-pj-wizard]');
    if (!form) {
        return;
    }

    const maxStep = 4;
    const panels = Array.from(form.querySelectorAll('[data-pj-step]'));
    const stepperBars = Array.from(form.querySelectorAll('[data-pj-stepper]'));
    const stepLabels = Array.from(form.querySelectorAll('[data-pj-step-label]'));
    const tips = Array.from(form.querySelectorAll('[data-pj-tip]'));
    const progress = form.querySelector('.valora-pj-wizard-stepper');
    let currentStep = 1;

    const amountInput = form.querySelector('[name="Input.Amount"]');
    const secondaryInput = form.querySelector('[name="Input.SecondaryAmount"]');
    const rateInput = form.querySelector('[name="Input.Rate"]');
    const proLaboreInput = form.querySelector('[name="Input.ProLaborePercent"]');
    const annexSelect = form.querySelector('[data-pj-annex-select]');
    const annexHint = form.querySelector('[data-pj-annex-hint]');
    const reviewAmount = form.querySelector('[data-pj-review="amount"]');
    const reviewSecondary = form.querySelector('[data-pj-review="secondary"]');
    const reviewRate = form.querySelector('[data-pj-review="rate"]');
    const reviewProLabore = form.querySelector('[data-pj-review="prolabore"]');

    const annexRatesElement = document.getElementById('pj-simples-annex-rates');
    const annexRates = annexRatesElement
        ? JSON.parse(annexRatesElement.textContent || '{}')
        : {};

    const annexDescriptions = {
        AnnexOne: 'Comércio em geral, varejo e revenda.',
        AnnexTwo: 'Indústria e transformação de produtos.',
        AnnexThree: 'Serviços como locação, academias e agências.',
        AnnexFour: 'Serviços com folha relevante (fator R ≥ 28%).',
        AnnexFive: 'Serviços intelectuais, TI, engenharia e consultoria.'
    };

    function formatCurrency(value) {
        if (!value || !value.trim()) {
            return '—';
        }

        return `R$ ${value.trim()}`;
    }

    function selectedAnnexKey() {
        if (!annexSelect) {
            return null;
        }

        const option = annexSelect.options[annexSelect.selectedIndex];
        return option ? option.text.split('—')[0].trim() : null;
    }

    function suggestedRateForAnnex() {
        if (!annexSelect) {
            return null;
        }

        const selectedOption = annexSelect.options[annexSelect.selectedIndex];
        if (!selectedOption) {
            return null;
        }

        const enumName = selectedOption.value;
        const keys = Object.keys(annexRates);
        const matchKey = keys.find((key) => key === enumName || String(annexSelect.selectedIndex) === key);
        if (matchKey && annexRates[matchKey] !== undefined) {
            return annexRates[matchKey];
        }

        return annexRates[enumName] ?? null;
    }

    function updateAnnexHint() {
        if (!annexSelect || !annexHint) {
            return;
        }

        const selectedOption = annexSelect.options[annexSelect.selectedIndex];
        const enumName = selectedOption ? Object.keys(annexDescriptions).find((key, index) => index === annexSelect.selectedIndex) : null;
        const description = enumName ? annexDescriptions[enumName] : '';
        const suggested = suggestedRateForAnnex();
        annexHint.textContent = suggested
            ? `${description} Alíquota sugerida na 1ª faixa: ${suggested}%`
            : description;
    }

    function updateReview() {
        if (reviewAmount && amountInput) {
            reviewAmount.textContent = formatCurrency(amountInput.value);
        }

        if (reviewSecondary && secondaryInput) {
            reviewSecondary.textContent = secondaryInput.value?.trim()
                ? formatCurrency(secondaryInput.value)
                : 'Estimado automaticamente';
        }

        if (reviewRate) {
            const annex = selectedAnnexKey();
            const customRate = rateInput?.value?.trim();
            if (customRate) {
                reviewRate.textContent = `${customRate}% (${annex || 'anexo'})`;
            } else {
                const suggested = suggestedRateForAnnex();
                reviewRate.textContent = suggested
                    ? `${suggested}% sugerido — ${annex || 'anexo'}`
                    : '—';
            }
        }

        if (reviewProLabore && proLaboreInput) {
            reviewProLabore.textContent = proLaboreInput.value?.trim()
                ? `${proLaboreInput.value.trim()}%`
                : '28% (padrão)';
        }
    }

    function showStep(step) {
        currentStep = step;

        panels.forEach((panel) => {
            const panelStep = Number(panel.getAttribute('data-pj-step'));
            const isActive = panelStep === step;
            panel.hidden = !isActive;
        });

        stepperBars.forEach((bar) => {
            const barStep = Number(bar.getAttribute('data-pj-stepper'));
            bar.classList.toggle('valora-pj-wizard-stepper-bar--active', barStep <= step);
        });

        stepLabels.forEach((label) => {
            const labelStep = Number(label.getAttribute('data-pj-step-label'));
            label.classList.toggle('valora-pj-wizard-step--active', labelStep === step);
        });

        tips.forEach((tip) => {
            const tipStep = Number(tip.getAttribute('data-pj-tip'));
            tip.hidden = tipStep !== step;
        });

        if (progress) {
            progress.setAttribute('aria-valuenow', String(step));
            progress.setAttribute('aria-valuemax', String(maxStep));
        }

        if (step === maxStep) {
            updateReview();
        }
    }

    form.addEventListener('click', (event) => {
        const target = event.target.closest('[data-pj-goto]');
        if (!target) {
            return;
        }

        event.preventDefault();
        const nextStep = Number(target.getAttribute('data-pj-goto'));
        if (nextStep >= 1 && nextStep <= maxStep) {
            showStep(nextStep);
        }
    });

    if (annexSelect) {
        annexSelect.addEventListener('change', updateAnnexHint);
        updateAnnexHint();
    }

    showStep(1);
})();
