using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// this quiz controller is used for all 3 levels.
public class UnifiedQuizController : MonoBehaviour
{

    public TextAsset[] csv = new TextAsset[3];					// the csv files for each quiz
    public QuizModel[] model = new QuizModel[3];				// quiz model for each
   
	public Image questionDisplayable;							// UI image for question images
	public Text questionText;									// text for question text
	public AnswerObjectScript[] answerDisplayables;				// list of answer objects (buttons in this case), 3 of them

	private int currentLevel;									// current level, 0-2
	public GameController gController;							// game controller, handles all the non quiz stuff. main menu, end screen etc.

	// Audio stuff
	public AudioClip wrongAnswerSFX;							// played when answered wrongly
	public AudioClip wrongAnswerVO;								// played after wrongAnswerSFX
	public AudioClip rightAnswer;								// played when answered correctly
	public AudioSource audioSource;

	
	public void Start()
	{
		// parse CSVs
		for(int i = 0; i < model.Length; i++)
		{
			model[i] = new QuizModel(csv[i]);
		}
	}

	private void UpdateQuestionDisplayable(QuizModel.QuestionModel question)
	{
		questionText.text = question.text;
        questionDisplayable.sprite = question.image;
	}

	private void UpdateAnswerDisplayables(QuizModel.QuestionModel question)
	{
		for (int i = 0; i < question.answers.Count; i++)
		{
			answerDisplayables[i].answer = question.answers[i];
			answerDisplayables[i].UpdateSelf();
		}
	}

	// called from AnswerObjectScript.cs
	public void ReceiveAnswer(bool isCorrect) 
	{
		if (isCorrect) 
		{
			if(model[currentLevel].HasNextQuestion())
			{
				print("Correct");
				StopAllCoroutines();
				StartCoroutine(RightAnswer());
				UpdateAnswerDisplayables(model[currentLevel].GetNextQuestion());
				UpdateQuestionDisplayable(model[currentLevel].GetCurrentQuestion());

			}
			else
			{
				gController.EndGame(currentLevel);
				StopAllCoroutines();
			}
		}
		else 
		{
			print("Wrong");
			StopAllCoroutines();
			StartCoroutine(WrongAnswer());
		}
	}

	// called from GameController.cs
	public void SetLevel(int level)
	{
		currentLevel = level;
		model[currentLevel].ResetCurrentLevel();
		UpdateQuestionDisplayable(model[currentLevel].GetCurrentQuestion());
		UpdateAnswerDisplayables(model[currentLevel].GetCurrentQuestion());
	}

	IEnumerator WrongAnswer()
	{
		audioSource.clip = wrongAnswerSFX;
		audioSource.Play();

		while(audioSource.isPlaying)
		{
			yield return null;
		}
	}

	IEnumerator RightAnswer()
	{
		audioSource.clip = rightAnswer;
		audioSource.Play();
		while(audioSource.isPlaying)
		{
			yield return null;
		}
	}
}