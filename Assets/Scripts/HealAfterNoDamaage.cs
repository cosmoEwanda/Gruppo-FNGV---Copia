using UnityEngine;
using System.Collections;

public class HealAfterNoDamage : MonoBehaviour
{
	[Header("Settings")]
	public float healAfterSeconds = 5f; // Secondi di attesa senza danni
	public float healAmount = 5f;      // Quanto curare
	public float healTickRate = 1f;     // Ogni quanti secondi curare (es. 10 HP ogni 1 secondo)

	private HealthSystem hs;
	private Coroutine healingCoroutine;

	void Start()
	{
		hs = GetComponent<HealthSystem>();

		if (hs != null)
		{
			hs.OnTakeDamage += ResetHealTimer;
		}
	}

	void ResetHealTimer(float damage)
	{
		if (healingCoroutine != null)
		{
			StopCoroutine(healingCoroutine);
		}

		healingCoroutine = StartCoroutine(HealRoutine());
	}

	private IEnumerator HealRoutine()
	{
		yield return new WaitForSeconds(healAfterSeconds);

		while (hs.CurrentHealth < hs.MaxHealth)
		{
			hs.TakeHeal(healAmount);
			yield return new WaitForSeconds(healTickRate);
		}

		healingCoroutine = null;
	}

	private void OnDestroy()
	{
		if (hs != null)
		{
			hs.OnTakeDamage -= ResetHealTimer;
		}
	}
}