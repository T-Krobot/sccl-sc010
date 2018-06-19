using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerObject : MonoBehaviour 
{
	// this can be attached to an object like a button or whatever else you are using.
	public QuizController qController;

	[HideInInspector]
	public bool isCorrect;
	[HideInInspector]
	public AudioClip answerAudioClip;
	
	void OnTriggerEnter2D(Collider2D other)
	{
		// if there are other colliders that might intersect then check other's tag and tag the bullet or w/e
		SubmitAnswer();
	}

	public void SubmitAnswer()
	{
		qController.ReceiveAnswer(isCorrect, answerAudioClip);
	}
}
