using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour 
{
	public AudioClip quizIntros;
	private UnifiedQuizController qControl;
	public GameObject mainMenu;
	public GameObject levelPanel;
	public AudioSource aSource;
	
	// end screen data
	public Sprite[] endSprites;
	public AudioClip[] endVoiceOver;
	public AudioClip[] endVoiceOver2;
	public Sprite[] avatars;
	private int level;

	// end screen elements
	public GameObject endScreen;
	public Image endImage, avatar;

	public GameObject[] gamePanels; // these are the panels used for the quiz and any ending screens. used to disable all panels when quitting to menu to avoid double ups


	void Start () 
	{
		qControl = FindObjectOfType<UnifiedQuizController>();
	}

	public void EndGame(int level)
	{
		this.level = level;
		levelPanel.SetActive(false);
		endScreen.SetActive(true);
		endImage.sprite = endSprites[level];
		StartCoroutine(PlayVO(endVoiceOver[level], level));
		avatar.sprite = avatars[level];
	}

	public void LoadQuizLevel(int level)
	{
		qControl.SetLevel(level);
		mainMenu.SetActive(false);
		levelPanel.SetActive(true);
		aSource.clip = quizIntros;
		aSource.Play();
	}

	IEnumerator PlayVO(AudioClip aclip, int level)
	{
		aSource.clip = aclip;
		aSource.Play();
		while(aSource.isPlaying)
		{
			yield return null;
		}
	}	

	public void PlayVO2()
	{
		aSource.clip = endVoiceOver2[level];
		aSource.Play();
	}

	public void ShowMainMenu()
	{
		endScreen.SetActive(false);
		mainMenu.SetActive(true);
	}

	public void QuitToMenu()
	{
		foreach(GameObject panel in gamePanels)
		{
			panel.SetActive(false);
		}
		mainMenu.SetActive(true);
	}
}