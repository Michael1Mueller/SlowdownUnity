# Point-and-Click Study (SlowdownUnity)

Dieses Repository enthält eine angepasste Version der Point-and-Click-Aufgabe, die im Rahmen meiner Masterarbeit zur Untersuchung des **Slowdown-Effekts** bei Systemlatenzen verwendet wurde.

## Zweck der Software
Die Software dient als experimentelle Umgebung zur Messung von Mausbewegungen und Klicks unter verschiedenen Zielverzögerungen. Die Teilnehmenden müssen ein mittleres Startziel sowie anschließend erscheinende seitliche Ziele abschießen. 

### Wichtigste Anpassung in dieser Version:
* **Startbereich-Restriktion (Aura):** Nach dem Treffer des mittleren Targets muss das Fadenkreuz für 400 ms in einem markierten Bereich verbleiben. Ein vorzeitiges Verlassen setzt den Timer zurück. Dies verhindert eine frühzeitige Richtungsentscheidung.
* **Ausführlichere Datenaufnahme**

## Herkunft & Referenz
Dieses Projekt ist eine Modifikation und baut direkt auf der Arbeit von **Hößl (2025)** auf. 

* **Vorgänger-Repository:** [MA_Slowdown_Effect (Hößl)](https://github.com/Rosti97/MA_Slowdown_Effect)
* **Theoretische Grundlage:** Die Studie untersucht den Effekt von künstlicher Verzögerung am Zielende auf die motorische Planung (Slowdown-Effekt), angelehnt an *Hößl (2025)* und *Bogon et al. (2025)*.
