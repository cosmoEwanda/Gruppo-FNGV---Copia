using UnityEngine;

public class MainMenu : MonoBehaviour
{
	public GameObject mainPanel;
	public GameObject gameModePanel;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (gameModePanel.activeInHierarchy)
			{
				ShowMainPanel();
			}
		}
	}

	public void ShowGameModesPanel()
	{
		mainPanel.SetActive(false);
		gameModePanel.SetActive(true);
	}

	public void ShowMainPanel()
	{
		gameModePanel.SetActive(false);
		mainPanel.SetActive(true);
	}

	public void QuitGame()
	{
		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
		#else
			Application.Quit();
		#endif
	}
}
