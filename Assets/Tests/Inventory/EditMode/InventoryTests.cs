using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class InventoryTests
{
    [Test]
    public void AddResource_NewType()
    {
		var inventory = new Inventory();
		var coinType = ScriptableObject.CreateInstance<ResourceType>();
		int coinAmount = 4;

		int added = inventory.AddResource(coinType, coinAmount);

		Assert.AreEqual(coinAmount, added);
		Assert.AreEqual(coinAmount, inventory.GetResourceAmount(coinType));
    }

	[Test]
	public void AddResource_IncrementAmount()
	{
		var inventory = new Inventory();
		var coinType = ScriptableObject.CreateInstance<ResourceType>();
		int coinAmount = 4;

		int firstAdd = inventory.AddResource(coinType, coinAmount);
		Assert.AreEqual(coinAmount, firstAdd);
		Assert.AreEqual(coinAmount, inventory.GetResourceAmount(coinType));

		int secondAdd = inventory.AddResource(coinType, coinAmount);
		Assert.AreEqual(coinAmount, secondAdd);
		Assert.AreEqual(coinAmount * 2, inventory.GetResourceAmount(coinType));
	}

	[Test]
	public void AddResource_NegativeAmount()
	{
		var inventory = new Inventory();
		var coinType = ScriptableObject.CreateInstance<ResourceType>();
		int coinAmount = -4;

		int added = inventory.AddResource(coinType, coinAmount);
		Assert.AreEqual(0, added);
		Assert.AreEqual(0, inventory.GetResourceAmount(coinType));
	}

	[Test]
	public void AddResource_AmountOverLimits()
	{
		var inventory = new Inventory();
		var coinType = ScriptableObject.CreateInstance<ResourceType>();

		inventory.AddResource(coinType, coinType.defaultMaxAmount + 1);
		Assert.AreEqual(coinType.defaultMaxAmount, inventory.GetResourceAmount(coinType));
	}

	[Test]
	public void AddResource_OverflowProtection_1()
	{
		var inventory = new Inventory();
		var coinType = ScriptableObject.CreateInstance<ResourceType>();

		int added = inventory.AddResource(coinType, int.MaxValue);
		Assert.AreEqual(coinType.defaultMaxAmount, added);
		Assert.AreEqual(coinType.defaultMaxAmount, inventory.GetResourceAmount(coinType));
	}

	[Test]
	public void AddResource_OverflowProtection_2()
	{
		var inventory = new Inventory();
		var coinType = ScriptableObject.CreateInstance<ResourceType>();
		coinType.defaultMaxAmount = int.MaxValue;

		int added = inventory.AddResource(coinType, int.MaxValue);
		Assert.AreEqual(coinType.defaultMaxAmount, added);
		Assert.AreEqual(coinType.defaultMaxAmount, inventory.GetResourceAmount(coinType));

		int secondAdd = inventory.AddResource(coinType, int.MaxValue);
		Assert.AreEqual(0, secondAdd);
		Assert.AreEqual(coinType.defaultMaxAmount, inventory.GetResourceAmount(coinType));	
	}

	[Test]
	public void ResetInventory()
	{
		var inventory = new Inventory();
		var coinType = ScriptableObject.CreateInstance<ResourceType>();
		inventory.AddResource(coinType, 4);
		Assert.AreEqual(4, inventory.GetResourceAmount(coinType));
		inventory.Clear();
		Assert.AreEqual(0, inventory.GetResourceAmount(coinType));
	}
}
