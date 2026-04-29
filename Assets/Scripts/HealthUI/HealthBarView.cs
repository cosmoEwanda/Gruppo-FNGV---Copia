using UnityEngine;

public class HealthBarView : MonoBehaviour, IHealthUI
{
	private RectTransform rect;
	private float maxWidth;
	private float height;

	void Awake()
	{
		rect = GetComponent<RectTransform>();
		maxWidth = rect.sizeDelta.x;
		height = rect.sizeDelta.y;
	}

	public void UpdateVisuals(float currentHealth, float maxHealth)
	{
		float healthPercentage = Mathf.Clamp01(currentHealth/maxHealth);
		float newWidth = healthPercentage * maxWidth;
		rect.sizeDelta = new Vector2(newWidth, rect.sizeDelta.y);
	}

	public float GetCurrentHealthPercentage()
	{
		return rect.sizeDelta.x / maxWidth;
	}
}
