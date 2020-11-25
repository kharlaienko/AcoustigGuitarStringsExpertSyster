using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Логика взаимодействия для StringsWindow.xaml
    /// </summary>
    public partial class StringsWindow : Window
    {
        ApplicationContext db;

        Quiz quiz = new Quiz();

        private int totalAnswerPoints { get; set; }

        public StringsWindow()
        {

        }
        public StringsWindow(int totalAnswerPoints)
        {
            InitializeComponent();

            db = new ApplicationContext();

            this.totalAnswerPoints = totalAnswerPoints;

            if (totalAnswerPoints == 1)
            {
                var selectecGuitarStrings = from t in quiz.guitarStrings where t.firstStringSize < 12 && t.material.ToUpper().StartsWith("Б") select t;

                AddSelectedStrings(selectecGuitarStrings);
            }
            else if (totalAnswerPoints == 2)
            {
                var selectecGuitarStrings = from t in quiz.guitarStrings where t.firstStringSize >= 12 && t.material.ToUpper().StartsWith("Б") select t;

                AddSelectedStrings(selectecGuitarStrings);
            }
            else if (totalAnswerPoints == 3)
            {
                var selectecGuitarStrings = from t in quiz.guitarStrings where t.firstStringSize < 12 && t.material.ToUpper().StartsWith("Ф") select t;
                
                AddSelectedStrings(selectecGuitarStrings);
            }
            else if (totalAnswerPoints == 4)
            {
                var selectecGuitarStrings = from t in quiz.guitarStrings where t.firstStringSize >= 12 && t.material.ToUpper().StartsWith("Ф") select t;

                AddSelectedStrings(selectecGuitarStrings);
            }

        }

        private void AddSelectedStrings(IEnumerable<GuitarString> selectecGuitarStrings)
        {
            foreach (var item in selectecGuitarStrings)
            {
                AddString(item);
            }
        }

        private void ToolBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void ButtonMinimaze_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void ButtonClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void AddString(GuitarString guitarString)
        {
            StackPanel stackPanel = new StackPanel();

            AddStringInfo("Назва: ", guitarString.name, stackPanel);
            AddStringInfo("Виробник: ", guitarString.manufacturer, stackPanel);
            AddStringInfo("Матеріал: ", guitarString.material, stackPanel);
            AddStringInfo("Ціна: ", $"{guitarString.price} грн.", stackPanel);
            AddStringInfo("Калібр струн: ", $"{guitarString.firstStringSize} - {guitarString.sixthStringSize}", stackPanel);

            Border borderInfo = new Border();
            borderInfo.Style = this.FindResource("InfoBorder") as Style;

            ScrollViewer scrollViewer = new ScrollViewer();
            scrollViewer.Style = this.FindResource("Scroll") as Style;

            scrollViewer.Content = stackPanel;

            borderInfo.Child = scrollViewer;

            Image image = new Image();
            image.Source = new BitmapImage(new Uri("pack://application:,,,/Images/Strings/" + guitarString.imageTitle + ".jpg"));

            Border imageBoder = new Border();
            imageBoder.Style = this.FindResource("ImageBorder") as Style;
            imageBoder.Child = image;

            Button button = new Button();
            button.Style = this.FindResource("BuyButton") as Style;
            button.Click += (sender, e) => { Process.Start(guitarString.buyUrl); };

            Grid grid = new Grid();
            grid.Style = this.FindResource("StringsInfoPannel") as Style;
            grid.Children.Add(imageBoder);
            grid.Children.Add(borderInfo);
            grid.Children.Add(button);

            Border itemBorder = new Border();
            itemBorder.Style = this.FindResource("ItemBorder") as Style;
            itemBorder.Child = grid;

            this.StringsPanel.Children.Add(itemBorder);
        }
        private void AddStringInfo(string type, string text, StackPanel stackPanel)
        {

            Border border = new Border();
            border.Style = this.FindResource("BoderInfo") as Style;

            TextBlock textBlock = new TextBlock();
            textBlock.Style = this.FindResource("TextGeneral") as Style;
            textBlock.Text += type + text;

            border.Child = textBlock;

            stackPanel.Children.Add(border);

        }
    }
}
