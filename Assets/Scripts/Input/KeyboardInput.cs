using UnityEngine;

public class KeyboardInput : IInputProvider
{
	public float GetHorizontal() => Input.GetAxis("Horizontal");
	public float GetVertical() => Input.GetAxis("Vertical");
	public bool IsButtonPressed(string buttonName) => Input.GetButton(buttonName);
	public Vector3 GetPointerScreenPosition() => Input.mousePosition;
}

