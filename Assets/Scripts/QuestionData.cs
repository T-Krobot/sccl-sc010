using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestionData 
{
	public string questionText;
	public string audioPath;
	public string imagePath;
	public int questionNum;
	public List<Answerdata> answers = new List<Answerdata>();
}
