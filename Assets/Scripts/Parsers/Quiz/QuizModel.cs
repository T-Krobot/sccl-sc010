using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuizModel {

    public TextAsset csv;
    public List<SectionModel> sections;
    private int currentSection, currentQuestion;

    public QuizModel(TextAsset csv) {
        Parser_Quiz_Usual parser = new Parser_Quiz_Usual();
        List<ScenerySet> scenery = parser.LoadCSV(csv);
        sections = new List<SectionModel>();
        // Load everything into this quiz model
        foreach (ScenerySet scene in scenery) {
            List<ScenerySet.question> questions = scene.Questions_list;
            List<QuestionModel> finalQuestions = new List<QuestionModel>();
            
            foreach (ScenerySet.question q in questions) {
                List<AnswerModel> answers = new List<AnswerModel>();
                foreach (ScenerySet.option a in q.option_parts) {
                    AnswerModel answer = new AnswerModel(a.iscorrect,
                                                a.text,
                                                Resources.Load<Sprite>(a.image),
                                                Resources.Load<AudioClip>(a.audio));
                    answers.Add(answer);
                }
                QuestionModel question = new QuestionModel(q.question_text,
                                                    Resources.Load<Sprite>(q.question_image),
                                                    Resources.Load<AudioClip>(q.question_audio),
                                                    answers);
                finalQuestions.Add(question);
            }
            SectionModel s = new SectionModel(finalQuestions);
            sections.Add(s);
        }
    }
    
    public void ResetCurrentLevel()
    {
        currentQuestion = 0;
    }

    public bool HasPreviousQuestion() {
        return currentQuestion > 0;
    }

    public bool HasNextQuestion() {
        return currentQuestion < sections[currentSection].questions.Count - 1;
    }

    public bool HasNextSection() {
        return currentSection < sections.Count;
    }

    public QuestionModel GetCurrentQuestion() {
        return sections[currentSection].questions[currentQuestion];
    }

    public QuestionModel GetNextQuestion() {
        currentQuestion++;
        return sections[currentSection].questions[currentQuestion];
    } 

    public QuestionModel GetPreviousQuestion() {
        currentQuestion--;
        return sections[currentSection].questions[currentQuestion];
    }

    public bool IsAnswerCorrect(AnswerModel answer) {
        return answer.isCorrect;
    }


    public class SectionModel {
        public List<QuestionModel> questions;
        public SectionModel(List<QuestionModel> questions) {
            this.questions = questions;
        }
    }

    public class QuestionModel {
        public string text;
        public Sprite image;
        public AudioClip audio;
        public List<AnswerModel> answers;

        public QuestionModel(string text, Sprite image, AudioClip audio, List<AnswerModel> answers) {
            this.text = text;
            this.image = image;
            this.audio = audio;
            this.answers = answers;
        }

    }

    public class AnswerModel {
        
        public bool isCorrect;
        public Sprite image;
        public string text;
        public AudioClip audio;

        public AnswerModel(bool isCorrect, string text, Sprite image, AudioClip audio) {
            this.isCorrect = isCorrect;
            this.text = text;
            this.image = image;
            this.audio = audio;    
        }

    }

}


