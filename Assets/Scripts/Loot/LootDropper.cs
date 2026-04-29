using UnityEngine;

public class LootDropper : MonoBehaviour
{
	public LootDrop[] lootDrops;
	public Vector3 dropOffset;

	[ContextMenu("Drop Loot")]
	public void DropLoot()
	{
		foreach(LootDrop drop in lootDrops)
		{
			if (Random.value <= drop.dropChance)
			{
				int dropAmount = Random.Range(drop.minAmount, drop.maxAmount + 1);

				Vector3 spawnPos = transform.position + dropOffset;
				GameObject lootObj = Instantiate(drop.dropType.prefab, spawnPos,
						Quaternion.identity);

				CollectibleLoot item = lootObj.GetComponent<CollectibleLoot>();
				if (item != null)
				{
					item.lootType = drop.dropType;
					item.quantity = dropAmount;
				}
			}
		}
	}
}
