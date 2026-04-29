# Player System

To change how the player receives commands, use `SetInputProvider()`.

## Movement 
Character movement is based on the `CharacterController` component instead of the `Rigidbody` physics system.

Since the character does not automatically react to external physical forces, any "knockback" effects must be implemented via script by manipulating the velocity vector.

### Movement Logic
Movement is implemented in the `PlayerMovement.cs` script and uses an **absolute Top-Down** approach: controls are mapped to global world coordinates, making movement independent of camera orientation.

Since the `CharacterController` is used, character movement must pass **exclusively** through the `controller.Move()` method.

The system queries the assigned input provider (currently `KeyboardInput`) to obtain the horizontal and vertical axis values. Using absolute directions ensures that the `W` key always moves the player toward the "North" of the map.

#### Gravity Management
Because the `Character Controller` does not apply gravity automatically, a constant manual gravity force is calculated to ensure the character remains in contact with the ground.

### Rotation
The character automatically rotates to look toward the mouse cursor position:

1.  A ray is projected from the camera (`ScreenPointToRay`) toward the cursor.
2.  The system calculates the intersection between the ray and a virtual horizontal plane positioned at the player's height.
3.  The character performs a rotation toward the intersection point.

**Important**:
The rotation calculation occurs exclusively on the horizontal axis (X and Z planes). The Y component of the direction vector is forced to 0. Consequently, it is not possible to aim up or down (along the Y axis). Aiming is strictly two-dimensional relative to the map plane.

## Attack
The attack is implemented in the `Warrior.cs` script and it manages the melee combat logic for a player character.
It handles input detection, animation state synchronization, and physics-based hit detection using Raycasts.

### Overview
`Warrior.cs` is responsible for:

- Detecting attack input and managing cooldowns.

- Synchronizing movement and combat states with the Animator.

- Performing forward Raycasting to detect and damage enemies.

- Applying knockback forces to targets.

### Inspector Parameters

some parameters can be modified in the inspector menu thanks to the following **public** variables:

|Variable|	Type|	Description|
|--------|------|--------------|
|`atkRange`|	float|	The length of the attack ray (reach)|
|`dmg`|	float|	The amount of damage dealt per hit|
|`atkRate`|	float|	The time interval (in seconds) between attacks|
|`knockBack`|	float|	The force applied to the hit target|

### Attack Logic
Every frame is monitored to check if the character is moving (via `Horizontal` and `Vertical` axes) and updates the `stationary` parameter in the `Animator` component. If the attack button (`Fire1`) is pressed and the cooldown timer has elapsed, it triggers the attack sequence.

### Core Methods

`Attack()`

Handles the physics-based hit calculation:

- Generates a Ray starting from the warrior's position with a vertical offset (`atkOrigin`).

- If the ray hits a collider within `atkRange`:

    - It attempts to find the `HealthSystem` component to apply damage.

    - It checks for the `IKnockback` interface to apply directional force.

`AttackAnimation(bool isMoving)`

Manages complex Animator trigger logic:

- **Stationary:** Picks a random integer between 0 and 2 (`atkIndex`) to vary the attack animation.

- **Idle State:** If the animator is in an "Idle" or "Empty" state, it fires the `atkTrigger`.

- **Animation Canceling/Chaining:** If an animation is already playing, it uses the `pressed` trigger to handle rapid transitions or combos.

### Debugging

Since the ray cast is normally invisible, The component uses `OnDrawGizmos()` to provide a visual representation of the attack range within the Unity Scene View.

**tip "Scene View Debugging":**
A red ray is drawn in front of the character. This represents the exact reach and direction of the attack. If an enemy is not taking damage, ensure this red line is physically intersecting the enemy's Collider.

### Notes
**Ray Origin:** The attack originates at a fixed height of `0.5f` units above the transform base to prevent the ray from hitting the floor.

**Interfaces:** The knockback system requires targets to implement the `IKnockback` interface.

**External Systems:** Damage is sent to a component named `HealthSystem`.

---
