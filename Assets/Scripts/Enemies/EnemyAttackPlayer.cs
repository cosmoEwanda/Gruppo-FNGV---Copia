using UnityEngine;
using UnityEngine.AI;

public class EnemyAttackPlayer : StateMachineBehaviour
{
	public int attackDamage = 10; 
	private Transform target;
	private NavMeshAgent agent;
	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		agent = animator.GetComponent<NavMeshAgent>();
		agent.updateRotation = false;
		agent.velocity = Vector3.zero;

		GameObject targetGameObj = GameObject.FindGameObjectWithTag("Player");
		if (targetGameObj != null)
		{
			target = targetGameObj.transform;
			Vector3 direction = (target.position - animator.transform.position).normalized;
			direction.y = 0;

			if (direction != Vector3.zero) // Sicurezza per evitare errori se sono sopra il player
			{
				animator.transform.rotation = Quaternion.LookRotation(direction);
			}

			HealthSystem targetHealth = targetGameObj.GetComponent<HealthSystem>();
			if (targetHealth != null)
			{
				targetHealth.TakeDamage(attackDamage);
			}
		}
		else
		{
			Debug.Log("Player Not Found");
		}
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	//override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	//{

	//}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	//{
	//    
	//}

	// OnStateMove is called right after Animator.OnAnimatorMove()
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	//{
	//    // Implement code that processes and affects root motion
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK()
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	//{
	//    // Implement code that sets up animation IK (inverse kinematics)
	//}
}
