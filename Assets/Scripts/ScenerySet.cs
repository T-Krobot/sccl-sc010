using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class ScenerySet{
    //holds the entire form. Every section composes of one scenery set.
    public Sectionset Section=null;
    public InstructionsSet Instructions=null;   
    public List<question> Questions_list = new List<question>();
    public string PageBackground_Audio;
    public string PageBackground_Image;
    public string QuestionBackground_Image;
    public bool errorflag = false;

    public void addPart(string[] values) {
        List<string> valueline = new List<string>(values);
        if (valueline[0] == "Section Name:") {
            string trash = valueline[0]; //remove first cell that tells me it is a section part.
            valueline.RemoveAt(0);
            this.Section = new Sectionset(valueline);
        }
        if (valueline[0] == "Instructions:") {
            string trash = valueline[0]; //remove first cell that tells me it is a instruction part.
            valueline.RemoveAt(0);
            this.Instructions = new InstructionsSet(valueline);
        }
        if (valueline[0] == "Page Background:") {
            this.PageBackground_Audio = valueline[2]; //save value
            this.PageBackground_Image = valueline[3]; //save value
        }
        if (valueline[0] == "Question Background:") {
            this.QuestionBackground_Image = valueline[3]; //save value
        }
        if (valueline[0] == "Question:") {
            string trash = valueline[0]; //remove the first cell which tells me it was a question.
            valueline.RemoveAt(0);
            this.Questions_list.Add(new question(valueline)); //add it into the list.
        }
    }

    public class InstructionsSet
    {
        public string textline;
        public string audio_loc;
        public string image_loc;
        public InstructionsSet(List<string> set)
        {
            textline = set[0];
            audio_loc = set[1];  //saved all values.
            image_loc = set[2];
        }
       
    }
    public class question //has a parser in it.
    {
        public string question_text; //question text.
        public string question_audio; //question audio_file location
        public string question_image; //question image file location
        public List<int> correct_option = new List<int>(); //List of correct options.
        public List<option> option_parts; //List of options. Each option is a class by itself.

        public question(List<string> a)
        {
            option_parts = new List<option>(); //create the parts. based on the template. (already has the first box removed at this point.
            this.question_text = a[0];
            this.question_audio = a[1];
            this.question_image = a[2];
            a.RemoveAt(0);
            a.RemoveAt(0); //saved all 3 strings. Remove them.. should be okay in terms of shallow and deep copy..
            a.RemoveAt(0);
            int counter = 1;
            while (true)
            {

                if ((a.Count - 4) < 0)
                { //i.e i have reached the correct answer part...
                    string garbage = a[0];
                    a.RemoveAt(0);
                    string answer_array = a[0];
                    string[] temp = answer_array.Split(new char[] { ',' }); //Answers are split by . for multiple answer cases.
                    foreach (string answers in temp)
                    {
                        int converted;
                        int.TryParse(answers, out converted);
                        this.correct_option.Add(converted); // attempt to parse into an integer and then push it into the list..
                    }
                    int final_int = 1;
                    foreach (option current_focus in this.option_parts)
                    {
                        if (this.correct_option.Contains(final_int))
                        {
                            current_focus.makeright();
                        }
                        else
                        {
                            current_focus.makewrong();
                        }
                        final_int += 1;
                    }
                    break;
                }
                else
                {
                    string pop1 = a[0]; // simply says answer number. It last time would say "Answer 1:" or "Answer 2:". Changed it to say a number only.
                    string pop2 = a[1]; //text prompt of option
                    string pop3 = a[2]; //audio associated with option
                    string pop4 = a[3]; //image associated with option
                    option_parts.Add(new option(counter, pop2, pop3, pop4)); //push into option class.
                    counter += 1;
                    a.RemoveAt(0);
                    a.RemoveAt(0);
                    a.RemoveAt(0); //remove all 4, loop to check next move.
                    a.RemoveAt(0);
                }
            }
        }
    }
    public class option
    {
        public int answer_number;
        public string text;
        public string audio;
        public string image;
        public bool iscorrect;
        public option(int option, string given_text, string given_audio, string given_image)
        {
            this.text = given_text;
            this.audio = given_audio;
            this.image = given_image;
            this.answer_number = option;
        }
        public void makewrong() { this.iscorrect = false; }//method to set that this question is indeed incorrect.
        public void makeright() { this.iscorrect = true; }//method to set that this question is indeed correct.
    }
    public class Sectionset
    {
        public string textline=null;//return appropriate text
        public string audio_loc;//return audio location
        public string image_loc;//return image location
        public int no_of_tries; //return tries per question
        public int points_per_q;//return points per question
        public bool display_q_num;//display queue number
        public int timer_max;//get timer.

        public Sectionset(List<string> a) //constructor
        {
            this.textline = a[0];  //save the text
            this.audio_loc = a[1];  //save the audio file name
            this.image_loc = a[2]; //save the background file name
            if (a[3] != "")
            {
                int.TryParse(a[3], out this.no_of_tries); //save the no_of_tries, if applicable. else, save as 9999
            }
            else { this.no_of_tries = 9999; }

            if (a[4] != "")
            {
                int.TryParse(a[4], out this.points_per_q);  //save number of points per question. Defaults to zero otherwise.
            }
            else { this.points_per_q = 0; }


            if (a[5] == "T" || a[5] == "true" || a[5] == "True" || a[5] == "Yes" || a[5] == "yes" || a[5] == "y" || a[5] == "t" || a[5] == "TRUE")
            {
                //accepts the following as true: T, true, True, Yes, yes, y ,t.
                this.display_q_num = true;
            }
            else
            {
                this.display_q_num = false;
            }
            if (a[6] == "")
            {
                this.timer_max = 0;  //If it is blank, set to zero. 
            }
            else
            {
                int.TryParse(a[6], out this.timer_max);  //else, save that as a number.
            }

        }
    }
}
