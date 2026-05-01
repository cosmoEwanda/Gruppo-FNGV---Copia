using UnityEngine;
using System.Collections;

public sealed class SimpleEnemySpawner : MonoBehaviour
{
	[Header("Settings")]
	public GameObject enemyPrefab;
	public int enemiesPerSpawn = 3;

	public float asyncActivationSeconds = 2f; 
	public float spawnIntervalSeconds = 5f;   
	public Vector3 offsetPosition;

	private float delayBetweenEnemies = 0.2f; 

	void Start()
	{
		
		StartCoroutine(SpawnRoutine());
	}

	private IEnumerator SpawnRoutine()
	{
		yield return new WaitForSeconds(asyncActivationSeconds);

		while (true)
		{
			for (int i = 0; i < enemiesPerSpawn; i++)
			{
				SpawnEnemy();
				yield return new WaitForSeconds(delayBetweenEnemies);
			}
			yield return new WaitForSeconds(spawnIntervalSeconds);
		}
	}

	private void SpawnEnemy()
	{
		if (enemyPrefab != null)
		{
			Instantiate(enemyPrefab, transform.position + offsetPosition, Quaternion.identity);
		}
	}
}