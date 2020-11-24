using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AcousticGuitarStrings
{
    /// <summary>
    /// Логика взаимодействия для QuizWindow.xaml
    /// </summary>
    public partial class QuizWindow : Window
    {
        ApplicationContext db;

        Quiz quiz = new Quiz();

        int currentQuiz = 0;

        int currentAnswer = 0;

        int totalAnswerWeight = 0;

        public QuizWindow()
        {
            InitializeComponent();

            db = new ApplicationContext();

            AddQuiz();
            
        }

        private void ToolBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void ButtonClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void ButtonMinimaze_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void NextQuestion_Click(object sender, RoutedEventArgs e)
        {
            this.ProgressBar.Value += 100 / quiz.QuestionAnswers.Count;
            AddQuiz();


            if (this.ProgressBar.Value >= 100)
            {
                this.NextQuestion.Click += Results;
                this.NextQuestion.Content = "Результати";
            }
            else
            {
                totalAnswerWeight += currentAnswer;
            }
        }

        private void Results(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"succes");
        }

        private void Answer_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;

            currentAnswer = Convert.ToInt32(radioButton.Name.Last().ToString());
        }

        private void AddQuiz()
        {
            if (currentQuiz <= quiz.QuestionAnswers.Count - 1)
                {

                TextBlock question = new TextBlock();

                question.Name = "Question";
                question.Style = this.FindResource("Question") as Style;

                foreach (var item in quiz.QuestionAnswers[currentQuiz])
                {
                    question.Text = item.Key;
                    this.QuestionBorder.Child = question;

                    this.AnswerPanel.Children.Clear();

                    foreach (var answer in item.Value)
                    {
                        AddAnswer(answer);
                    }
                }
            }

            currentQuiz += 1;
        }

        private void AddAnswer(KeyValuePair<string, int> answer)
        {
            Border answerBorder = new Border();
            answerBorder.Style = this.FindResource("AnswerBorder") as Style;

            RadioButton answerRadioButton = new RadioButton();
            answerRadioButton.Name += $"AnswerWeight_{answer.Value.ToString()}";
            answerRadioButton.Style = this.FindResource("AnswerRadioButton") as Style;
            answerRadioButton.Checked += Answer_Checked;

            TextBlock answerText = new TextBlock();
            answerText.Text = answer.Key;
            answerText.Style = this.FindResource("AnswerText") as Style;

            answerRadioButton.Content = answerText;

            answerBorder.Child = answerRadioButton;

            this.AnswerPanel.Children.Add(answerBorder);
        }
    }
}
