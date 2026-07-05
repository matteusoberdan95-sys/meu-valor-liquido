(() => {
  const loaded = new Set();

  document.querySelectorAll("link[data-deferred-stylesheet]").forEach((preload) => {
    const href = preload.getAttribute("href");
    if (!href || loaded.has(href) || document.querySelector(`link[rel="stylesheet"][href="${href}"]`)) {
      return;
    }

    loaded.add(href);
    const stylesheet = document.createElement("link");
    stylesheet.rel = "stylesheet";
    stylesheet.href = href;
    document.head.appendChild(stylesheet);
  });
})();
