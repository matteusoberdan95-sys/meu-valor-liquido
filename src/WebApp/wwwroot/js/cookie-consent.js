(function () {
    var storageKey = "mvl-cookie-consent";
    var consentVersion = 2;
    var policyVersion = "2026-07-17";
    var personalizationStorageKeys = ["mvl-local-panel-v1", "mvl-rescisao-checklist-v1"];

    function parseConsent(raw) {
        if (!raw) {
            return null;
        }

        try {
            var parsed = JSON.parse(raw);
            if (parsed
                && parsed.version === consentVersion
                && parsed.policyVersion === policyVersion
                && parsed.essential === true) {
                return {
                    version: consentVersion,
                    policyVersion: policyVersion,
                    essential: true,
                    analytics: parsed.analytics === true,
                    personalization: parsed.personalization === true,
                    advertising: parsed.advertising === true,
                    updatedAt: parsed.updatedAt || null
                };
            }
        } catch (error) {
            return null;
        }

        return null;
    }

    function readConsent() {
        return parseConsent(localStorage.getItem(storageKey));
    }

    function allows(category) {
        var consent = readConsent();
        return !!(consent && consent[category] === true);
    }

    function writeConsent(consent) {
        localStorage.setItem(storageKey, JSON.stringify(consent));
        document.dispatchEvent(new CustomEvent("mvl:cookie-consent", { detail: consent }));
    }

    function clearPersonalizationStorage() {
        personalizationStorageKeys.forEach(function (key) {
            localStorage.removeItem(key);
        });
    }

    function loadScript(src, attributes) {
        return new Promise(function (resolve, reject) {
            if (document.querySelector('script[src="' + src + '"]')) {
                resolve();
                return;
            }

            var script = document.createElement("script");
            script.src = src;
            script.async = true;
            if (attributes) {
                Object.keys(attributes).forEach(function (key) {
                    script.setAttribute(key, attributes[key]);
                });
            }

            script.onload = function () { resolve(); };
            script.onerror = function () { reject(new Error("script-load-failed")); };
            document.head.appendChild(script);
        });
    }

    function adSenseScriptSelector() {
        return 'script[src^="https://pagead2.googlesyndication.com/pagead/js/adsbygoogle.js"]';
    }

    function loadAdSense(publisherId) {
        if (!publisherId) {
            return;
        }

        var src = "https://pagead2.googlesyndication.com/pagead/js/adsbygoogle.js?client=" + encodeURIComponent(publisherId);
        loadScript(src, { crossorigin: "anonymous" })
            .then(function () {
                if (window.MvlAdSense && typeof window.MvlAdSense.init === "function") {
                    window.MvlAdSense.init();
                }
            })
            .catch(function () { });
    }

    function updateAdSlots(isAllowed) {
        document.querySelectorAll("[data-ad-consent-required]").forEach(function (slot) {
            slot.hidden = !isAllowed;
        });
    }

    function setPreferenceInputs(banner, consent) {
        var current = consent || {};
        var categories = ["analytics", "personalization", "advertising"];
        categories.forEach(function (category) {
            var input = banner.querySelector("[data-cookie-consent-" + category + "]");
            if (input) {
                input.checked = current[category] === true;
            }
        });
    }

    function applyConsent(consent, banner, adsSlotsEnabled, adsScriptEnabled, publisherId) {
        if (!consent) {
            updateAdSlots(false);
            return;
        }

        banner.hidden = true;
        updateAdSlots(consent.advertising && adsSlotsEnabled);

        if (consent.advertising && adsScriptEnabled) {
            loadAdSense(publisherId);
        }
    }

    function createConsent(analytics, personalization, advertising) {
        return {
            version: consentVersion,
            policyVersion: policyVersion,
            essential: true,
            analytics: !!analytics,
            personalization: !!personalization,
            advertising: !!advertising,
            updatedAt: new Date().toISOString()
        };
    }

    function showBanner(banner, consent, openPreferences) {
        setPreferenceInputs(banner, consent);
        banner.hidden = false;

        var preferences = banner.querySelector("[data-cookie-consent-preferences]");
        var customizeButton = banner.querySelector("[data-cookie-consent-customize]");
        if (preferences && customizeButton) {
            preferences.toggleAttribute("hidden", !openPreferences);
            customizeButton.setAttribute("aria-expanded", openPreferences ? "true" : "false");
        }

        var focusTarget = openPreferences
            ? banner.querySelector("[data-cookie-consent-analytics]")
            : banner.querySelector("[data-cookie-consent-accept]");
        focusTarget?.focus();
    }

    function init() {
        var banner = document.querySelector("[data-cookie-consent]");
        if (!banner) {
            return;
        }

        var adsSlotsEnabled = banner.getAttribute("data-ads-slots-enabled") === "true";
        var adsScriptEnabled = banner.getAttribute("data-ads-script-enabled") === "true";
        var publisherId = banner.getAttribute("data-ads-publisher") || "";
        var consent = readConsent();

        if (consent) {
            applyConsent(consent, banner, adsSlotsEnabled, adsScriptEnabled, publisherId);
        } else {
            showBanner(banner, null, false);
        }

        function saveCategories(analytics, personalization, advertising) {
            var previous = readConsent();
            var next = createConsent(analytics, personalization, advertising);
            if (!next.personalization) {
                clearPersonalizationStorage();
            }

            writeConsent(next);
            applyConsent(next, banner, adsSlotsEnabled, adsScriptEnabled, publisherId);

            if (previous
                && previous.advertising
                && !next.advertising
                && document.querySelector(adSenseScriptSelector())) {
                window.location.reload();
            }
        }

        banner.querySelector("[data-cookie-consent-accept]")?.addEventListener("click", function () {
            saveCategories(true, true, true);
        });

        banner.querySelector("[data-cookie-consent-essential]")?.addEventListener("click", function () {
            saveCategories(false, false, false);
        });

        banner.querySelector("[data-cookie-consent-close]")?.addEventListener("click", function () {
            var current = readConsent();
            if (current) {
                banner.hidden = true;
                return;
            }

            saveCategories(false, false, false);
        });

        var preferences = banner.querySelector("[data-cookie-consent-preferences]");
        var customizeButton = banner.querySelector("[data-cookie-consent-customize]");
        customizeButton?.addEventListener("click", function () {
            var isOpen = preferences?.hasAttribute("hidden") === true;
            preferences?.toggleAttribute("hidden", !isOpen);
            customizeButton.setAttribute("aria-expanded", isOpen ? "true" : "false");
        });

        banner.querySelector("[data-cookie-consent-save]")?.addEventListener("click", function () {
            var analytics = banner.querySelector("[data-cookie-consent-analytics]")?.checked === true;
            var personalization = banner.querySelector("[data-cookie-consent-personalization]")?.checked === true;
            var advertising = banner.querySelector("[data-cookie-consent-advertising]")?.checked === true;
            saveCategories(analytics, personalization, advertising);
        });

        function manageConsent(event) {
            event?.preventDefault();
            showBanner(banner, readConsent(), true);
        }

        document.querySelectorAll("[data-cookie-consent-manage]").forEach(function (button) {
            button.addEventListener("click", manageConsent);
        });

        window.MvlCookieConsent = {
            get: readConsent,
            allows: allows,
            manage: function () { showBanner(banner, readConsent(), true); },
            reset: function () {
                localStorage.removeItem(storageKey);
                updateAdSlots(false);
                showBanner(banner, null, false);
            },
            version: consentVersion,
            policyVersion: policyVersion
        };
    }

    if (document.readyState === "loading") {
        document.addEventListener("DOMContentLoaded", init);
    } else {
        init();
    }
})();
