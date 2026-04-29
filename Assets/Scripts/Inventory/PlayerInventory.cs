using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
	private Inventory inventory;

    void Awake()
    {
		inventory = new Inventory();
    }

	public void ResetInventory() => inventory.Clear();

    public void Add(ResourceType resType, int amount) => inventory.AddResource(resType, amount);

	public int GetAmount(ResourceType resType) => inventory.GetResourceAmount(resType);
}
