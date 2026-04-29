# Manual Test Plan: TimeManager.cs

This document outlines the manual verification procedures to ensure the `TimeManager` system is correctly integrated, visually functional, and robust against manual or external modifications.

**Test Scene:** `Assets\Scenes\TestScenes\TimeManager`.

---

## 1. Inspector Interaction (Editor-Time)
**Goal:** Confirm that the validation logic triggers instantly even without entering Play Mode.
Select the `TimeManager` game object in the Hierarchy and locate the component in the Inspector.

* **Value Clamping:** Manually type `75` into the **Minutes** field. Upon pressing Enter, it should automatically clamp to `59`.
* **Negative Values:** Manually type `-5` into the **Minutes** field. Upon pressing Enter, it should clamp to `0`.
* **Range Constraints:** Verify that the `[Range]` sliders for **Hours** (0-23) and **Minutes** (0-59) prevent accidental invalid inputs.

> **Note:** In Editor mode (outside of Play Mode), changes to parameters will not provide immediate visual feedback in the scene. To observe visual updates, you must enter **Play Mode**.

---

## 2. Environmental Consistency (Runtime)
**Goal:** Validate the visual "look and feel" during gameplay. In the Inspector, ensure the **Test Mode** parameter is enabled to read feedback in the console. 

* **Night (00:00):** Ambient lighting must match the cool/dark tones defined at the start and end of the color gradient.
* **Noon (12:00):** The Skybox `_Exposure` must reach its peak value as defined by the `AnimationCurve`.
* **Sunset/Sunrise:** Light color should transition smoothly through the gradient keys during morning and evening transitions.