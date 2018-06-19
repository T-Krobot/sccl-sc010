using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Parser_Quiz_Usual {
    //change resources.Load target below to load a different file. Takes in a .csv file.
    // Parser. Can be made to return a list of Scenery Sets. For how to use, please look at test() below.
    //Uncomment the following line to change it to return a List<ScenerySet>. Also Uncomment the return statement and comment out the test function call at the bottom.
    //List<ScenerySet> Start(){
    
    public TextAsset quizQuestions;
    public List<ScenerySet> Start(string target=null)
    {
        if (target != null)
        {
            quizQuestions = (TextAsset)Resources.Load(target);//("Default_Quiz");
        }
        string[] items = quizQuestions.text.Split(new char[] { '\n' });
        List<ScenerySet> ListOfSets = new List<ScenerySet>();

        ScenerySet LatestScenery = new ScenerySet();
        foreach (string s in items)
        {
            if (s.Contains("\"")) // has a comma. will have magical " inside.
            {
                string[] holding = s.Split(new char[] { ',' });
                string concat1 = "";
                List<string> newvalues = new List<string>();
                bool hasreceivedpart = false;
                foreach (string possible in holding)
                {
                    if (possible.Contains("\"")) //this part is part of a comma in the middle branch!
                    {
                        if (!hasreceivedpart)
                        { // i have received the start of a "phrase"

                            concat1 = possible.Substring(1, possible.Length - 1);
                            hasreceivedpart = true;
                        }
                        else
                        { //this is the end of a phrase.
                            newvalues.Add(concat1 + "," + possible.Substring(0, possible.Length - 1));
                            concat1 = "";
                            hasreceivedpart = false;
                        }
                    }
                    else
                    {
                        if (hasreceivedpart)
                        {
                            concat1 += "," + possible;
                        }
                        else
                        {
                            newvalues.Add(possible);
                        }
                    }




                }
                if (newvalues[0] == "Section Name:" && LatestScenery.Instructions.textline != null)
                {
                    string[] linevalue = newvalues.ToArray();
                    ListOfSets.Add(LatestScenery);
                    LatestScenery = new ScenerySet();
                    LatestScenery.addPart(linevalue);
                }
                else { LatestScenery.addPart(newvalues.ToArray()); }

            }
            else //no comma in line. safe.
            {
                if (s != "")
                {
                    string[] linevalue = s.Split(',');
                    if (linevalue[0] == "") { } //Do absolutely nothing because i don't care about it. 
                    else
                    {
                        if (linevalue[0] == "Section Name:" && LatestScenery.Instructions != null)
                        {
                            ListOfSets.Add(LatestScenery);
                            LatestScenery = new ScenerySet();
                            LatestScenery.addPart(linevalue);
                        }
                        else { LatestScenery.addPart(linevalue); }


                    }
                }
            }

        }
        ListOfSets.Add(LatestScenery);
        test(ListOfSets[0]);
        test(ListOfSets[1]);
        return ListOfSets;

    }
    public void test(ScenerySet LatestScenery) { //test function
            Debug.Log("SECTION TEXT: " + LatestScenery.Section.textline); //gets the text that is associated with the section name.
                                                                              //returns string 

            Debug.Log("SECTION AUDIO: " + LatestScenery.Section.audio_loc); //Gets the audio file name associated with the section name.
                                                                                //returns string (file name)

            Debug.Log("SECTION IMAGE: " + LatestScenery.Section.image_loc); //Gets the image string associated with the section name
                                                                                //returns string (file name)

            Debug.Log("SECTION TRIES: " + LatestScenery.Section.no_of_tries); //Gets the number of tries the player has to complete the question. 
                                                                                // Defaults to 9999 if left blank.
                                                                                //returns int

            Debug.Log("SECTION POINTS: " + LatestScenery.Section.points_per_q); //Gets the points per question that should be awarded. 
                                                                                  //Defaults to 0 if left blank.
                                                                                  //returns int

            Debug.Log("SECTION DISPLAY: " + LatestScenery.Section.display_q_num); //Gets the boolean whether we should display the question number.
                                                                                    //Defaults to false, if not within { "T", "true", "True", "Yes", "yes", "y" , "t", "TRUE"}
                                                                                    //returns bool

            Debug.Log("SECTION TIME: " + LatestScenery.Section.timer_max); //Gets the timer field of this question. 
                                                                              //Defaults to 0 if there is no timer.
                                                                              //returns int


            Debug.Log("INSTRUCTIONS TEXT: " + LatestScenery.Instructions.textline); //Gets the text which should be contained in the text box.
                                                                                    //returns string


            Debug.Log("INSTRUCTIONS AUDIO: " + LatestScenery.Instructions.audio_loc); //Gets Audio of the instruction. Probably audio of someone reading it.
                                                                                      //returns string (file name)

            Debug.Log("INSTRUCTIONS IMAGE: " + LatestScenery.Instructions.image_loc); //Get Image which accompanies the instructions
                                                                                      //returns string (file name)

            Debug.Log("PAGE BACKGROUND AUDIO: " + LatestScenery.PageBackground_Audio); //Get Page audio.
                                                                                           //returns string (file name)

            Debug.Log("PAGE BACKGROUND BACKGROUND: " + LatestScenery.PageBackground_Image); //Get Page background. 

            Debug.Log("QUESTION BACKGROUND BACKGROUND: " + LatestScenery.QuestionBackground_Image); //Get question background
                                                                                                             //returns string(file name)

            Debug.Log("TOTAL NUMBER OF QUESTIONS: " + LatestScenery.Questions_list.Count);
            // Tell the total number of questions.

            
        //KEEP NOTE:
            ScenerySet.question SAMPLE_QUESTION = LatestScenery.Questions_list[0]; //An example of obtaining one question.

            string trash = "";
            foreach (int i in SAMPLE_QUESTION.correct_option)
            { //Echoing all the answers out. 
                trash += i.ToString();
                trash += ",";
            }
            Debug.Log("Question Answers: " + trash); //printed all question answers.
                                                     //Answers are all int





            trash = "";
            int counter = 0;
            foreach (ScenerySet.option current in SAMPLE_QUESTION.option_parts) //Echoing all the options.
            {
                //each option is class byitself. Containing all the following parts:
                //All options are contained in the List<option> that is returned. if you call LatestScenery.Questions().get(0).options().

                string record = "";
                record = "OPTION " + current.answer_number +  //returns option number.
                " - TEXT: " + current.text +  //returns a string
                "  AUDIO: " + current.audio +  //returns a string
                "  IMAGE: " + current.image + //returns a string
                "IS CORRECT OPTION: " + current.iscorrect + "\n"; //returns a bool to tell if the answer is correct


                //pictured above is how to obtain each of the attributes inside an option. 
                //The current answers store whether they are the correct answer, their image and text and audio components.
                //Keep in mind that just because I stored whether this option is correct, does not mean it is the ONLY option required to 
                //get the question correct and proceed! 
                //for that, see above on how to echo all question answers out. 

                trash += record;
                counter += 1;
            }
            Debug.Log("Options: " + trash);
            Debug.Log("Questiontext: " + SAMPLE_QUESTION.question_text); //the question's text is echoed.
            Debug.Log("Question IMAGE" + SAMPLE_QUESTION.question_image); //echo the submitted question image
            Debug.Log("Question audio " + SAMPLE_QUESTION.question_audio); //echo the submitted question's audio
    }

	// Update is called once per frame
	void Update () {
		
	}
}