using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using NUnit.Framework;

public class EnemyBaseTets
{
	[UnitySetUp]
	public IEnumerator Setup()
	{
		yield return SceneManager.LoadSceneAsync("EnemyTestScene");
		yield return null;

		InputInjector.ApplyGlobally(new MockInput());
	}

	[UnityTest]
	public IEnumerator EnemyBase()
	{
		GameObject player = GameObject.FindWithTag("Player");
		GameObject nemico = GameObject.FindWithTag("Enemy");

		Assert.IsNotNull(player, "Player not found in Scene!");
		Assert.IsNotNull(nemico, "Nemico not found in Scene!");

		HealthSystem health = player.GetComponent<HealthSystem>();
		float initHealth = health.GetCurrentHealth();

		float distance = Vector3.Distance(player.transform.position, nemico.transform.position);
		Debug.Log($"Initial distance: {distance}");
		Assert.Greater(distance, 5f, "Player and Enemy too close at start scene!");

		float timeout = Time.time + 10f;
		while (Vector3.Distance(player.transform.position, nemico.transform.position) > 2f)
		{
			if (Time.time > timeout)
			{
				Assert.Fail("Enemy could not get near player in less the 10 sec.");
			}
			yield return null;
		}

		Debug.Log("Enemy got to the player!");

		yield return new WaitForSeconds(1.5f);

		Debug.Log($"final health: {health.GetCurrentHealth()}");
		Assert.Less(health.GetCurrentHealth(), initHealth, "Enemy Close but player didnt get any dmg.");
	}
}

