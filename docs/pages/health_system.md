# HealthSystem.cs Documentation

The `HealthSystem` component is a modular Unity script used to manage health, damage, and healing for any entity (players, enemies, or destructible objects). It utilizes C# Events to decouple game logic from visual feedback (like UI bars or death animations).

---

## Configuration
To use this script, attach it to a GameObject. You can set the initial health value directly in the Inspector.

* **Health (SerializeField):** The starting health value assigned during the Awake phase.

---

## Technical Logic

### Variables and Parameters

* `private float health`: The serialized field for the initial health value.
* `public float MaxHealth`: The maximum amount of hitpoints the object can have.
* `public float CurrentHealth`: The actual amount of hitpoints of the object.
* `private bool isDead`: Whether the cumulative damage taken reached `MaxHealth` or not.
* `public event Action<float> OnTakeDamage`: Fired when the gameObject takes damage.
* `public event Action<float> OnTakeHeal`: Fired when the gameObject receives healing.
* `public event Action OnDeath`: Fired when the health reaches zero.

### Core Actions
The script handles three main state changes:

* **TakeDamage(amount):** Subtracts health. If health reaches 0, it sets `isDead` to true and triggers the death event. It prevents health from dropping below zero using `Math.Max`.
* **TakeHeal(amount):** Adds health but caps it at `MaxHealth` using `Math.Min`. It cannot heal an entity that is already dead.
* **SetHealth(nHealth):** A wrapper that determines whether the change is damage or healing and calls the respective method to ensure events are fired correctly.

### Events (Actions)
This system uses `System.Action` to notify other scripts when something happens without needing direct references:

* **OnTakeDamage(float):** Fired when health decreases. Passes the damage amount.
* **OnTakeHeal(float):** Fired when health increases. Passes the heal amount.
* **OnDeath():** Fired exactly once when health hits zero.

---

## Key Methods

| Method | Description |
| :--- | :--- |
| `Initialize(float initial)` | Resets health and max health. Useful for pooling or respawning. |
| `SetHealth(float nHealth)` | Force health to a specific value, automatically calculating if it's damage or healing. |
| `IsDead()` | Returns true if the entity's health is 0 or less. |
| `GetCurrentHealth()` | Returns the current health value. |
| `TakeDamage(float amount)` | Decrease the `CurrentHealth` accordingly to the damage taken |
| `TakeHeal(float amount)` | Increase the `CurrentHealth` accordingly to the heal received |