using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempTestingScript : MonoBehaviour {

	public GameObject[] allObjects;
	public GameObject mainMenu;

	void Start () 
	{
		for(int i = 0; i < allObjects.Length; i++)
		{
			allObjects[i].SetActive(false);		
		}
		mainMenu.SetActive(true);
	}
}
