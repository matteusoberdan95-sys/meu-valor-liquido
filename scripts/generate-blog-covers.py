"""Compat: redireciona para generate-blog-images.py render."""
from __future__ import annotations

import subprocess
import sys
from pathlib import Path

if __name__ == "__main__":
    script = Path(__file__).with_name("generate-blog-images.py")
    raise SystemExit(subprocess.call([sys.executable, str(script), "render", *sys.argv[1:]]))
