using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuizDataStorer 
{
	public QuizData qData;
    //private int h = 0;

	public void StoreQuizData(List<ScenerySet> latestScenery)
    {
        
        for(int h = 0; h < latestScenery.Count; h++)
        {
            Debug.Log(latestScenery[h].Questions_list.Count);

            SectionData newSection = new SectionData();

            newSection.numberOfTries = latestScenery[h].Section.no_of_tries;
            newSection.pointsPerQ = latestScenery[h].Section.points_per_q;
            newSection.sectionAudio = latestScenery[h].Section.audio_loc;
            newSection.sectionImage = latestScenery[h].Section.image_loc;
            newSection.textLine = latestScenery[h].Section.textline;
            newSection.timerMax = latestScenery[h].Section.timer_max;
            qData.sectionData.Add(newSection);
        
            for(int i = 0; i < latestScenery[h].Questions_list.Count; i++)
            {   // storing question data, can add fields to QuestionData class and then add lines here
                QuestionData q = new QuestionData();
                q.questionText = latestScenery[h].Questions_list[i].question_text;
                q.imagePath = latestScenery[h].Questions_list[i].question_image;
                q.audioPath = latestScenery[h].Questions_list[i].question_audio;
                q.questionNum = i;
                for(int j = 0; j < latestScenery[h].Questions_list[i].option_parts.Count; j++)
                {   // can add extra data for answers here. need to add fields to Answerdata first
                    Answerdata a = new Answerdata();
                    a.answerText = latestScenery[h].Questions_list[i].option_parts[j].text;
                    a.imagePath = latestScenery[h].Questions_list[i].option_parts[j].image;
                    a.audioPath = latestScenery[h].Questions_list[i].option_parts[j].audio;
                    a.answerNum = latestScenery[h].Questions_list[i].option_parts[j].answer_number;
                    a.isCorrect = latestScenery[h].Questions_list[i].option_parts[j].iscorrect;

                    q.answers.Add(a);

                }

                qData.sectionData[h].questions.Add(q);
            }
    }
    
    }

    public void TestMethod2()
    {
        Debug.Log("tmethod 2");
    }
}
