using UnityEngine;
using UnityEngine.AI;

public class EnemyWalk : StateMachineBehaviour
{
	
	public float distanceAttackPlayer = 1;
	public float rotationSlerp = 10;

	private NavMeshAgent agent;
	private Transform target;
	
	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		agent = animator.GetComponent<NavMeshAgent>();
		agent.updateRotation = false;
		GameObject targetGameObj = GameObject.FindGameObjectWithTag("Player");
		if (targetGameObj != null)
		{
			target = targetGameObj.transform;
		}
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if (agent != null && target != null)
		{
			if (!agent.isActiveAndEnabled || !agent.isOnNavMesh) return;
			agent.SetDestination(target.position);
			Quaternion lookRotation = Quaternion.LookRotation(agent.velocity.normalized);
			animator.transform.rotation = Quaternion.Slerp(animator.transform.rotation, lookRotation, Time.deltaTime * rotationSlerp);
		}

		if (Vector3.Distance(animator.transform.position, target.position) < distanceAttackPlayer)
		{
			animator.SetTrigger("isPlayerClose");
		}
	}
}
