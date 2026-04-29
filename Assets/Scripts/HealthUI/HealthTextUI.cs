using UnityEngine;
using TMPro;

public class HealthTextUI: MonoBehaviour, IHealthUI
{
	public GameObject HpText;
    private TextMeshProUGUI hpTextMesh;
	void Awake()
	{
		if (HpText != null)
		{
			hpTextMesh = HpText.GetComponent<TextMeshProUGUI>(); 
			if (hpTextMesh == null)
			{
				Debug.LogError("TextMeshPro component not found on HpText object.");
			}
		}
		else
		{
			Debug.LogError("HpText GameObject is not assigned.");
		}
	}

    public void UpdateVisuals(float currentHealth, float maxHealth)
	{
        if (hpTextMesh != null && currentHealth >= 0 && currentHealth <= maxHealth)
		{
			hpTextMesh.text = $"{currentHealth} / {maxHealth}";
		}
	}
}
