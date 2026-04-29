using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class PlayerInventoryTests
{
	private GameObject coinPrefab;

	private GameObject player;
	private PlayerInventory inventory;
	private CharacterController controller;
	private ResourceType coinType;
	private MockInput mockInput;
	private PlayerTestHelper playerHelper;
	private Camera cam;

	private float coinRadius;

	[UnitySetUp]
	public IEnumerator Setup()
	{
		AsyncOperation op = SceneManager.LoadSceneAsync("Scenes/TestScenes/DefaultTestScene");

		while (!op.isDone)
		{
			yield return null;
		}

		coinPrefab = Resources.Load<GameObject>("Prefabs/Loot/Coin");
		var lootComponent = coinPrefab.GetComponent<CollectibleLoot>();
		Assert.IsNotNull(lootComponent, "The prefab does not have the CollectibleLoot script!");
		coinType = lootComponent.lootType;

		Collider coinCollider = coinPrefab.GetComponentInChildren<Collider>();
		Assert.IsNotNull(coinCollider, "The coin prefab does not have a Collider!");
		coinRadius = coinCollider.bounds.extents.magnitude; 

		player = GameObject.FindGameObjectWithTag("Player");
		inventory = player.GetComponent<PlayerInventory>();
		controller = player.GetComponent<CharacterController>();

		player.transform.position = Vector3.zero;

		cam = Camera.main;
		playerHelper = new PlayerTestHelper(cam, player);

		mockInput = new MockInput();
		InputInjector.ApplyGlobally(mockInput);

		yield return null;
	}

	[UnityTearDown]
	public IEnumerator Teardown()
	{
		var spawnedObjects = GameObject.FindObjectsByType<CollectibleLoot>(FindObjectsSortMode.None);
		foreach (var obj in spawnedObjects)
		{
			Object.DestroyImmediate(obj.gameObject);
		}

		inventory.ResetInventory(); 
		yield return null;
	}

	[UnityTest]
	public IEnumerator PlayerCollectCoin_SingleItem()
	{
		var coinAmount = 1;

		Vector3 spawnPos = player.transform.position + new Vector3(0, 0, 3);
		GameObject spawnedCoin = SpawnLoot(coinPrefab, spawnPos, coinAmount);

		yield return new WaitForSeconds(0.5f);

		float timeout = 0;
		while (timeout < 16f) 
		{
			playerHelper.MoveTowards(mockInput, spawnPos);
			if (spawnedCoin == null) break;

			timeout += Time.deltaTime;
			yield return null;
		}
		Assert.IsTrue(spawnedCoin == null, "The coin should have been destroyed!");
		Assert.AreEqual(inventory.GetAmount(coinType), coinAmount, "The inventory should contain the collected resource!");
		Object.Destroy(spawnedCoin);
	}


	[UnityTest]
	public IEnumerator PlayerCollectCoin_MultipleItems()
	{
		int numberOfCoins = 10;
		int amountPerCoin = 5; 
		Vector3 spawnPos = player.transform.position;

		int totalExpected = numberOfCoins * amountPerCoin;
		Vector3 lastPos = spawnPos;

		for (int i = 0; i < numberOfCoins; i++)
		{
			lastPos = spawnPos + new Vector3(0, 0, 2+i);
			SpawnLoot(coinPrefab, lastPos, amountPerCoin);
		}

		float timeout = 0;
		while (timeout < 16f)
		{
			playerHelper.MoveTowards(mockInput, lastPos);

			var remainingCoins = GameObject.FindObjectsByType<CollectibleLoot>(FindObjectsSortMode.None);
			if (remainingCoins.Length == 0) break;

			timeout += Time.deltaTime;
			yield return null;
		}

		int actualCoins = inventory.GetAmount(coinType);
		Assert.AreEqual(totalExpected, actualCoins, $"The inventory should have had {totalExpected}, but has {actualCoins}!");
	}

	[UnityTest]
	public IEnumerator PlayerCollectCoin_RespectsPickupRadius()
	{
		var collectorComponent = player.GetComponent<LootCollector>();
		Assert.IsNotNull(collectorComponent, "The player does not have the LootCollector component!");

		int insideCount = 5;
		int outsideCount = 5;
		int amountPerCoin = 1;

		yield return new WaitForSeconds(0.5f);

		Vector3 center = collectorComponent.transform.position + collectorComponent.pickupOffset;
		float borderDistance = collectorComponent.pickupRadius + coinRadius;

		SpawnCoinInCircle(center, insideCount, amountPerCoin, borderDistance);
		SpawnCoinInCircle(center, outsideCount, amountPerCoin, borderDistance + 0.1f);

		yield return new WaitForSeconds(1f);

		int collected = inventory.GetAmount(coinType);

		var remaining = GameObject.FindObjectsByType<CollectibleLoot>(FindObjectsSortMode.None);

		Assert.AreEqual(insideCount * amountPerCoin, collected,
				"The coins inside the radius were not collected correctly!");

		Assert.AreEqual(outsideCount * amountPerCoin, remaining.Length,
				"The coins outside the radius should not have been collected!");
	}

	private GameObject SpawnLoot(GameObject prefab, Vector3 spawnPosition, int amount)
	{
		GameObject lootObj = GameObject.Instantiate(prefab, spawnPosition, Quaternion.identity);

		var lootScript = lootObj.GetComponent<CollectibleLoot>();
		lootScript.quantity = amount;
		var resourceType = lootScript.lootType;
		return lootObj;
	}

	private void SpawnCoinInCircle(Vector3 center, int qty, int amountPerCoin, float distance)
	{
		for (int i = 0; i < qty; i++)
		{
			float angle = i * Mathf.PI * 2 / qty;

			Vector3 pos = center + new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * distance;
			SpawnLoot(coinPrefab, pos, amountPerCoin);
		}
	}
}
