using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {
	
	public void LoadCredits()
	{
		SceneManager.LoadScene("Credits");
	}
	
	public void LoadMainMenu()
	{
		SceneManager.LoadScene("GameScene");
	}
}
