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
                this.NextQuestion.Content = "Результаты";
            }
        }

        private void Results(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"succes");
        }

        private void Answer_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;

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
                }
            }

            currentQuiz += 1;
        }
    }
}
