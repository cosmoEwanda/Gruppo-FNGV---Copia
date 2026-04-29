using UnityEngine;
using UnityEngine.Windows;

public class Warrior : MonoBehaviour, IRequireInput
{
	public float atkRange = 3f;
	public float dmg = 1f;
	public float atkRate = 0.3f;
	private float nextAtkTimer = 0f;
	private const float atkOrigin = 0.5f;
	public float knockBack = 100f;
	private Animator atkAnimator;

	private IInputProvider input;	

	void Start()
	{
		atkAnimator = GetComponent<Animator>();
		SetInputProvider(new KeyboardInput());
	}

	void Update()
	{
		if (atkAnimator != null)
		{
			bool isMoving = input.GetHorizontal() != 0 || input.GetVertical() != 0;
			atkAnimator.SetBool("stationary", !isMoving);

			if (input.IsButtonPressed("Fire1") && Time.time >= nextAtkTimer)
			{
				AttackAnimation(isMoving);
				Attack();

				nextAtkTimer = Time.time + atkRate;
			}
		}
	}

	public void SetInputProvider(IInputProvider inputProvider)
	{
		input = inputProvider;
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Vector3 origin = transform.position + Vector3.up * atkOrigin;
		Gizmos.DrawRay(origin, transform.forward * atkRange);
	}

	void AttackAnimation(bool isMoving)
	{
		int layerIndex = 0;
		AnimatorStateInfo state = atkAnimator.GetCurrentAnimatorStateInfo(layerIndex);
		bool isInTransition = atkAnimator.IsInTransition(layerIndex);

		if (!isMoving)
		{
			int randomAtk = Random.Range(0, 3);
			atkAnimator.SetInteger("atkIndex", randomAtk);
		}

		bool isActuallyIdle = (state.IsTag("Idle") || state.IsName("Empty")) && !isInTransition;

		if (isActuallyIdle)
		{
			atkAnimator.ResetTrigger("pressed");
			atkAnimator.SetTrigger("atkTrigger");
		}
		else
		{
			atkAnimator.ResetTrigger("atkTrigger");
			atkAnimator.SetTrigger("pressed");
		}

	}

	void Attack()
	{
		Vector3 origin = transform.position + Vector3.up * atkOrigin;
		Ray ray = new Ray(origin, transform.forward);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, atkRange))
		{
			HealthSystem health = hit.collider.GetComponent<HealthSystem>();
			if (health != null)
			{
				health.TakeDamage(dmg);
			}

			if (hit.collider.TryGetComponent<IKnockback>(out IKnockback knockbackComponent))
			{
				knockbackComponent.ApplyKnockback(hit.normal, knockBack);
			}
		}
	}


}
