using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelSelectButtonScript : MonoBehaviour 
{
	public QuizController qControl;
	public int level;

	public void	SendLevelNumber()
	{
		qControl.SetLevel(level);
	}
}
