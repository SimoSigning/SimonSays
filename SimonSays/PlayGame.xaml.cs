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
    /// Interaction logic for PlayGame.xaml
    /// </summary>
    public partial class PlayGame : Page
    {
        DispatcherTimer timer = new DispatcherTimer();
        List<string> comparissonContent = new List<string>();
        List<string> clickContent = new List<string>();
        int incrementer = 0;
        int timerstop = 0;
        bool IsLooping = false;
        bool FirstIteration = true;
        public PlayGame()
        {
            InitializeComponent();
            NumberGen();
            NextNumber.Content = comparissonContent[0];
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
            ClearColours();
            if (incrementer < timerstop)
            {
                switch (comparissonContent[incrementer])
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
                        Three.Background = Brushes.DarkBlue;
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
                ButtonClicks.Items.Clear();
                clickContent.Clear();
                IsLooping = false;
                NumberGen();
                NextNumber.Content = comparissonContent[comparissonContent.Count - 1];
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
                if (clickContent[i] != comparissonContent[i])
                {
                    NavigationService.Navigate(new Uri("Failed.xaml", UriKind.Relative));
                }
            }
        }
        private void FillClickCont(string Number)
        {
            if (clickContent.Count != comparissonContent.Count-1)
            {
                clickContent.Add(Number);
                Compare();
                ButtonClicks.Items.Add(Number);
            }
            else
            {
                if(FirstIteration == true)
                {
                    FirstIteration = false;
                    clickContent.Add(Number);
                    Compare();
                }
                ButtonClicks.Items.Add(Number);
                IsLooping = true;
                LoopThrough();
            }
        }
        private void OneClick(object sender, RoutedEventArgs e)
        {
            if(IsLooping == false)
            {
                Keyboard.ClearFocus();
                FillClickCont("1");
            }
        }
        private void TwoClick(object sender, RoutedEventArgs e)
        {
            if (IsLooping == false)
            {
                Keyboard.ClearFocus();
                FillClickCont("2");
            }
        }
        private void ThreeClick(object sender, RoutedEventArgs e)
        {
            if (IsLooping == false)
            {
                Keyboard.ClearFocus();
                FillClickCont("3");
            }
        }
        private void FourClick(object sender, RoutedEventArgs e)
        {
            if (IsLooping == false)
            {
                Keyboard.ClearFocus();
                FillClickCont("4");
            }
        }
        /*
                     //skal gøre så hvis man klikker på knapper når looperen køre så får man fejl.
            //Endnu bedre vil være hvis man kan gøre så man ikke kan klikke på knapper når looperen
            //køre.
            /*
             Jeg skal også lave brugergrænsefladen bedre. 

            Man kan også klikke løs på knapper før man overhovedet har fået et tal. 
            Generelt skal man kun kunne klikke på knapper når der er et nyt tal i rækken
            og man skal indtaste den gamle talrække igen.

            FillClickCont er der hvor vi bestemmer hvad der skal ske når der er klikket på en knap.
            Derfor må det være der vi sætter betingelser for når man klikker på knapper på
            tidspunkter hvor man ikke skal klikke på knapper men observere de knapper man har klikket
            på samt rækken af tal.
             */
         
    }
}
