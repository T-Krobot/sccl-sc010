using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour 
{

	private UnifiedQuizController qControl;

	void Start () 
	{
		qControl = FindObjectOfType<UnifiedQuizController>();
	}
	
	void Update () 
	{
		
	}


	public void LoadQuizLevel()
	{

	}
}
