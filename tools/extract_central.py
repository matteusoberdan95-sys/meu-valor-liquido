import re
from pathlib import Path
p = Path("stitch_redesign_meu_valor_l_quido/central_de_calculadoras_meu_valor_l_quido/code.html")
text = p.read_text(encoding="utf-8", errors="replace")
text = re.sub(r"https://lh3\.googleusercontent\.com/\S+", "IMG_URL", text)
text = re.sub(r"data:image/\S+", "data:image/...", text)
for i, line in enumerate(text.splitlines()[90:250], start=91):
    if len(line) > 200:
        line = line[:200] + "..."
    print(f"{i}: {line}")
