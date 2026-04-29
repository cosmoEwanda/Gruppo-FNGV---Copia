using System;
using UnityEngine;


public class HealthSystem : MonoBehaviour
{
	public event Action<float> OnTakeDamage;
	public event Action<float> OnTakeHeal;
	public event Action OnDeath;

	[SerializeField]
	private float health = 100f;
	[SerializeField] 
	private bool destroyOnDeath = true;

	private bool isDead;

	public float MaxHealth { get; private set; }
	public float CurrentHealth { get; private set; }
	public bool DestroyOnDeath 
	{ 
		get { return destroyOnDeath; } 
		set { destroyOnDeath = value; } 
	}

	public void Awake()
	{
		Initialize(health);
	}

	public void Initialize(float initialHealth)
	{
		if (initialHealth < 0)
			throw new ArgumentException("Initial health cannot be negative.", nameof(initialHealth));
		health = initialHealth;
		MaxHealth = initialHealth;
		SetHealth(initialHealth);
		isDead = false;
	}

	public void SetHealth(float nHealth)
	{
		if (IsDead()) return;

		if (nHealth < 0 || nHealth > MaxHealth) return;

		if (nHealth < CurrentHealth)
		{
			TakeDamage(CurrentHealth - nHealth);
		}
		else if (nHealth > CurrentHealth)
		{
			TakeHeal(nHealth - CurrentHealth);
		}
	}
	public void TakeDamage(float amount)
	{
		if (IsDead()) return;

		if (amount < 0)
			throw new ArgumentException("Damage amount cannot be negative.", nameof(amount));
		CurrentHealth = Math.Max(0, CurrentHealth - amount);
		OnTakeDamage?.Invoke(amount);

		if (CurrentHealth <= 0)
		{
			CurrentHealth = 0;
			isDead = true;

			OnDeath?.Invoke();

			if (destroyOnDeath)
			{
				Destroy(gameObject);
			}
		}
	}

	public void TakeHeal(float amount)
	{
		if (IsDead()) return;

		if (amount < 0)
			throw new ArgumentException("Heal amount cannot be negative.", nameof(amount));
		CurrentHealth = Math.Min(MaxHealth, CurrentHealth + amount);
		OnTakeHeal?.Invoke(amount);
	}

	public bool IsDead()
	{
		return isDead;
	}

	public float GetCurrentHealth()
	{ 
		return CurrentHealth; 
	}
}
