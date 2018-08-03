using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UnifiedQuizController : MonoBehaviour
{

    public TextAsset[] csv = new TextAsset[3];
    public QuizModel[] model = new QuizModel[3];
   
	// Change GameOBject to your new question object class
	public Image questionDisplayable;
	public Text questionText;
	// Change ExampleAnswerObject to your new answer object class
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
        model[0] = new QuizModel(csv[0]);
        model[1] = new QuizModel(csv[1]);
        model[2] = new QuizModel(csv[2]);
	}

	private void UpdateQuestionDisplayable(QuizModel.QuestionModel question)
	{
		// Example usage of question properties
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
	}
}