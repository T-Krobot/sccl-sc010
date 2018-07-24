using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour 
{

	private UnifiedQuizController qControl;
	public GameObject mainMenu;
	public GameObject levelPanel;
	public AudioSource aSource;
	
	// end screen data
	public Sprite[] endSprites;
	public AudioClip[] endVoiceOver;
	public AudioClip[] endVoiceOver2;
	public Sprite[] avatars;

	// end screen elements
	public GameObject endScreen;
	public Image endImage, avatar;


	void Start () 
	{
		qControl = FindObjectOfType<UnifiedQuizController>();
	}

	public void EndGame(int level)
	{
		levelPanel.SetActive(false);
		endScreen.SetActive(true);
		avatar.sprite = avatars[level];
		endImage.sprite = endSprites[level];
		StartCoroutine(PlayVO(endVoiceOver[level], endVoiceOver2[level]));
		endScreen.SetActive(false);
		mainMenu.SetActive(true);
	}

	public void LoadQuizLevel(int level)
	{
		qControl.SetLevel(level);
		mainMenu.SetActive(false);
		levelPanel.SetActive(true);
	}

	IEnumerator PlayVO(AudioClip aclip, AudioClip aclip2)
	{
		aSource.clip = aclip;
		while(aSource.isPlaying)
		{
			yield return null;
		}

		aSource.clip = aclip2;
		while(aSource.isPlaying)
		{
			yield return null;
		}
	}	
}