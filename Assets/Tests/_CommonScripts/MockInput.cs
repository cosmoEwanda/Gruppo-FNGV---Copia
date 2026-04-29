using System.Collections.Generic;
using UnityEngine;

public class MockInput : IInputProvider
{
	public float Horizontal { get; set; }
	public float Vertical { get; set; }

	public Dictionary<string, bool> ButtonStates { get; } = new Dictionary<string, bool>();
	public Vector3 LookAtDirection { get; set; }

	public float GetHorizontal() => Horizontal;
	public float GetVertical() => Vertical;

	public bool IsButtonPressed(string buttonName) 
	{
		if (ButtonStates.TryGetValue(buttonName, out bool state))
		{
			return state;
		}
		return false;
	}
	public Vector3 GetPointerScreenPosition() => LookAtDirection;
}
