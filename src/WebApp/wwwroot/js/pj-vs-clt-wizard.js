(function () {
    const form = document.querySelector('[data-pj-wizard]');
    if (!form) {
        return;
    }

    const panels = Array.from(form.querySelectorAll('[data-pj-step]'));
    const stepperBars = Array.from(form.querySelectorAll('[data-pj-stepper]'));
    const stepLabels = Array.from(form.querySelectorAll('[data-pj-step-label]'));
    const tips = Array.from(form.querySelectorAll('[data-pj-tip]'));
    const progress = form.querySelector('.valora-pj-wizard-stepper');
    let currentStep = 1;

    const amountInput = form.querySelector('[name="Input.Amount"]');
    const secondaryInput = form.querySelector('[name="Input.SecondaryAmount"]');
    const rateInput = form.querySelector('[name="Input.Rate"]');
    const reviewAmount = form.querySelector('[data-pj-review="amount"]');
    const reviewSecondary = form.querySelector('[data-pj-review="secondary"]');
    const reviewRate = form.querySelector('[data-pj-review="rate"]');

    function formatCurrency(value) {
        if (!value || !value.trim()) {
            return '—';
        }

        return `R$ ${value.trim()}`;
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

        if (reviewRate && rateInput) {
            reviewRate.textContent = rateInput.value?.trim()
                ? `${rateInput.value.trim()}%`
                : '—';
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
        }

        if (step === 3) {
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
        if (nextStep >= 1 && nextStep <= 3) {
            showStep(nextStep);
        }
    });

    showStep(1);
})();
