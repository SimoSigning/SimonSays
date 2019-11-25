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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SimonSays
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        List<string> comparissonContent = new List<string>();
        List<string> clickContent = new List<string>();
        bool failed = false;
        int incrementer = 0;
        int timerstop = 0;
        public MainWindow()
        {
            InitializeComponent();
            NumberGen();
            RefreshLists();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Looper;
        }
        private void NumberGen()
        {
            Random ranGen = new Random();
            comparissonContent.Add(ranGen.Next(1, 5).ToString());
        }
        private void LoopThrough()
        {
            timerstop = comparissonContent.Count;
            timer.Start();
        }
        void Looper(object sender, EventArgs e)
        {
            testlabel.Content = incrementer.ToString();
            ClearColours();
            if (incrementer < timerstop)
            {
                switch(comparissonContent[incrementer])
                {
                    case "1":
                        One.Background = Brushes.DarkRed;
                        NumberList.Items.Add("1");
                        break;
                    case "2":
                        Two.Background = Brushes.DarkGreen;
                        NumberList.Items.Add("2");
                        break;
                    case "3":
                        Three.Background =  Brushes.DarkBlue;
                        NumberList.Items.Add("3");
                        break;
                    case "4":
                        Four.Background = Brushes.Orange;
                        NumberList.Items.Add("4");
                        break;
                    default: break;
                }
                incrementer++;
            }
            else
            {
            timer.Stop();
            incrementer = 0;
            NumberList.Items.Clear();
            clickContent.Clear();
            NumberGen();
            RefreshLists();
            }
        }
        private void ClearColours()
        {
            One.Background = Brushes.Red;
            Two.Background = Brushes.Green;
            Three.Background = Brushes.Blue;
            Four.Background = Brushes.Yellow;
        }

        private void Compare()
        {
            for (int i = 0; i < clickContent.Count; i++)
            {
                if(clickContent[i] != comparissonContent[i])
                {
                    //i princippet kan den foretage afbrydningsproceduren allerede hernede..
                    failed = true;
                    // den behøver egentlig ikke break;
                    break;
                }
            }
        }
        private void FillClickCont(string Number)
        {
            if(clickContent.Count != comparissonContent.Count-1)
            {
                clickContent.Add(Number);
                Compare();
                //den skal afgøre om den skal fortsætte eller ej med det samme efter den har compared.
                if(failed == true)
                {
                    testlabel.Content = "Failed";
                    //og så skal den afbryde processen evt ved at sende en til en failed page..
                }
            }
            else
            {
                LoopThrough();
            }
        }
        private void OneClick(object sender, RoutedEventArgs e)
        {
            Keyboard.ClearFocus();
            FillClickCont("1");
        }
        private void TwoClick(object sender, RoutedEventArgs e)
        {
            Keyboard.ClearFocus();
            FillClickCont("2");
        }
        private void ThreeClick(object sender, RoutedEventArgs e)
        {
            Keyboard.ClearFocus();
            FillClickCont("3");
        }
        private void FourClick(object sender, RoutedEventArgs e)
        {
            Keyboard.ClearFocus();
            FillClickCont("4");
        }
        private void RefreshLists()
        {
            ClearConList();
            ClearGenList();
            FillConList();
            FillGenList();
        }
        private void FillGenList()
        {
            for (int i = 0; i < clickContent.Count; i++)
            {
                GeneratedContent.Items.Add(clickContent[i]);
            }
        }
        private void FillConList()
        {
            for (int i = 0; i < comparissonContent.Count; i++)
            {
                CurrentContent.Items.Add(comparissonContent[i]);
            }
        }
        private void ClearGenList()
        {
            GeneratedContent.Items.Clear();
        }
        private void ClearConList()
        {
            CurrentContent.Items.Clear();
        }
    }
}
