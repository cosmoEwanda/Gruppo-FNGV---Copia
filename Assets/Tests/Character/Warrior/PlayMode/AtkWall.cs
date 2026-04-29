using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using NUnit.Framework;
using System.Reflection;


public class AtkWall
{
	private GameObject player;
	private GameObject wall;
	private Warrior warrior;
	private MockInput mockInput;
	private HealthSystem health;
	private Camera cam;

	[UnitySetUp]
	public IEnumerator Setup()
	{
		yield return SceneManager.LoadSceneAsync("AttackWall");
		yield return null;

		player = GameObject.FindWithTag("Player");
		wall = GameObject.FindWithTag("Enemy");
		cam = Camera.main;

		Assert.IsNotNull(player, "Player not found!");
		Assert.IsNotNull(wall, "Enemy not found!");
		Assert.IsNotNull(cam, "Main Camera not found!");

		health = wall.GetComponent<HealthSystem>();

		warrior = player.GetComponent<Warrior>();

		mockInput = new MockInput();
		mockInput.LookAtDirection = cam.WorldToScreenPoint(wall.transform.position);

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
		float initHealth = 0;
		if (health != null) 
		{
			initHealth = health.GetCurrentHealth();
		}

		yield return null;

		float initialDistance = Vector3.Distance(player.transform.position, wall.transform.position);
		Debug.Log($"initial distance: {initialDistance}");
		float atkRange = warrior.atkRange;
		Assert.Greater(atkRange, initialDistance, "wall and player are too far apart at the beginning of the test");

		mockInput.ButtonStates["Fire1"] = true;

		yield return new WaitForSeconds(1f);

		if (health != null)
		{
			Assert.AreEqual(initHealth, health.GetCurrentHealth(), "Wall got damaged");
		}
	}

	[UnityTest]
	public IEnumerator KnockBackEntityTest()
	{
		float initialDistance = Vector3.Distance(player.transform.position, wall.transform.position);
		Debug.Log($"initial distance: {initialDistance}");
		float atkRange = warrior.atkRange;
		Assert.Greater(atkRange, initialDistance, "wall and player are too far apart at the beginning of the test");

		mockInput.ButtonStates["Fire1"] = true;

		yield return new WaitForSeconds(1f);

		float distanzaFinale = Vector3.Distance(player.transform.position, wall.transform.position);
		Assert.AreEqual(distanzaFinale, initialDistance, "attack did knockback");
	}
}
