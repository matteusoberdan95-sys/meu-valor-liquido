import re
from pathlib import Path
p = Path("stitch_redesign_meu_valor_l_quido/home_meu_valor_l_quido_mobile_1/code.html")
text = p.read_text(encoding="utf-8", errors="replace")
text = re.sub(r"data:image/[^\"']+", "data:image/...", text)
for i, line in enumerate(text.splitlines()[160:280], start=161):
    if len(line) > 180:
        line = line[:180] + "..."
    print(f"{i}: {line}")
