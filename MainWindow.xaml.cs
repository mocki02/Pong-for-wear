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
using System.ComponentModel;
using System.Data;
using System.Drawing;

// Bibliotheken für Befehle

namespace Wpf_Game1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
     
        private bool goRight = true;                                                    //Ball Bewegungsbedingung
        private bool goDown = false;
        private readonly DispatcherTimer _playerTimer = new DispatcherTimer();          //Spieler Bewegungsbedingung
        private bool playerDown = false;
        private bool playerUp = false;       
        private bool player2Down = false;                                               //Player2 Bewegungsbedingung
        private int p = 0;
        private int z = 0;
        private double a = 200;
        private double b = 250;
        private double c = 200;
        private double d = 250;
        private double speed = 5;


        public MainWindow()
        {
            InitializeComponent();
            Player.KeyDown += Player_KeyDown;
                  // Timer für bewegbare Objekte
            _playerTimer.Interval = TimeSpan.FromMilliseconds(30);                      //Spieler Bewegungsgeschwindigkeit
            _playerTimer.Tick += _positioningPlayer;
            Player.DataContext = Player;
        }


        private void _positioningPlayer(object sender, EventArgs e)                     //Spieler Bewegungsbedingungen
        {
           a = Canvas.GetTop(Player);                                                   // Variable für Bewegungsrichtung
           b = Canvas.GetTop(Player);

            if (playerDown)                                                             // Was passiert, wenn playerDown = true
            {
                Canvas.SetTop(Player, a + 5);
            }
            if (playerUp)                                                               // Was passiert, wenn playerUp = true
            {
                Canvas.SetTop(Player, a - 5);
            }
            
            
            if (a >= Playground.ActualHeight - Player.ActualHeight)                     // damit Player das Spielfeld nicht verlässt
            {
                playerDown = false;
                playerUp = true;
            }
            else if (b <= 0)
            {
                playerDown = true;
                playerUp = false;
            } 

            //Player2 Bewegungsbedingungen

            c = Canvas.GetTop(Player2);
            d = Canvas.GetTop(Player2);

            if (player2Down)
            {
                Canvas.SetTop(Player2, c + 5);
            }
            else
            {
                Canvas.SetTop(Player2, c - 5);             
            }

            if (c >= Playground.ActualHeight - Player2.ActualHeight)
            {
                player2Down = false;
            }
            else if (d <= 0)
            {
                player2Down = true;
            }

             //Ball Bewegungsbedingungen

            var x = Canvas.GetLeft(Ball);
            var y = Canvas.GetTop(Ball);


            if (goRight)
            {
                Canvas.SetLeft(Ball, x + 5);
            }
            else
            {
                Canvas.SetLeft(Ball, x - 5);
            }

            if (goDown)
            {
                Canvas.SetTop(Ball, y + 5);
            }
            else
            {
                Canvas.SetTop(Ball, y - 5);
            }

            
            if (x >= Playground.ActualWidth - Ball.ActualWidth)
            {                                             
                _playerTimer.Stop();                                                    // damit alle Objekte stehen bleiben
                Canvas.SetTop(Ball, y = 205);                                           // setzt Ball zurück in die Ausgangsposition
                Canvas.SetLeft(Ball, x = 338);
                p += 1;                                                                 // Score Zähler für Spieler
                PlayerCount.Content = $"{p}";
                Exit.IsEnabled = true;                                                  // Der Beenden-Button kann wieder gedrückt werden
                goRight = false;                                                        // damit sich der Ball in der nächsten Runde in die andere Richtung bewegt
            }
            else if (x <= 0 - Ball.ActualWidth / 2)
            {
                _playerTimer.Stop();            
                Canvas.SetTop(Ball, y = 205);
                Canvas.SetLeft(Ball, x = 338);
                z += 1;                                                                 // Score Zähler für den Gegenspieler
                Player2Count.Content = $"{z}";
                Exit.IsEnabled = true;
                goRight = true;
            }

            if (y >= Playground.ActualHeight - Ball.ActualHeight)                       // damit der Ball von den Spielfeldgrenzen "abspringt"
            {
                goDown = false;
            }
            else if (y <= 0 - Ball.ActualHeight / 2)
            {
                goDown = true;
            }  
            

            if ((x <= 20) & (Ball.ActualHeight >= a) & (Ball.ActualHeight <= b))                 // damit der Ball von dem Spieler abspringt
            {
                goRight = true;
            }

            if ((x >= 640) & (Ball.ActualHeight >= c) & (Ball.ActualHeight <= d))               // damit der Ball vom Gegenspieler abspringt
            {
                goRight = false;
            }
        }

        private void StartStop_Click(object sender, RoutedEventArgs e)                  //Start-Stop Button Aktionen
        {

            if (_playerTimer.IsEnabled)
            {
                _playerTimer.Stop();
            }
            else
            {
                _playerTimer.Start();
            }
        }



        private void Exit_Click(object sender, RoutedEventArgs e)                   // was passiert, wenn man den Beenden-Button drückt
        {
            this.Close();                                                           // Schließt das Fenster
        }

       private void Player_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.W))
            {
                playerDown = false;
                playerUp = true;
            }

            if (Keyboard.IsKeyDown(Key.S))
            {
                playerDown = true;
                playerUp = false;
            }
        }
    }
}
