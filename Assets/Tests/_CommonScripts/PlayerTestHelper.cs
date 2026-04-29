using UnityEngine;
using System.Collections.Generic;

public class PlayerTestHelper
{
	private PlayerMovement movement;
	private Transform playerTransform;
	private MockInput mockInput;
	private Camera cam;

	public PlayerTestHelper(Camera camera, GameObject player)
	{
		movement = player.GetComponent<PlayerMovement>();
		playerTransform = player.transform;
		cam = camera;
	}

	public void MoveTowards(MockInput mockInput, Vector3 destPosition)
	{
		if (playerTransform == null) return;

		Vector3 dir = (destPosition - playerTransform.position).normalized;
		mockInput.Horizontal = dir.x;
		mockInput.Vertical = dir.z;
		mockInput.LookAtDirection = cam.WorldToScreenPoint(destPosition);
	}
}
