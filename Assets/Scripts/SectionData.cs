using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SectionData
{
	public string sectionAudio;
	public string textLine;
	public string sectionImage;
	public int numberOfTries;
	public int pointsPerQ;
	public int timerMax;
	public List<QuestionData> questions = new List<QuestionData>();
}
