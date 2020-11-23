using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;


namespace AcousticGuitarStrings
{
    class Quiz
    {
        static ApplicationContext db = new ApplicationContext();

        private Question[] questions = db.Questions.ToArray<Question>();

        private Answer[] answers = db.Answers.ToArray<Answer>();

        public List<Dictionary<string, Dictionary<string, int>>> QuestionAnswers = new List<Dictionary<string, Dictionary<string, int>>>();

        public Quiz() { MakeQuiz(); }

        private void MakeQuiz()
        {
            for (int i = 0; i < questions.Length; i++)
            {
                string quesiton = questions[i].text;

                quesiton = char.ToUpper(quesiton[0]) + quesiton.Substring(1);

                Dictionary<string, int> answerAndWeight = new Dictionary<string, int>();

                foreach (var answer in answers)
                {
                    if (answer.questionID == questions[i].id)
                    {
                        answerAndWeight.Add(answer.text, answer.weight);
                    }
                }

                Dictionary<string, Dictionary<string, int>> readyQuiz = new Dictionary<string, Dictionary<string, int>>();

                readyQuiz.Add(quesiton, answerAndWeight);

                QuestionAnswers.Add(readyQuiz);
                
            }

        }

    }


    class Question
    {
        public int id { get; set; }
        public string text { get; set; }

        public Question() { }

    }

    class Answer
    { 
        public int id { get; set; }
        
        public int questionID { get; set; }
        
        public string text { get; set; }
        
        public int weight { get; set; }
        public Answer() { }

    }
}
