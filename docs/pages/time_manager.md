# TimeManager.cs Documentation

The `TimeManager` component is a Unity system designed to manage a dynamic day-night cycle. It synchronizes in-game time (minutes, hours, and days) with environmental visuals, specifically the Sun's rotation and Skybox properties.

---

## Configuration
To use this script, attach it to a GameObject and assign the following in the Inspector:

* **Time Step:** Duration (in real-world seconds) for one in-game minute.
* **Directional Light:** The main light source representing the Sun.
* **Exposure Curve:** An AnimationCurve (0 to 1 on X-axis) controlling Skybox brightness.
* **Light Color Gradient:** A Gradient (0 to 1 on X-axis) defining the light and sky color throughout the day.

---

## Technical Logic

### Variables and Parameters

* `private int minutes`: The internal counter for minutes.
* `private int hours`: Current in-game hour (0-23).
* `private int days`: Total days elapsed.
* `private float timeSeconds`: Tracks the real-time seconds passed toward the next `timeStep`.
* `private Material skyboxMaterial`: Reference to the active skybox material retrieved at start.
* `private bool testMode`: used to enable debug console in manual tests. 


### Time Progression

The script uses a custom timer to increment minutes:

* **Seconds to Minutes:** When `timeSeconds` exceeds `timeStep`, a minute is added and `timeSeconds` resets.
* **Overflow Handling:** The `HandleTimeOverflow()` method ensures that 60 minutes wrap to 1 hour, and 24 hours wrap to 1 day.

### Visual Synchronization
The environment is updated every frame inside `UpdateEnvironment()`:

* **Skybox Tint:** Updated via the `_Tint` property using the `lightColorGradient`.
* **Skybox Exposure:** Updated via the `_Exposure` property using the `exposureCurve`.
* **Skybox Rotation:** Slightly rotates the sky texture over time using the `_Rotation` property.

### Optimization
To maintain performance while using real-time global illumination:

* **DynamicGI.UpdateEnvironment():** This expensive call is throttled to run only once every **20 frames**, refreshing indirect lighting without overloading the GPU.

---

## Key Methods

| Method | Description |
| :--- | :--- |
| `HandleTimeOverflow()` | Manages the logic for resetting minutes and hours when they reach their limit. |
| `UpdateEnvironment()` | Calculates the current time progression (0 to 1) and applies it to the light and skybox. It also calls `DynamicGI.UpdateEnvironment()` to smooth transitions. |
|  `OnValidate()` | Overwrites the unity native function to validate inspector inputs when testing the component. |
| `Minutes/Hours/Days`| Wrappers for correct management of values overflow. |

## Notes

* **Material Persistence:** This script modifies the `RenderSettings.skybox` material directly. Changes made during Play Mode may persist in the Project files.
* **Shader Compatibility:** Requires a shader that supports `_Tint`, `_Exposure`, and `_Rotation` (e.g., Unity's built-in *Skybox/Procedural*).