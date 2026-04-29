using UnityEngine;

public class CollectibleLoot : MonoBehaviour
{
	public ResourceType lootType;
	public int quantity = 1;

	public void Collect()
	{
		Destroy(gameObject);
	}
}
