using System.Collections.Generic;

public class Inventory 
{
	private Dictionary<ResourceType, ResourceDetail> inventory = new Dictionary<ResourceType, ResourceDetail>();

	public int AddResource(ResourceType resType, int amount)
	{
		if (amount <= 0) return 0;

		if (inventory.TryGetValue(resType, out ResourceDetail detail))
		{
			return detail.ChangeAmountBy(amount);
		}
		else
		{
			detail = new ResourceDetail(amount, resType.defaultMaxAmount);
			inventory.Add(resType, detail);
			return detail.CurrentAmount;
		}
	}

	public int GetResourceAmount(ResourceType resType)
	{
		if (inventory.TryGetValue(resType, out ResourceDetail detail))
		{
			return detail.CurrentAmount;

		}
		return 0;
	}

	public void Clear()
	{
		inventory.Clear();
	}
}
