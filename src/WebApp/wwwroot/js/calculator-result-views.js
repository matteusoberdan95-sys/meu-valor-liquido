(function () {
    document.querySelectorAll('[data-result-view-root]').forEach(function (root) {
        var buttons = root.querySelectorAll('[data-result-view]');
        var panels = root.querySelectorAll('[data-result-panel]');

        buttons.forEach(function (button) {
            button.addEventListener('click', function () {
                var view = button.getAttribute('data-result-view');
                if (!view) {
                    return;
                }

                buttons.forEach(function (item) {
                    var selected = item === button;
                    item.setAttribute('aria-selected', selected ? 'true' : 'false');
                });

                panels.forEach(function (panel) {
                    var active = panel.getAttribute('data-result-panel') === view;
                    panel.hidden = !active;
                });
            });
        });
    });
})();
