using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TimerUI: MonoBehaviour
{
	public GameObject TimeText;
    private TextMeshProUGUI timerText;
	private HealthSystem healthSystem;
	public GameObject endPanel;

	public float timeLeft = 30f;
	void Awake()
	{
		//endPanel.SetActive(false);
		var player = GameObject.FindWithTag("Player");
		healthSystem = player.GetComponent<HealthSystem>();
		healthSystem.OnDeath += GameOver;

		if (TimeText != null)
		{
			timerText = TimeText.GetComponent<TextMeshProUGUI>(); 
			if (timerText == null)
			{
				Debug.LogError("TextMeshPro component not found on TimeText object.");
			}
		}
		else
		{
			Debug.LogError("TimeText GameObject is not assigned.");
		}
	}

	void Update()
	{
		if (timeLeft <= 0)
		{
			YouWin();
		}
		else
		{
			timeLeft -= Time.deltaTime;
			UpdateVisuals(timeLeft);
		}
		
	}
	public void UpdateVisuals(float timeToDisplay)
	{
        if (timerText != null)
		{
			timerText.text = $"{timeToDisplay:F2}"; // Display time with 2 decimal places
		}
	}

	private void YouWin()
	{
		Time.timeScale = 0f; // Pause the game
		endPanel.SetActive(true);
		endPanel.GetComponentInChildren<TextMeshProUGUI>().text = "You Win!";
		// Implement win logic here (e.g., show win screen, load next level, etc.)
	}
	private void GameOver()
	{
		Time.timeScale = 0f; // Pause the game
		endPanel.SetActive(true);
		endPanel.GetComponentInChildren<TextMeshProUGUI>().text = "Game Over";
		// Implement game over logic here (e.g., show game over screen, reset level, etc.)
	}

	public void LoadMenuScene()
	{       // Implement scene loading logic here (e.g., using SceneManager.LoadScene)
		Debug.Log("Loading Main Menu...");
		SceneManager.LoadScene("MainMenu");
	}
}
