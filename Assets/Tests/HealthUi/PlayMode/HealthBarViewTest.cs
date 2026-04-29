using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

[TestFixture]
public class HealthBarViewTest
{

	GameObject tester, canvas;
	HealthBarView healthBarView;
	RectTransform rect;

	float w, h;

	[SetUp]
	public void SetUp()
	{	
		w = 200f; h = 50f;
		canvas = new GameObject("Canvas",typeof(Canvas));
		tester = new GameObject("Tester", typeof(RectTransform));
		tester.transform.SetParent(canvas.transform);

		rect = tester.GetComponent<RectTransform>();
		rect.sizeDelta = new Vector2(w, h);

		healthBarView = tester.AddComponent<HealthBarView>();
	}

	[Test]
	public void HealthBarView_InitializesCorrectly()
	{
		Assert.IsNotNull(healthBarView);
		Assert.AreEqual(w, rect.sizeDelta.x);
		Assert.AreEqual(h, rect.sizeDelta.y);
	}

	[Test]
	public void HealthBarView_UpdatesHealthCorrectly()
	{
		float newHealthPercentage = 0.75f;
		healthBarView.UpdateVisuals(w*newHealthPercentage, w);

		float currentHealthPercentage = healthBarView.GetCurrentHealthPercentage();
		Assert.AreEqual(newHealthPercentage, currentHealthPercentage);
	}

	[Test]
	public void HealthBarView_EmptyHealth_WidthIsZero()
	{
		healthBarView.UpdateVisuals(0f, w);
		Assert.AreEqual(0f, rect.sizeDelta.x, "Bar width should be 0 when health is 0!");
	}

	[Test]
	public void HealthBarView_ExceedingHealth_WidthIsGreater()
	{

		float exceedingPercentage = 1.2f;
		healthBarView.UpdateVisuals(w * exceedingPercentage, w);

		float expectedWidth = w;
		Assert.AreEqual(expectedWidth, rect.sizeDelta.x, "Bar width greater than maximum!");
	}

	[Test]
	public void HealthBarView_NegativeHealth_WidthBehavior()
	{
		healthBarView.UpdateVisuals(-0.5f, w);
		Assert.AreEqual(0f, rect.sizeDelta.x);
	}

	[TearDown]
	public void TearDown()
	{
		Object.DestroyImmediate(canvas.gameObject);
	}
}
