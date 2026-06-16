(() => {
  const collect = (eventName, dimension) => {
    if (!eventName) {
      return;
    }

    fetch("/api/metrics/collect", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ event: eventName, dimension: dimension ?? null }),
      keepalive: true,
    }).catch(() => {});
  };

  window.MvlMetrics = { collect };
})();
