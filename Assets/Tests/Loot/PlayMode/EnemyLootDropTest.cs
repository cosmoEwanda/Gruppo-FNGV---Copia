using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class EnemyLootDropTest
{
	private GameObject enemyPrefab;

	[UnitySetUp]
	public IEnumerator Setup()
	{
		enemyPrefab = Resources.Load<GameObject>("Prefabs/Enemies/EnemyBase");
		Assert.IsNotNull(enemyPrefab, "EnemyBase not found in Resources/Prefabs/Enemies/EnemyBase!");
		yield return null;
	}

	[UnityTearDown]
	public IEnumerator Teardown()
	{
		var enemies = Object.FindObjectsByType<LootDropper>(FindObjectsSortMode.None);
		foreach (var e in enemies)
		{
			Object.DestroyImmediate(e.gameObject);
		}

		var loot = Object.FindObjectsByType<CollectibleLoot>(FindObjectsSortMode.None);
		foreach (var l in loot)
		{
			Object.DestroyImmediate(l.gameObject);
		}
		yield return null;
	}

	[UnityTest]
	public IEnumerator EnemyDropsLoot_100Chance()
	{
		GameObject enemy = Object.Instantiate(enemyPrefab);

		if (enemy.TryGetComponent<Animator>(out var anim)) anim.enabled = false;
		if (enemy.TryGetComponent<UnityEngine.AI.NavMeshAgent>(out var agent)) agent.enabled = false;

		var dropper = enemy.GetComponent<LootDropper>();

		foreach(var drop in dropper.lootDrops)
		{
			drop.dropChance = 1.0f;
			drop.minAmount = 1;
			drop.maxAmount = 1;
		}

		dropper.DropLoot();
		var spawnedItems = Object.FindObjectsByType<CollectibleLoot>(FindObjectsSortMode.None);

		Assert.AreEqual(spawnedItems.Length, 1, "The enemy did not drop anything!");
		yield return null;
	}
}
