using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSVParse : MonoBehaviour {
	/*
	The file you are loading must be in a folder called Resources
	If using TSV files, you need to change their extension to .csv first as Unity doesn't natively recognise tsv files.
	
	 */

	public string fileName;
	public List<questionAnswerData> qNa = new List<questionAnswerData>();

	void Start () 
	{
		TextAsset quizData = Resources.Load<TextAsset>(fileName);
		string[] rawCSVData = quizData.text.Split(new char[] {'\n' });	
		for(int i = 1; i < rawCSVData.Length - 1; i++)
		{
			string[] row = rawCSVData[i].Split(new char[] { '\t' }); // change to ',' if using real csv instead of tsv
			questionAnswerData q = new questionAnswerData();
			

			if(row[1] != "")
			{
				int.TryParse(row[0], out q.questionNumber);
				int.TryParse(row[2], out q.correctAnswer);
				q.question = row[1];
				q.correctAnswer--;						// to make correct answer number match answer element (starts at 0)
				for(int aCol = 3; aCol < row.Length; aCol++)
				{
					if(row[aCol] != "")
					{
						q.answers.Add(row[aCol]);
					}
				}
				qNa.Add(q);
			}
		}
	}
	
	void Update () {
		
	}
}
