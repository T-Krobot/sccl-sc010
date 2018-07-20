using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizController : MonoBehaviour 
{
	public Parser_Quiz_Usual pqUsual;								// reference to parser
	public Text questionDisplay;									// text to display question text
	public QuizDataStorer quizDataStorer;							// reference to the script that stores quiz data in classes
	public GameObject[] answerObject;								// the objects that will be used for answers. buttons, asteroids, whatever
	public Image questionImage;										// if the question has an image attached then can output it here if you want

	// UI panels
	public GameObject mainMenuPanel;
	public GameObject levelPanel;

	public Text levelTitle;
	
	public AudioSource sauce;
	public AudioClip wrongAnswerSFX;
	public AudioClip wrongAnswerVO;
	public AudioClip rightAnswerSFX;
	public AudioClip rightAnswerVO;

	public AudioClip[] levelDescriptions;

	public RewardsScreen[] rewardsScreen;

	private int questionNum = 0;									// current question in round.
	private int maxQuestions;										// set max number of questions in the current round.
	private int roundNumber = 0;										// the round (section) to grab info from. if the app has multiple quizs that use the same parser this will be used for getting level 1 questions or level 2 etc.
	
	public Sprite[] rewardImages;

	private List<ScenerySet> listOfScenerySets;
	List<QuizData> listofQdatas;

	// reward screen UI stuff
	public GameObject chestPanel;
	public GameObject rewardPanel;
	public Image rewardImage;
	public Image rewardLevelImage;
	public Image rewardAvatar;

	//private int tempInt = 0; // for testing purposes


	void Start () 
	{
		//UpdateQuestions();											// show the first question. IF YOU GET NULL REFERENCE OR OUT OF RANGE this might be because this is trying to display a question before the quizDataStorer has finished, either make this script execute after with code execution order in unity or idk maybe use a startbutton instead of calling from start
		//SetMaxRounds();												// call to set max rounds
		//listOfScenerySets = pqUsual.Start();
		Debug.Log(listOfScenerySets.Count);
		quizDataStorer.StoreQuizData(listOfScenerySets);
		
	}

	void ResetVariables()
	{
		// reset round vars if you need to
		questionNum = 0;	
		maxQuestions = 0;		
		roundNumber = 0;									
	}

	void SetMaxRounds()
	{
		maxQuestions = quizDataStorer.qData.sectionData[roundNumber].questions.Count - 1;		// syncing up rounds
	}
	

	void UpdateQuestions()											// pulls question and related answers, images and audio at [roundNum]
	{
		if(questionNum <= maxQuestions)
		{
			questionDisplay.text = quizDataStorer.qData.sectionData[roundNumber].questions[questionNum].questionText;									// set question text
			questionImage.sprite = Resources.Load<Sprite>(quizDataStorer.qData.sectionData[roundNumber].questions[questionNum].imagePath);				// set question image
			Debug.Log(quizDataStorer.qData.sectionData[roundNumber].questions[questionNum].imagePath);
			
			for(int i = 0; i < answerObject.Length; i++)		// as many answer objects as there are, set text and stuff
			{
				var aData = quizDataStorer.qData.sectionData[roundNumber].questions[questionNum].answers[i];
				answerObject[i].GetComponentInChildren<Text>().text = aData.answerText;		// set answer text. looks for text in children
				answerObject[i].GetComponent<AnswerObject>().isCorrect = aData.isCorrect;	// set the isCorrect bool
				answerObject[i].GetComponent<Image>().sprite = Resources.Load<Sprite>(aData.imagePath);
				answerObject[i].GetComponent<AnswerObject>().answerAudioClip = Resources.Load<AudioClip>(aData.audioPath);
			}

		}
		else
		{
			Debug.Log("out of questions");			// round over
			ShowChestScreen(roundNumber);
		}
		
	}

	public void ReceiveAnswer(bool isCorrect, AudioClip answerAudio)
	{
		StopAllCoroutines();
		sauce.Stop();
		if(isCorrect)
		{
			
			//StartCoroutine(PlayCorrectAudio(rightAnswerSFX));
			StartCoroutine(PlayCorrectAudio(answerAudio));
			Debug.Log("yes");
			questionNum++;
			UpdateQuestions();						// if answer is correct then we show next question
		}
		else
		{
			StartCoroutine(PlayWrongAudioPrompt());
			Debug.Log("no");						// if incorrect do nothing. can just go to next question anyway but deduct/not award score or smth
			//StartCoroutine(PlayWrongAudioPrompt());
		}
	}



	// get rid of roundNum++ in ReceiveAnswer if you use this
	IEnumerator QuestionCooldown()
	{
		questionNum++;
		yield return new WaitForSeconds(2f);
		UpdateQuestions();
	}

	public void TestMethod()
	{
		
	}

	public void SetLevel(int level)
	{
		sauce.clip = levelDescriptions[level];
		sauce.Play();
		roundNumber = level;
		mainMenuPanel.SetActive(false);
		levelPanel.SetActive(true);
		SetMaxRounds();
		UpdateQuestions();
		if(levelTitle){ levelTitle.text = "Level: " + (level + 1); }
	}

	public void QuitToMenu(GameObject lastPanel)
	{
		mainMenuPanel.SetActive(true);
		lastPanel.SetActive(false);
		ResetVariables();
		StopAllCoroutines();
		sauce.Stop();
	}

	public void ToggleBGM()
	{
		// if true stop, if false start idk
	}

	IEnumerator PlayWrongAudioPrompt()
	{
		sauce.clip = wrongAnswerSFX;
		sauce.Play();
		yield return new WaitWhile(()=> sauce.isPlaying);
		sauce.clip = wrongAnswerVO;
		sauce.Play();
	}

	IEnumerator PlayCorrectAudio(AudioClip audioClip)
	{
		sauce.clip = rightAnswerSFX;
		sauce.Play();
		yield return new WaitWhile(()=> sauce.isPlaying);
		sauce.clip = audioClip;
		sauce.Play();
	}


	public void ShowChestScreen(int levelNumber)
	{
		var rs = rewardsScreen[levelNumber];
		levelPanel.SetActive(false);
		chestPanel.SetActive(true);
		rewardAvatar.sprite = rs.avatar;
		rewardLevelImage.sprite = rs.levelImage;
	}

	public void ShowRewardsScreen()
	{
		chestPanel.SetActive(false);
		rewardPanel.SetActive(true);
		rewardImage.sprite = rewardImages[roundNumber];
	}
}

[System.Serializable]
public class RewardsScreen
{
	public string name;
	public Sprite levelImage;
	public Sprite avatar;
}