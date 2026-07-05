(() => {
  const loadStyle = (id, href) => {
    if (document.getElementById(id)) {
      return;
    }

    const link = document.createElement("link");
    link.id = id;
    link.rel = "stylesheet";
    link.href = href;
    document.head.appendChild(link);
  };

  const loadFonts = () => {
    loadStyle(
      "mvl-google-fonts",
      "https://fonts.googleapis.com/css2?family=Plus+Jakarta+Sans:wght@600;700;800&family=Inter:wght@400;500;600;700;800&display=swap",
    );
    loadStyle(
      "mvl-material-symbols",
      "https://fonts.googleapis.com/css2?family=Material+Symbols+Outlined:opsz,wght,FILL,GRAD@24,300,0,0&display=swap",
    );
  };

  if ("requestIdleCallback" in window) {
    window.requestIdleCallback(loadFonts, { timeout: 1200 });
    return;
  }

  window.setTimeout(loadFonts, 700);
})();
