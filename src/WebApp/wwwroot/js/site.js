document.addEventListener("DOMContentLoaded", () => {
  const toggle = document.querySelector("[data-nav-toggle]");
  const mobileNav = document.querySelector("[data-nav-mobile]");

  if (toggle && mobileNav) {
    toggle.addEventListener("click", () => {
      const isOpen = mobileNav.classList.toggle("open");
      toggle.setAttribute("aria-expanded", isOpen ? "true" : "false");
    });
  }
});
