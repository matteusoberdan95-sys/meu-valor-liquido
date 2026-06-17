(() => {
  const article = document.querySelector("[data-blog-article]");
  const bar = document.querySelector("[data-blog-article-progress]");
  if (!article || !bar) {
    return;
  }

  const update = () => {
    const rect = article.getBoundingClientRect();
    const total = article.offsetHeight - window.innerHeight;
    if (total <= 0) {
      bar.style.width = "0%";
      return;
    }

    const scrolled = Math.min(Math.max(-rect.top, 0), total);
    bar.style.width = `${(scrolled / total) * 100}%`;
  };

  window.addEventListener("scroll", update, { passive: true });
  window.addEventListener("resize", update);
  update();
})();
