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

using System.Threading;
using System.Windows.Threading;

namespace PingPong
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer;
        double positionY;
        double positionX;

        double speedY;
        double speedX;
        double directionY;
        double directionX;

        int score;


        public MainWindow()
        {
            InitializeComponent();

            NewGame();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (Canvas.GetTop(ball) < 1)                    // if ball hit on top side
            {
                directionY = speedY;                        // ball move in positive direction
            }
            if (Canvas.GetLeft(ball) < 1)                   // if ball hit on left side
            {
                directionX = speedX;                        // ball move in positive direction
                if (directionX < 0)
                {
                    directionX = 1;
                }
            }
            if (Canvas.GetLeft(ball) > this.Width - 40)     // if ball hit on right side
            {
                directionX = -speedX;                       // ball move in negative direction
                if (directionX > 0)
                {
                    directionX = -1;
                }
            }
            if (Canvas.GetTop(ball) > Canvas.GetTop(racket) + 25)
            {
                timer.Stop();
                MessageBox.Show("GAME OVER. Press F1 for new game");
            }


            if (Canvas.GetTop(ball) > Canvas.GetTop(racket) - 16 && (Canvas.GetLeft(ball) + 8) > Canvas.GetLeft(racket) && (Canvas.GetLeft(ball) - 8) < Canvas.GetLeft(racket) + 100)
            {
                if ((Canvas.GetLeft(ball) + 8) < Canvas.GetLeft(racket) + 33) // if ball hit left part of rocket
                {
                    if (directionX < 0)
                    {
                        speedX += 0.5;
                        if (speedX > 0)
                        {
                            directionX = -0.5;
                        }
                    }
                    else
                    {
                        speedX -= 0.5;
                        if (speedX < 0)
                        {
                            directionX = 0.5;
                        }
                    }
                }
                else if ((Canvas.GetLeft(ball) + 8) > Canvas.GetLeft(racket) + 66)
                {
                    if (directionX < 0)
                    {
                        speedX -= 0.5;
                        if (speedX > 0)
                        {
                            directionX -= 0.5;
                        }
                    }
                    else
                    {
                        speedX += 0.5;
                        if (speedX < 0)
                        {
                            directionX = 0.5;
                        }
                    }
                }

                score++;
                this.Title = "PingPong         SCORE: " + score.ToString();
                //Console.Beep(770, 25);
                speedX += 0.1;
                speedY += 0.1;

                directionY = -speedY;         // negative speed before hit

                if (directionX < 0)
                {
                    directionX = -speedX;
                }
                else
                {
                    directionX = speedX;
                }
            }

            positionY += directionY;
            positionX += directionX;
            Canvas.SetLeft(ball, positionX);
            Canvas.SetTop(ball, positionY);


        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            Point point = e.MouseDevice.GetPosition(canvas);

            if (e.MouseDevice.GetPosition(canvas).X > racket.Width / 2 && e.MouseDevice.GetPosition(canvas).X < this.Width - 25 - racket.Width / 2)
            {
                Canvas.SetLeft(racket, point.X - racket.Width / 2);
            }

        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Application.Current.Shutdown();
            }
            if (e.Key == Key.F1)
            {
                timer.Stop();
                NewGame();
            }

        }

        private void NewGame()
        {
            positionY = 10;
            positionX = 200;

            speedY = 4;
            speedX = 2;
            score = 0;

            directionY = speedY;
            directionX = speedX;

            this.Title = "PingPong         SCORE: " + score.ToString();
            double middle = ((this.Width - 25) / 2) - (racket.Width / 2);
            Canvas.SetLeft(racket, middle);

            Canvas.SetLeft(ball, positionX);
            Canvas.SetTop(ball, positionY);

            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 5);
            timer.Tick += Timer_Tick;
            timer.Start();
            this.Cursor = Cursors.Hand;
        }
    }
}
