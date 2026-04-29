using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour, IRequireInput
{
	public Animator animator;
	public float speed = 6f;
	public float gravity = -9.81f;
	public float rotationSpeed = 500f;

	private IInputProvider input = new KeyboardInput();
	private CharacterController controller;
	private Camera cam;

	private Vector3 velocity;

	void Start()
	{
		controller = GetComponent<CharacterController>();
		cam = Camera.main;

		if (animator == null)
		{
			animator = GetComponentInChildren<Animator>();
		}
	}

	void Update()
	{
		MovePlayer();
		RotateToMouse();
	}	

	public void SetInputProvider(IInputProvider inputProvider)
	{
		input = inputProvider;
	}

	void MovePlayer()
	{
		float x = input.GetHorizontal();
		float z = input.GetVertical();

		Vector3 absoluteDir = new Vector3(x, 0f, z);
		absoluteDir = Vector3.ClampMagnitude(absoluteDir, 1f); 

		if (controller.isGrounded && velocity.y < 0)
		{
			velocity.y = -2f;
		}

		velocity.y += gravity * Time.deltaTime;

		Vector3 moveVector = (absoluteDir * speed) + velocity;
		controller.Move(moveVector * Time.deltaTime);

		UpdateMovementAnimations(absoluteDir);
	}

	void RotateToMouse()
	{
		Ray rayCameraToMousePos = cam.ScreenPointToRay(input.GetPointerScreenPosition());
		Plane horizontalPlane = new Plane(Vector3.up, transform.position);

		if (horizontalPlane.Raycast(rayCameraToMousePos, out float rayDistance))
		{
			Vector3 lookAtPoint = rayCameraToMousePos.GetPoint(rayDistance);
			Vector3 targetDir = lookAtPoint - transform.position;
			targetDir.y = 0;

			if (targetDir != Vector3.zero)
			{
				Quaternion targetRot = Quaternion.LookRotation(targetDir);
				transform.rotation = Quaternion.Slerp(transform.rotation, targetRot,
						rotationSpeed * Time.deltaTime);
			}
		}
	}

	void UpdateMovementAnimations(Vector3 moveDir)
	{
        if (animator == null) return;

        float currentSpeed = moveDir.magnitude;
        animator.SetFloat("movementSpeed", currentSpeed);
	}
}
