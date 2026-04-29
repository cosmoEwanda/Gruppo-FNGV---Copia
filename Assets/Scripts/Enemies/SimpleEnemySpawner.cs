using UnityEngine;
using System.Collections;
public class SimpleEnemySpawner : MonoBehaviour
{
	public GameObject enemyPrefab;
	public int enemiesPerSpawn = 3;

	public float spawnIntervalSeconds = 5f;
	public Vector3 offsetPosition;

	private float delaySpawnSeconds = 0.2f;

	private IEnumerator coroutine;
	void Start()
	{
			coroutine = SpawnEnemies(spawnIntervalSeconds);
			StartCoroutine(coroutine);
	}

	private IEnumerator SpawnEnemies(float waitTime)
	{
		while (true)
		{
			for (int i = 0; i < enemiesPerSpawn; i++)
			{
				Instantiate(enemyPrefab, this.transform.position + offsetPosition, Quaternion.identity);
				yield return new WaitForSeconds(delaySpawnSeconds);
			}
			yield return new WaitForSeconds(waitTime);
		}

	}
}
