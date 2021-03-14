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

            maxXPos = pbCanvas.Size.Width / Settings.Wibth;
            maxYPos = pbCanvas.Size.Height / Settings.Height;

            StartGame();
        }

        private void StartGame()
        {
            lblGameOverfc.Visible = false;

            new Settings();

            WindowsFormsApp3.Clear();
            Circle head = new Circle();
            Random random = new Random();
            head.X = random.Next(0, maxXPos);
            head.Y = random.Next(0, maxYPos);
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
            if (Settings.GameOver)
            {
                if (Input.KeyPressed(Keys.Enter))
                    StartGame();
            }
            else
            {
                if (Input.KeyPressed(Keys.Left) && Settings.direction != Direction.Right)
                {
                    Settings.direction = Direction.Left;
                }
                else if (Input.KeyPressed(Keys.Right) && Settings.direction != Direction.Left)
                {
                    Settings.direction = Direction.Right;
                }
                if (Input.KeyPressed(Keys.Up) && Settings.direction != Direction.Down)
                {
                    Settings.direction = Direction.Up;
                }
                else if (Input.KeyPressed(Keys.Down) && Settings.direction != Direction.Up)
                {
                    Settings.direction = Direction.Down;
                }

                MovePlayer();
            }

            pbCanvas.Invalidate();
        }

        private void pbCanvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics canvas = e.Graphics;

            if (!Settings.GameOver)
            {
                Brush snakeColor;

                for (int i = 0; i < WindowsFormsApp3.Count; i++)
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
            for (int i = WindowsFormsApp3.Count - 1; i >= 0; i--)
            {
                if (i == 0)
                {
                    switch (Settings.direction)
                    {
                        case Direction.Up:
                            WindowsFormsApp3[i].Y--;
                            break;
                        case Direction.Down:
                            WindowsFormsApp3[i].Y++;
                            break;
                        case Direction.Left:
                            WindowsFormsApp3[i].X--;
                            break;
                        case Direction.Right:
                            WindowsFormsApp3[i].X++;
                            break;

                    }
                    if (WindowsFormsApp3[i].X > maxXPos || WindowsFormsApp3[i].X < 0 || WindowsFormsApp3[i].Y > maxYPos || WindowsFormsApp3[i].Y < 0)
                        Die();

                    for (int j = 1; j < WindowsFormsApp3.Count; j++)
                    {
                        if (WindowsFormsApp3[0].X == WindowsFormsApp3[j].X && WindowsFormsApp3[0].Y == WindowsFormsApp3[j].Y)
                        {
                            Die();
                            break;
                        }
                    }
                
                if (WindowsFormsApp3[0].X == food.X && WindowsFormsApp3[0].Y == food.Y)
                    Eat();

            }
                else
            {
                WindowsFormsApp3[i].X = WindowsFormsApp3[i - 1].X;
                WindowsFormsApp3[i].Y = WindowsFormsApp3[i - 1].Y;
            }
        }
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
        label1.Text = "Score: " + Settings.Score.ToString();

            Settings.Speed++;
            gameTimer.Interval = 1000 / Settings.Speed;

        GenerateFood();

    }

    private void pbCanvas_Click(object sender, EventArgs e)
    {

    }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Input.ChangeState(e.KeyCode, true);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            Input.ChangeState(e.KeyCode, false);
        }
    }
}
