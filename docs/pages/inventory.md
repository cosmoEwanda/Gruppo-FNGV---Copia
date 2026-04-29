# Inventory System
Gameplay requirements are limited to resource and consumable accumulation and tracking mission items. For this reason, the inventory system does not support multiple items of the same type with different statistics.

## Components
The core of the inventory is composed of four main components:

* `ResourceType`: Defines the identity of a resource (name, description, maximum quantity, etc.). It is used as the key for the inventory system. This component is a *ScriptableObject*.
* `ResourceDetail`: Represents the state of a resource currently in the inventory.
* `Inventory`: Implements the inventory using a dictionary that allows access to resources via the `ResourceType`.
* `PlayerInventory`: This is the only `MonoBehaviour` component and acts as a bridge between Unity `GameObjects` and the `Inventory`. In this way, the inventory management system is separated from Unity's object lifecycle, which also facilitates testing.

To add resources:
```csharp
// add 10 units of 'coin'
int addedQuantity = playerInventory.Add(coinType, 10);
```

## Usage Guide
### Assigning the inventory to the character in Unity
To assign the inventory, simply add the `PlayerInventory` component to the GameObject representing the character.

Currently, there are no features to define a starting inventory directly from the editor.

### Adding a new resource from the Unity Editor
1. Navigate to the folder `Assets/Resources/ScriptableObjects/GameResources`.
2. Right-click -> Create -> Resource -> resourceType.
3. Select the newly created *ScriptableObject* to modify the necessary properties in the Inspector.
