using UnityEngine;

public class Tester : MonoBehaviour
{
	public GameObject prefab;
	private Mannequin target;

	public float interval = 2.0f;

	private float timer = 0f;
	private int step = 0;

	void Awake()
	{
		RespawnManager.OnEnemySpawned += GetMannequin;
	}
	void Update()
	{
		if (target == null) return;

		timer += Time.deltaTime;

		if (timer >= interval)
		{
			timer = 0f;
			ExecuteStep();
			step++;
		}
	}

	void GetMannequin(Mannequin mannequin)
	{
		target = mannequin;
	}

	void ExecuteStep()
	{
		switch (step % 4)
		{
			case 0:
				target.GetHealthSystem().TakeDamage(10);
				break;
			case 1:
				target.GetHealthSystem().TakeHeal(5);
				break;
			case 2:
				target.GetHealthSystem().TakeDamage(20);
				break;
			case 3:
				target.GetHealthSystem().TakeHeal(10);
				break;
		}
	}
}
