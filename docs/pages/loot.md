# Looting System
The system features a *Source*, which is the entity responsible for generating loot, and a *Collector*, which is the entity that can pick up the drop.

A source can generate multiple independent loots at the same time (e.g., 100% coin probability and 10% medical kit probability).

## Architecture and Components

### LootDrop
This data structure represents the loot configuration: it allows defining which resource (`ResourceType`) can be released by a source, in what quantity (min-max range), and the drop probability.

### LootDropper
A component to be assigned to the source that can drop resources (enemies, mines, etc.).

The `DropLoot` method iterates over a list of `LootDrop` and, for each element, instantiates the relative prefab in the game space according to the `LootDrop` configuration.

The `dropOffset` vector allows for adjusting the spawn point relative to the center of the object.

### CollectibleLoot
Represents the physical object present in the scene. It holds the resource data (type and quantity) until it is collected.

It is created by the `LootDropper`.

### LootCollector
Manages the automatic collection of resources through a proximity area.

Inside the `FixedUpdate` method:

1. It casts a sphere with radius `pickupRadius` using the Unity function `Physics.OverlapSphereNonAlloc`.
2. It determines all objects in the game scene belonging to the `Loot` layer that collide with the sphere.
3. Each `CollectibleLoot` object is added to the inventory and removed from the game scene.

The standard version of `OverlapSphere` allocates a new array every frame; the NonAlloc approach reuses a pre-allocated buffer, ensuring more stable performance.

---

## Usage Guide

### Configuring a GameObject for dropping
1. Add the `LootDropper` component to the `GameObject` implementing the *Source*.
2. From the Unity Inspector, populate the `Loot Drops` array to define which resources the object can drop and with what probability.
3. Call the `DropLoot` function at the point in the code where you want to drop the loot.

### Configuring a GameObject for collecting drops
1. Add the `LootCollector` component to the `GameObject` implementing the *Collector*.
2. Assign the `Loot Layer` to the layer that has been assigned to the loot prefabs.
3. Modify the `Pickup Radius` to increase or decrease the drop capture range. In the **Scene View**, the collection radius is visible via a Gizmo.
