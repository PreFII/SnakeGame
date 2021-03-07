using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp3
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    class Settings
    {
        public static int Wibth;
        public static int Height;
        public static int Speed;
        public static int Score;
        public static int Points;
        public static bool GameOver;
        public static Direction direction;


        public Settings()
        {
            Wibth = 16;
            Height = 16;
            Speed = 8;
            Score = 0;
            Points = 100;
            GameOver = false;
            direction = Direction.Down;
        }
    }
}
