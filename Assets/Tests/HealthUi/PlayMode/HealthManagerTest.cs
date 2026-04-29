using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

[TestFixture]
public class HealthManagerTest
{
	private GameObject prefab;
	private GameObject instance;
	private UIHealthManager manager;
	private HealthSystem hs;
	private HealthBarView view;

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

		hs.Initialize(100f);
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
	public IEnumerator Damage_UpdatesUI_Correctly()
	{
		hs.TakeDamage(30);
		yield return null;

		Assert.AreEqual(0.7f, view.GetCurrentHealthPercentage(), 0.01f);
	}

	[UnityTest]
	public IEnumerator Healing_UpdatesUI_Correctly()
	{
		hs.TakeDamage(50);
		yield return null;

		hs.TakeHeal(20);
		yield return null;

		Assert.AreEqual(0.7f, view.GetCurrentHealthPercentage(), 0.01f);
	}

	[UnityTest]
	public IEnumerator MultipleSetupUI_DoesNotDuplicateSubscribers()
	{
		manager.SetupUI();
		manager.SetupUI();

		hs.TakeDamage(10);
		yield return null;

		Assert.AreEqual(0.9f, view.GetCurrentHealthPercentage(), 0.01f);
	}
}
	

	

	
