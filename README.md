**Skyward Artifacts** is a Unity 3D FPS that blends arcade shooting mechanics with educational collection elements. Players traverse levels defined by XML data, shooting down hot air balloons to recover lost historical artifacts. The goal is to collect every artifact across all levels while maintaining a high accuracy score.

---

## 🎮 Gameplay Overview

### The Mission
You are an artifact hunter. Your targets are reinforced hot air balloons carrying crates containing valuable historical items.
* **Shoot the Balloon:** Deals damage and based on the balloon hitpoints it falls. Destroying the balloon drops a **Reward Crate**.
* **Avoid the Basket:** The create under the balloon is fragile! Hitting it results in a **-5 Point Penalty**.
* **Collect the Artifact:** Open the fallen crates to retrieve items with unique description.

### 🔫 Weaponry
Switch continuously to adapt to different balloon distances and health pools.
1. **Pistol:** Reliable mid-range sidearm.
2. **Sniper Rifle:** High damage, low rate of fire. Perfect for distant balloons.
3. **Shotgun:** High spread. risky to use near the baskets, but effective at close range.

### 💯 Scoring System
Precision is key. Your accuracy score is saved locally upon game completion.
* **Hit Balloon:** +1 Point
* **Miss Shot:** -1 Point
* **Hit Basket (Penalty):** -5 Points

---

## 📦 Systems & Architecture

## 1. Data-Driven Levels (XML)
The game utilizes an external XML parsing and XSD validation to load level configurations. This allows for easy modification of level difficulty without recompiling the code.

```xml
<Level id="1">
    <balloon hitpoints="3">
    <item>Coin 1</item>
    <description>Coin 1 description</description>
    <icon>Coin</icon>
    <trajectory
        startX="20" startY="30"
        endX="60" endY="0"
        speed="6"
        type="linear"/>
    </balloon>
</Level>
```

## 2. Inventory & Metadata
The inventory system is designed to handle metadata logic rather than complex visual interactions.
* **Static Representation:** Items are represented as static sprites within the inventory UI.
* **Data Layer:** Despite the visual simplicity, every item object holds specific description loaded from the source files. The item description is displayed as a tooltip on mouse hover.
* **Progression:** The game loop checks the player's inventory against the level requirements. You only proceed to the next level once all required game objects are collected.

## 🕹️ Controls

| Key | Action |
| :--- | :--- |
| **W, A, S, D** | Movement |
| **Space** | Jump |
| **Mouse** | Look / Aim |
| **Left Mouse Click** | Shoot |
| **Right Mouse Click** | Sniper scope |
| **1 / 2 / 3** | Switch Weapons |
| **E** | Interact (Open Crate / Pick up Item) |
| **Tab** | Toggle Inventory |

---

## 🚀 Installation & Setup

1. **Clone the repository:**
   ```bash
   git clone [https://github.com/yourusername/skyward-artifacts.git](https://github.com/yourusername/skyward-artifacts.git)
2. **Modify the game.xml file as desired.**
2. **Play in the Unity editor or build the game.**

## 📝 Credits
* **Developers:** Radin Tiholov, Vladimir Kotsev
* **Course:** Xml @ FMI Sofia University
* **Engine:** Unity 3D (6000.2.10f1)
* **Technology** C#
