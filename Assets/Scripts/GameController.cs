using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// this script handles all the non-quiz aspects of the game. loading levels, end screens and voice clip playing for each level.

public class GameController : MonoBehaviour 
{
	public AudioClip quizIntros;					// this clip is used at the start of each level
	private UnifiedQuizController qControl;			// quiz script
	public GameObject mainMenu;						// the panel used for the main menu
	public GameObject levelPanel;					// panel used for the levels
	public AudioSource aSource;
	
	// end screen data
	public Sprite[] endSprites;						// images of the locations the quiz is about, shown at the end
	public AudioClip[] endVoiceOver;				// first part of voice over, there are 3 files, one for each location
	public AudioClip[] endVoiceOver2;				// second part of above
	public Sprite[] avatars;						// character avatars shown at the end of each level
	private int level;								// level to load

	// end screen elements
	public GameObject endScreen;					// end screen panel
	public Image endImage, avatar;					// image object used for endimage and avatar				

	public GameObject[] gamePanels; // these are the panels used for the quiz and any ending screens. used to disable all panels when quitting to menu to avoid double ups. NOTE: mainmenu isn't included in this


	void Start () 
	{
		qControl = FindObjectOfType<UnifiedQuizController>();
	}

	private void Update()
	{
		Debug.Log(level);
	}


	// called from unified quiz controller, passes in level that ended
	public void EndGame(int level)
	{
		this.level = level;
		levelPanel.SetActive(false);
		endScreen.SetActive(true);
		endImage.sprite = endSprites[level];
		StartCoroutine(PlayVO(endVoiceOver[level], level));
		avatar.sprite = avatars[level];
	}


	// called from buttons in main menu, passes in level int
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

	// called from avatar image button in end screen
	public void PlayVO2()
	{
		aSource.clip = endVoiceOver2[level];
		aSource.Play();
	}

	// called from X button in game and exit in the End screen
	public void QuitToMenu()
	{
		foreach(GameObject panel in gamePanels)
		{
			panel.SetActive(false);
		}
		mainMenu.SetActive(true);
	}
}