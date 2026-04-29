using UnityEngine;
using UnityEngine.SceneManagement;

public class GameModePanel : MonoBehaviour
{
	public void SelectGameMode(string sceneName)
	{
		SceneManager.LoadScene(sceneName);
	}
}
