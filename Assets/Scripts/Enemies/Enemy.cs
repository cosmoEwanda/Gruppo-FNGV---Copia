using UnityEngine;
using UnityEngine.AI;
using System.Collections;

[RequireComponent(typeof(HealthSystem))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour, IKnockback
{
	private HealthSystem healthSystem;
	private Rigidbody rigidBody;
	private NavMeshAgent agent;

	void Awake()
	{
		healthSystem = GetComponent<HealthSystem>();
		rigidBody = GetComponent<Rigidbody>();
		agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
	}
	
    void Start()
    {
		healthSystem.OnDeath -= Die;
		healthSystem.OnDeath += Die;
    }
 
	void Die()
	{
		if (TryGetComponent<LootDropper>(out LootDropper dropper))
		{
			dropper.DropLoot();
		}
	}

	void OnDestroy()
	{
		healthSystem.OnDeath -= Die;
	}

	private IEnumerator KnockbackRoutine(Vector3 verse, float force)
	{
		agent.enabled = false;

		//rigidBody.isKinematic = false;
		rigidBody.linearVelocity = Vector3.zero;
		rigidBody.AddForce(-verse * force, ForceMode.Impulse);

		yield return new WaitForSeconds(0.3f);

		rigidBody.linearVelocity = Vector3.zero;
		//rigidBody.isKinematic = true;

		agent.enabled = true;
	}

	public void ApplyKnockback(Vector3 forceVerse, float knockBack)
	{
		StartCoroutine(KnockbackRoutine(forceVerse, knockBack));
	}
}
