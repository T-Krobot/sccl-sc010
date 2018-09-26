using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/***** EXAMPLE ANSWER OBJECT CLASS */
/*
 * Create a new answer object following this template and attach it to the object representing you answer.
 * Edit the UpdateSelf function to reflect the necessary components (sprites, audio, etc)
 */


public class AnswerObjectScript : MonoBehaviour 
{
    public CanvasGroup cg;
    public AudioSource source;
    public UnifiedQuizController quizController;
    public QuizModel.AnswerModel answer {get; set;}
    private Image img;

    private void Awake()
    {
        img = GetComponent<Image>();
    }
    public void StartSubmission() 
    {
        StopAllCoroutines();
        StartCoroutine(PlaySound());
        Debug.Log(answer.audio);
    }

    public void UpdateSelf() {
        // Update the necessary components of the answer 
        GetComponentInChildren<Text>().text = answer.text;
    }

    private void SendToController()
    {
        cg.blocksRaycasts = true;
        img.color = Color.white;
        quizController.ReceiveAnswer(answer.isCorrect);
    }


    IEnumerator PlaySound()
    {
        cg.blocksRaycasts = false;
        img.color = Color.gray;
        source.clip = answer.audio;
        source.Play();
        while(source.isPlaying)
        {
            yield return null;
        }
        SendToController();
    }
}