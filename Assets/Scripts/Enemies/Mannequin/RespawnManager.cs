using UnityEngine;
using System;

public class RespawnManager : MonoBehaviour
{
	public static RespawnManager Instance { get; private set; }
	public static event Action<Mannequin> OnEnemySpawned;

	public Mannequin enemyPrefab;
	public Vector3 spawnPoint;
	public Vector3 spawnRotation;
	public float respawnTime = 2.0f;

	private Mannequin copy;
	private float timer;
	private bool isRespawning = false;

	private void Awake()
	{
		if (Instance == null) Instance = this;
		else Destroy(gameObject);
	}

	private void Start()
	{
		SpawnNow();
	}

	private void Update()
	{
		if (copy == null && !isRespawning)
		{
			isRespawning = true;
			Invoke("SpawnNow", respawnTime); 
		}
	}

	public void SpawnNow()
	{
		isRespawning = false;
		copy = Instantiate(enemyPrefab, spawnPoint, Quaternion.Euler(spawnRotation));
		OnEnemySpawned?.Invoke(copy);
	}	
}
