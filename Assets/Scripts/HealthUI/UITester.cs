using UnityEngine;

public class UITester : MonoBehaviour
{
	public UIHealthManager manager;
	public float interval = 2.0f; 

	private float timer = 0f;
	private int step = 0;

	void Update()
	{
		if (manager == null) return;

		timer += Time.deltaTime;

		if (timer >= interval)
		{
			timer = 0f;
			ExecuteStep();
			step++;
		}
	}

	void ExecuteStep()
	{
		switch (step % 4)
		{
			case 0:
				Debug.Log("Test: Danno 10");
				manager.GetHealthSystem().TakeDamage(10);
				break;
			case 1:
				Debug.Log("Test: Cura 5");
				manager.GetHealthSystem().TakeHeal(5);
				break;
			case 2:
				Debug.Log("Test: Danno 20");
				manager.GetHealthSystem().TakeDamage(20);
				break;
			case 3:
				Debug.Log("Test: Cura 10");
				manager.GetHealthSystem().TakeHeal(10);
				break;
		}
	}
}
