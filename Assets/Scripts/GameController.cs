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
		Debug.Log("starting VO coroutine");
		StartCoroutine(PlayVO(endVoiceOver[level]));
		StartCoroutine(PlayVO2(endVoiceOver2[level]));
		Debug.Log("after playVO coroutine");
	}

	public void LoadQuizLevel(int level)
	{
		qControl.SetLevel(level);
		mainMenu.SetActive(false);
		levelPanel.SetActive(true);
	}

	IEnumerator PlayVO(AudioClip aclip)
	{
		aSource.clip = aclip;
		aSource.Play();
		Debug.Log("playing first clip in PlayVO");
		while(aSource.isPlaying)
		{
			yield return null;
		}
	}	

	IEnumerator PlayVO2(AudioClip aclip2)
	{
		aSource.clip = aclip2;
		aSource.Play();
		Debug.Log("playing second clip in PlayVO");
		while(aSource.isPlaying)
		{
			yield return null;
		}
	}

	public void ShowMainMenu()
	{
		endScreen.SetActive(false);
		mainMenu.SetActive(true);
	}
}