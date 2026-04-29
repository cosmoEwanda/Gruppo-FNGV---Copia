using UnityEngine;
using System.Linq;

public class InputInjector
{
	private static IInputProvider currentProvider;

	public static void ApplyGlobally(IInputProvider inputProvider)
	{
		currentProvider = inputProvider;

		var objectsNeedingInput = Object.FindObjectsByType<MonoBehaviour>(
				FindObjectsInactive.Include,
				FindObjectsSortMode.None).OfType<IRequireInput>();

		foreach (var obj in objectsNeedingInput)
		{
			obj.SetInputProvider(inputProvider);
		}
	}
}

