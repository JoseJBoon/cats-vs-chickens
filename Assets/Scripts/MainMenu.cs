using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public void LoadGame()
	{
		// this should be changed to the actual game scene
		SceneManager.LoadScene(2);
	}

	public void ExitGame()
	{
		Application.Quit();
		Debug.Log("Game is exiting....");
	}
}
