(function () {
    var storageKey = "mvl-cookie-consent";
    var consentVersion = 1;

    function parseConsent(raw) {
        if (!raw) {
            return null;
        }

        try {
            var parsed = JSON.parse(raw);
            if (parsed && parsed.version === consentVersion) {
                return parsed;
            }
        } catch (error) {
            if (raw === "accepted") {
                return { version: consentVersion, essential: true, advertising: true };
            }
        }

        return null;
    }

    function readConsent() {
        return parseConsent(localStorage.getItem(storageKey));
    }

    function writeConsent(consent) {
        localStorage.setItem(storageKey, JSON.stringify(consent));
        document.dispatchEvent(new CustomEvent("mvl:cookie-consent", { detail: consent }));
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

    function applyConsent(consent, banner, adsEnabled, publisherId) {
        if (!consent) {
            return;
        }

        if (banner) {
            banner.hidden = true;
        }

        if (consent.advertising && adsEnabled) {
            loadAdSense(publisherId);
        }
    }

    function saveConsent(advertising, banner, adsEnabled, publisherId) {
        var consent = {
            version: consentVersion,
            essential: true,
            advertising: !!advertising,
            updatedAt: new Date().toISOString()
        };

        writeConsent(consent);
        applyConsent(consent, banner, adsEnabled, publisherId);
    }

    function showBanner(banner) {
        if (!banner) {
            return;
        }

        banner.hidden = false;
    }

    function init() {
        var banner = document.querySelector("[data-cookie-consent]");
        if (!banner) {
            return;
        }

        var adsEnabled = banner.getAttribute("data-ads-enabled") === "true";
        var publisherId = banner.getAttribute("data-ads-publisher") || "";
        var consent = readConsent();

        if (consent) {
            applyConsent(consent, banner, adsEnabled, publisherId);
        } else {
            showBanner(banner);
        }

        var acceptButton = banner.querySelector("[data-cookie-consent-accept]");
        if (acceptButton) {
            acceptButton.addEventListener("click", function () {
                saveConsent(adsEnabled, banner, adsEnabled, publisherId);
            });
        }

        var essentialButton = banner.querySelector("[data-cookie-consent-essential]");
        if (essentialButton) {
            essentialButton.addEventListener("click", function () {
                saveConsent(false, banner, adsEnabled, publisherId);
            });
        }

        document.querySelectorAll("[data-cookie-consent-manage]").forEach(function (button) {
            button.addEventListener("click", function (event) {
                event.preventDefault();
                localStorage.removeItem(storageKey);
                showBanner(banner);
            });
        });

        window.MvlCookieConsent = {
            get: readConsent,
            reset: function () {
                localStorage.removeItem(storageKey);
                showBanner(banner);
            }
        };
    }

    if (document.readyState === "loading") {
        document.addEventListener("DOMContentLoaded", init);
    } else {
        init();
    }
})();
