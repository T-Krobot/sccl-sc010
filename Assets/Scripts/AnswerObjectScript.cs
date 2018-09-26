using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AnswerObjectScript : MonoBehaviour 
{
    public CanvasGroup cg;                              // canvas group of the buttons, used to block raycasts while audio playing
    public AudioSource source;
    public UnifiedQuizController quizController;        // quiz controller
    public QuizModel.AnswerModel answer {get; set;}     // quiz model's answer class
    private Image img;                                  // UI image

    private void Awake()
    {
        img = GetComponent<Image>(); // grab attached image component
    }

    // this is called from the answer buttons in the level panel
    public void StartSubmission() 
    {
        // haven't tidied this up to stop specific coroutines yet, but it works without it so yay for now
        StopAllCoroutines();
        StartCoroutine(PlaySound());
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


    // plays sound and then sends answer to controller for verification
    IEnumerator PlaySound()
    {
        cg.blocksRaycasts = false;      // stops buttons being clickable. .interactable causes problems with the button states (pressed, hover etc).
        img.color = Color.gray;         // visual feedback for button being pressed during audio playback.
        source.clip = answer.audio;     // set clip
        source.Play();
        while(source.isPlaying)         // wait until finished
        {
            yield return null;
        }
        SendToController();             // verify answer
    }
}