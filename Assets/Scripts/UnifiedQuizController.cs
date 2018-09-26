using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// this quiz controller is used for all 3 levels.
public class UnifiedQuizController : MonoBehaviour
{

    public TextAsset[] csv = new TextAsset[3];
    public QuizModel[] model = new QuizModel[3];
   
	public Image questionDisplayable;
	public Text questionText;
	public AnswerObjectScript[] answerDisplayables;

	private int currentLevel;
	public GameController gController;

	// Audio stuff
	public AudioClip wrongAnswerSFX;
	public AudioClip wrongAnswerVO;
	public AudioClip rightAnswer;
	public AudioSource audioSource;

	public CanvasGroup buttonCanvasGroup;
	
	public void Start()
	{
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

	private void DisableAnswerButtons()
	{
		buttonCanvasGroup.interactable = !buttonCanvasGroup.interactable;
		Debug.Log("disable interactable");
	}
}