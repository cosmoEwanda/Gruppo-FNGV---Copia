using UnityEngine;

public interface IInputProvider
{
    float GetHorizontal();
    float GetVertical();
	bool IsButtonPressed(string buttonName);
	Vector3 GetPointerScreenPosition();
}
