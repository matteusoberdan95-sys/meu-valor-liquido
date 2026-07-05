(() => {
  const loaded = new Set();

  document.querySelectorAll("link[data-deferred-stylesheet]").forEach((preload) => {
    const href = preload.getAttribute("href");
    if (!href || loaded.has(href)) {
      return;
    }

    if (preload.media && !window.matchMedia(preload.media).matches) {
      return;
    }

    const hasAllMediaStylesheet = Array.from(document.querySelectorAll('link[rel="stylesheet"]'))
      .some((link) => link.getAttribute("href") === href && !link.media);
    if (hasAllMediaStylesheet) {
      return;
    }

    loaded.add(href);
    const stylesheet = document.createElement("link");
    stylesheet.rel = "stylesheet";
    stylesheet.href = href;
    document.head.appendChild(stylesheet);
  });
})();
