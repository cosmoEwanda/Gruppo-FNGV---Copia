# Manual Test Plan: Mannequin & Respawn System

This document outlines the manual verification steps for the `Mannequin` component and its dedicated `RespawnManager`. The system ensures that the Mannequin actor is correctly tracked, damaged, and replaced following its destruction.

**Test Scene:** `Assets\Scenes\TestScenes\Mannequin`

---

## 1. Scene Overview & Setup
**Goal:** Confirm the test environment is correctly configured prior to simulation.

* **Hierarchy Check:** Verify that both the `Respawner` (containing the `RespawnManager`) and `Tester` game objects are present.
* **Initialization:** Upon entering **Play Mode**, a Mannequin prefab instance must be automatically spawned at the designated location.
* **Visual Feedback:** Confirm the presence of the UI lifebar and the damage-flash component. These are auxiliary visual tools for the tester and should not impact core respawn logic.

---

## 2. Functional Requirements & Constraints
**Goal:** Validate the specialized behavior and dependencies of the system.

* **Direct Dependency:** The `RespawnManager` is **strictly dependent** on the Mannequin prefab. Verify that the manager correctly tracks this specific reference throughout its lifecycle.
* **Lifecycle Loop:** 1.  The `Tester` object applies damage until the Mannequin is destroyed.
    2.  Upon destruction, the `RespawnManager` must immediately trigger its cooldown sequence.
    3.  After the timer elapses, a new Mannequin instance must be re-instantiated.
* **State Persistence:** Ensure the system prevents "double spawning" if multiple destruction events are triggered in rapid succession.
* **Coherence Logic (Feedback):** 

    * **Healing:** When the Mannequin flashes **green**, the health bar width must increase (up to its maximum limit).
    * **Damage:** When the Mannequin flashes **red**, the health bar width must decrease. 

> *Note:* The transition to 0 health may result in immediate despawning, making the final empty state of the bar brief or invisible.

---

## 3. Dynamic Parameter Testing (Runtime)
**Goal:** Verify that the timing logic is responsive to real-time changes in the Inspector.

* **Respawn Time Calibration:** Locate the **Respawn Time** field in the `Respawner` component.
* **Runtime Modification:** Change the value during active gameplay to test logic robustness:
    * **Short Delay (e.g., 1s):** Confirm the Mannequin reappears almost immediately after death.
    * **Long Delay (e.g., 5s+):** Confirm the scene remains empty for the full duration of the timer before the new instance appears.