using UnityEngine;

[RequireComponent(typeof(PlayerInventory))]
public class LootCollector : MonoBehaviour
{
	private PlayerInventory playerInventory;
	private const int overlapBufferSize = 16;

	[Header("Magnet Settings")]
	public float pickupRadius = 3f;
	public Vector3 pickupOffset;
	public LayerMask lootLayer;

	private Collider[] collidersFound = new Collider[overlapBufferSize];

	private void Awake()
	{
		playerInventory = GetComponent<PlayerInventory>();
	}

	private void FixedUpdate()
	{
		int lootsFound = Physics.OverlapSphereNonAlloc(transform.position + pickupOffset,
				pickupRadius, collidersFound, lootLayer);

		for (int i=0; i < lootsFound; i++)
		{
			if (collidersFound[i].TryGetComponent<CollectibleLoot>(out CollectibleLoot loot))
			{
				playerInventory.Add(loot.lootType, loot.quantity);
				loot.Collect();
			}
		}
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position + pickupOffset, pickupRadius);
	}
}
