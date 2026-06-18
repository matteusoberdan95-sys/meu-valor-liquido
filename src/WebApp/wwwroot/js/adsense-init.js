(function () {
    function initSlots() {
        var slots = document.querySelectorAll("ins.adsbygoogle");
        if (!slots.length || typeof window.adsbygoogle === "undefined") {
            return;
        }

        slots.forEach(function () {
            (window.adsbygoogle = window.adsbygoogle || []).push({});
        });
    }

    window.MvlAdSense = { init: initSlots };
    initSlots();
})();
