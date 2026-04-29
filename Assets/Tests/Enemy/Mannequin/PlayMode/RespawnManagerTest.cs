using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System;

[TestFixture]
public class RespawnManagerTest
{
	RespawnManager rm;
	Mannequin prefab;

	[SetUp]
	public void SetUp()
	{
		var go = new GameObject();
		rm = go.AddComponent<RespawnManager>();
		Assert.IsNotNull(rm);

		this.prefab = Resources.Load<Mannequin>("Prefabs/Enemies/Mannequin");
		Assert.IsNotNull(prefab, "Mannequin not found in Prefabs/Enemies/Mannequin");
		var spawnPoint = new Vector3(0f, 0f, 0f);

		rm.enemyPrefab = prefab;
		rm.spawnPoint = spawnPoint;
		rm.respawnTime = 2f;
	}
	
	[TearDown]
	public void TearDown()
	{
		UnityEngine.Object.DestroyImmediate(rm.gameObject);

	}

	[UnityTest]
	public IEnumerator OnEnemySpawned_EventIsTriggered()
	{
		bool eventFired = false;
		Mannequin spawnedRef = null;

		Action<Mannequin> handler = (m) => {
			eventFired = true;
			spawnedRef = m;
		};
		RespawnManager.OnEnemySpawned += handler;

		rm.SpawnNow();
		yield return null;

		Assert.IsTrue(eventFired, "OnEnemySpawned event not invoked!");
		Assert.IsNotNull(spawnedRef, "Prefab reference is null!");

		RespawnManager.OnEnemySpawned -= handler;
	}
}
