using UnityEngine;

[RequireComponent(typeof(HealthSystem))]
public class UIHealthManager : MonoBehaviour
{
	public IHealthUI healthBarUI;
	private HealthSystem healthSystem;

	public HealthSystem GetHealthSystem() => healthSystem;

	void Awake()
	{
		healthSystem = GetComponent<HealthSystem>();

		if (healthBarUI == null)
		{
			healthBarUI = GetComponentInChildren<IHealthUI>();
			if (healthBarUI == null)
			{
				Debug.Log("HealthBarUI non trovato!");
			}
		}
	}

	void Start()
	{
		if (healthBarUI != null)
		{
			SetupUI();
		}
	}

	public void SetupUI()
	{
		if (healthSystem == null) return;

		healthSystem.OnTakeDamage -= UpdateHealthUI;
		healthSystem.OnTakeDamage += UpdateHealthUI;
		healthSystem.OnTakeHeal -= UpdateHealthUI;
		healthSystem.OnTakeHeal += UpdateHealthUI;

		healthBarUI.UpdateVisuals(healthSystem.CurrentHealth, healthSystem.MaxHealth);
	}

	public void UpdateHealthUI(float amount)
	{
		healthBarUI.UpdateVisuals(healthSystem.CurrentHealth, healthSystem.MaxHealth);
	}

	private void OnDestroy()
	{
		if (healthSystem != null && healthBarUI != null)
		{
			healthSystem.OnTakeDamage -= UpdateHealthUI;
			healthSystem.OnTakeHeal -= UpdateHealthUI;
		}
	}
}
