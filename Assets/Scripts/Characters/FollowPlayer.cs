using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
	public Transform player;
	public Vector3 cameraOffset = new Vector3(0, 5, -2);

	void LateUpdate()
	{
		transform.position = player.position + cameraOffset;
	}
}
