using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using NUnit.Framework;
using System.Reflection;
using System.IO;


public class AtkEntity
{
	private GameObject player;
	private GameObject enemy;
	private Warrior warrior;
	private HealthSystem health;
	private MockInput mockInput;
	private Camera cam;

	[UnitySetUp]
	public IEnumerator Setup()
	{
		yield return SceneManager.LoadSceneAsync("AttackEntity");
		yield return null; 

		player = GameObject.FindWithTag("Player");
		enemy = GameObject.FindWithTag("Enemy");
		cam = Camera.main;

		Assert.IsNotNull(player, "Player not found!");
		Assert.IsNotNull(enemy, "Enemy not found!");
		Assert.IsNotNull(cam, "Main Camera not found!");

		health = enemy.GetComponent<HealthSystem>();

		warrior = player.GetComponent<Warrior>();
		mockInput = new MockInput();
		mockInput.LookAtDirection = cam.WorldToScreenPoint(enemy.transform.position);

		InputInjector.ApplyGlobally(mockInput);
		yield return null;
	}

	[UnityTearDown]
	public IEnumerator Teardown()
	{
		mockInput.ButtonStates["Fire1"] = false;
		yield return null;
	}

	[UnityTest]
    public IEnumerator DamageEntityTest()
	{
		
		float initHealth = health.GetCurrentHealth();
		yield return null;

		float initialDistance = Vector3.Distance(player.transform.position, enemy.transform.position);
		Debug.Log($"initial distance: {initialDistance}");
		float atkRange = warrior.atkRange;
		Assert.Greater(atkRange, initialDistance, "enemy and player are too far apart at the beginning of the test");

		mockInput.ButtonStates["Fire1"] = true;

		yield return new WaitForSeconds(1f);

		Assert.Greater(initHealth, health.GetCurrentHealth(), "no entity damage taken");
	}

	[UnityTest]
	public IEnumerator KnockBackEntityTest()
	{
		float initialDistance = Vector3.Distance(player.transform.position, enemy.transform.position);
		Debug.Log($"initial distance: {initialDistance}");
		float atkRange = warrior.atkRange;
		Assert.Greater(atkRange, initialDistance, "enemy and player are too far apart at the beginning of the test");

		mockInput.ButtonStates["Fire1"] = true;

		yield return new WaitForSeconds(1f);

		float finalDistance = Vector3.Distance(player.transform.position, enemy.transform.position);
		Assert.Greater(finalDistance, initialDistance, "attack did no knockback");
	}
}
