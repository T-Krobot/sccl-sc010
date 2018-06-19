using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class questionAnswerData 
{
	public int questionNumber;				// number of the current question
	public string question;
	public List<string> answers = new List<string>();
	public int correctAnswer;				// the correct answer number

	public Sprite questionImage;
}
