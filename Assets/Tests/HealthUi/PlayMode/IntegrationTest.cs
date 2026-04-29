using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

[TestFixture]
public class IntegrationTest
{
	private GameObject prefab;
	private GameObject instance;
	private UIHealthManager manager;
	private HealthSystem hs;
	private HealthBarView view;
	private float maxHealth = 100f; 

	[OneTimeSetUp]
	public void LoadPrefab()
	{

		prefab = Resources.Load<GameObject>("Prefabs/Buildings/POI");
		Assert.IsNotNull(prefab, "POI not found Resources/Prefabs/Buildings/POI!");
	}

	[SetUp]
	public void SetUp()
	{
		instance = Object.Instantiate(prefab);
		manager = instance.GetComponent<UIHealthManager>();
		hs = manager.GetHealthSystem();
		view = instance.GetComponentInChildren<HealthBarView>();

		Assert.IsNotNull(manager, "Missing UIHealthManager component");
		Assert.IsNotNull(hs, "Missing HealthSystem component");
		Assert.IsNotNull(view, "Missing HealthBarView component in children");

		hs.Initialize(maxHealth);
		manager.SetupUI();
	}

	[TearDown]
	public void TearDown()
	{
		if (instance != null)
		{
			Object.DestroyImmediate(instance);
		}
	}

	[UnityTest]
	public IEnumerator HealthManager_ShouldLinkSystemToUI()
	{
		
		manager.SetupUI();
		yield return null;

		var healthBar = view.GetComponent<RectTransform>();
		var maxWidth = healthBar.sizeDelta.x;
		hs.TakeDamage(50);

		yield return null;

		float expectedWidth = (float)50 / maxHealth * maxWidth;
		Assert.AreEqual(expectedWidth, healthBar.sizeDelta.x, 0.1f);
	}

	[UnityTest]
	public IEnumerator HealthManager_ShouldHandleRapidDamageSpam()
	{
		manager.SetupUI();
		yield return null;
		var healthBar = view.GetComponent<RectTransform>();
		var maxWidth = healthBar.sizeDelta.x;

		for (int i = 0; i < 50; i++)
		{
			hs.TakeDamage(1);
		}

		yield return new WaitForFixedUpdate(); 

		float expectedWidth = 0.5f * maxWidth;
		Assert.AreEqual(expectedWidth, healthBar.sizeDelta.x, 0.1f, "UI lost updates during rapid damage spam!");
	}
}
