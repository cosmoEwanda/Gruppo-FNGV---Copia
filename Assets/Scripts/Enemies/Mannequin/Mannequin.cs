using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UIHealthManager))]
public class Mannequin : MonoBehaviour
{
	private HealthSystem healthSystem;
	private FlashColor flashColor;
	private UIHealthManager uiHealthManager;
	
	void Awake()
	{
		uiHealthManager = GetComponent<UIHealthManager>();
		flashColor = GetComponent<FlashColor>();
	}

	void Start()
	{
		healthSystem = uiHealthManager.GetHealthSystem();
		uiHealthManager.SetupUI();
		SetupHealthSystem();
	}

	public void SetupHealthSystem()
	{
		healthSystem.OnTakeDamage -= NotifyDamage;
		healthSystem.OnTakeHeal -= NotifyHealing;
		healthSystem.OnDeath -= NotifyDeath;
		healthSystem.OnTakeDamage += NotifyDamage;
		healthSystem.OnTakeHeal += NotifyHealing;
		healthSystem.OnDeath += NotifyDeath;
	}

	public HealthSystem GetHealthSystem()
	{
		return healthSystem;
	}

	public UIHealthManager GetUIHealthManager()
	{
		return uiHealthManager;
	}

	private void NotifyDamage(float amount)
	{
		flashColor.Flash(flashColor.flashColorDamage);
		Debug.Log("Damage taken");
	}

	private void NotifyHealing(float amount)
	{
		flashColor.Flash(flashColor.flashColorHealing);
		Debug.Log("Heal taken");
	}

	private void NotifyDeath()
	{
		Debug.Log("Is mannequin dead? Answer: " + healthSystem.IsDead());
	}

	//public  float GetHealth() => healthSystem.CurrentHealth;
}
