using UnityEngine;
using NUnit.Framework;
using System;

[TestFixture]
public class HealthSystemTest
{
	private GameObject obj;
	private const float InitialHealth = 100f;
	private HealthSystem hs;
	
	[SetUp]
	public void Setup()
	{
		obj = new GameObject();
		hs = obj.AddComponent<HealthSystem>();
		hs.DestroyOnDeath = false;
	}

	[TearDown]
	public void Teardown()
	{
		GameObject.DestroyImmediate(obj);
	}

	[Test]
	public void HealthSystem_WhenNegativeHealth_ThrowException()
	{		
		Assert.Throws<ArgumentException>(() => hs.Initialize(-20));
	}

	[Test]
	public void HealthSystem_WhenDamageIsNegative_ThrowException()
	{
		hs.Initialize(InitialHealth);
		Assert.Throws<ArgumentException>(() => hs.TakeDamage(-10));
	}

	[Test]
	public void HealthSystem_WhenHealIsNegative_ThrowException()
	{
		hs.Initialize(InitialHealth);
		Assert.Throws<ArgumentException>(() => hs.TakeHeal(-10));
	}

	[Test]
	public void HealthSystem_WhenDamageIsLessThanCurrentHealth_HealthShouldDecrease()
	{
		hs.Initialize(InitialHealth);
		hs.TakeDamage(30);
		Assert.AreEqual(70, hs.CurrentHealth);
	}

	[Test]
	public void HealthSystem_WhenDamageIsGreaterThanCurrentHealth_HealthShouldBeZero()
	{
		hs.Initialize(InitialHealth);
		hs.TakeDamage(150);
		Assert.AreEqual(0, hs.CurrentHealth);
	}

	[Test]
	public void HealthSystem_WhenHealIsLessThanMaxHealth_HealthShouldIncrease()
	{
		hs.Initialize(InitialHealth);
		hs.TakeDamage(50);
		hs.TakeHeal(30);
		Assert.AreEqual(80, hs.CurrentHealth);
	}

	[Test]
	public void HealthSystem_WhenHealIsGreaterThanMaxHealth_HealthShouldBeMax()
	{
		hs.Initialize(InitialHealth);
		hs.TakeDamage(50);
		hs.TakeHeal(80);
		Assert.AreEqual(InitialHealth, hs.CurrentHealth);
	}

	[Test]
	public void HealthSystem_WhenHealthReachesZero_ShouldInvokeOnDeath()
	{
		hs.Initialize(InitialHealth);
		bool isDead = false;
		hs.OnDeath += () => isDead = true;
		hs.TakeDamage(InitialHealth);
		Assert.IsTrue(isDead);
	}

	[Test]
	public void HealthSystem_WhenDamageTaken_ShouldInvokeOnTakeDamage()
	{
		hs.Initialize(InitialHealth);
		float damageAmount = 30f;
		float receivedDamage = 0f;
		hs.OnTakeDamage += (amount) => receivedDamage = amount;
		hs.TakeDamage(damageAmount);
		Assert.AreEqual(damageAmount, receivedDamage);
	}

	[Test]
	public void HealthSystem_WhenHealTaken_ShouldInvokeOnTakeHeal()
	{
		hs.Initialize(InitialHealth);
		float healAmount = 30f;
		float receivedHeal = 0f;
		hs.OnTakeHeal += (amount) => receivedHeal = amount;
		hs.TakeDamage(50);
		hs.TakeHeal(healAmount);
		Assert.AreEqual(healAmount, receivedHeal);
	}

}
