(function () {
    var storageKey = "mvl-cookie-consent";
    var banner = document.querySelector("[data-cookie-consent]");
    if (!banner) {
        return;
    }

    if (localStorage.getItem(storageKey) === "accepted") {
        banner.hidden = true;
        return;
    }

    banner.hidden = false;
    var acceptButton = banner.querySelector("[data-cookie-consent-accept]");
    if (!acceptButton) {
        return;
    }

    acceptButton.addEventListener("click", function () {
        localStorage.setItem(storageKey, "accepted");
        banner.hidden = true;
    });
})();
