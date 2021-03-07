using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        private List<Circle> WindowsFormsApp3 = new List<Circle>();
        private Circle food = new Circle();
        int maxXPos;
        int maxYPos;

        public Form1()
        {
            InitializeComponent();

            new Settings();

            gameTimer.Interval = 1000 / Settings.Speed;
            gameTimer.Tick += UpdateScreen;
            gameTimer.Start();

            int maxXPos = pbCanvas.Size.Width / Settings.Wibth;
            int maxYPos = pbCanvas.Size.Height / Settings.Height;

            StartGame();
        }       

        private void StartGame()
        {
            lblGameOverfc.Visible = false;

            new Settings();

            WindowsFormsApp3.Clear();
            Circle head = new Circle();
            Random random = new Random();
            head.X = 10;
            head.Y = 5;
            WindowsFormsApp3.Add(head);

            label1.Text = Settings.Score.ToString();
            GenerateFood();
        }

        private void GenerateFood()
        {
            Random random = new Random();

            food = new Circle();
            food.X = random.Next(0, maxXPos);
            food.Y = random.Next(0, maxYPos);
        }

        private void UpdateScreen(object sender, EventArgs e)
        {

        }

        private void pbCanvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics canvas = e.Graphics;

            if (!Settings.GameOver)
            {
                Brush snakeColor;

                for(int i = 0; i < WindowsFormsApp3.Count;i++)
                {
                    if (i == 0)
                        snakeColor = Brushes.Black;
                    else
                        snakeColor = Brushes.Coral;

                    canvas.FillEllipse(snakeColor,
                        new Rectangle(WindowsFormsApp3[i].X * Settings.Wibth,
                                         WindowsFormsApp3[i].Y * Settings.Height,
                                         Settings.Wibth,
                                         Settings.Height));    
                }
                canvas.FillEllipse(Brushes.Red,
                        new Rectangle(food.X * Settings.Wibth,
                                         food.Y * Settings.Height,
                                         Settings.Wibth,
                                         Settings.Height));
            }
        }

        private void MovePlayer()
        {

        }

        private void Die()
        {
            Settings.GameOver = true;
        }

        private void Eat()
        {
            Circle eatenFood = new Circle();
            eatenFood.X = WindowsFormsApp3[WindowsFormsApp3.Count - 1].X;
            eatenFood.Y = WindowsFormsApp3[WindowsFormsApp3.Count - 1].Y;

            WindowsFormsApp3.Add(eatenFood);
            Settings.Score += Settings.Points;
            label1.Text = Settings.Score.ToString();

            GenerateFood();

        }

       
    }
}
