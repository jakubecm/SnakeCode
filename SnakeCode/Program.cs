// Source - https://codereview.stackexchange.com/q/127515
// Posted by Wagacca, modified by community. See post 'Timeline' for change history
// Retrieved 2026-02-17, License - CC BY-SA 3.0

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Snake
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WindowHeight = 16;
            Console.WindowWidth = 32;

            int screenWidth = Console.WindowWidth;
            int screenHeight = Console.WindowHeight;

            Random numberGenerator = new Random();

            int score = 5;

            Pixel snakeHead = new Pixel();
            snakeHead.posX = screenWidth / 2;
            snakeHead.posY = screenHeight / 2;
            snakeHead.color = ConsoleColor.Red;

            string currentDirection = "RIGHT";

            List<Pixel> torso = new List<Pixel>();

            int berryPosX = numberGenerator.Next(0, screenWidth);
            int berryPosY = numberGenerator.Next(0, screenHeight);

            while (true)
            {
                Console.Clear();
                DrawBorders(screenWidth, screenHeight);

                bool hitWall = CheckWallCollision(snakeHead, screenWidth, screenHeight);
                DrawTorso(torso);
                bool hitSelf = CheckSelfCollision(torso, snakeHead);

                if (hitWall || hitSelf)
                {
                    break;
                }

                if (berryPosX == snakeHead.posX && berryPosY == snakeHead.posY)
                {
                    score++;
                    berryPosX = numberGenerator.Next(1, screenWidth - 2);
                    berryPosY = numberGenerator.Next(1, screenHeight - 2);
                }

                DrawSnakeHead(snakeHead);
                DrawBerry(berryPosX, berryPosY);

                currentDirection = HandleInput(currentDirection);
                UpdateTorso(torso, snakeHead, score);
                MoveSnakeHead(snakeHead, currentDirection);
            }

            Console.SetCursorPosition(screenWidth / 5, screenHeight / 2);
            Console.WriteLine("Game over, Score: " + score);
            Console.SetCursorPosition(screenWidth / 5, screenHeight / 2 + 1);
        }
        public static bool CheckWallCollision(Pixel snakeHead, int screenWidth, int screenHeight)
        {
            return snakeHead.posX == screenWidth - 1 || snakeHead.posX == 0
                || snakeHead.posY == screenHeight - 1 || snakeHead.posY == 0;
        }

        public static bool CheckSelfCollision(List<Pixel> torso, Pixel snakeHead)
        {
            for (int i = 0; i < torso.Count; i++)
            {
                if (torso[i].posX == snakeHead.posX && torso[i].posY == snakeHead.posY)
                {
                    return true;
                }
            }
            return false;
        }

        public static void DrawTorso(List<Pixel> torso)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            for (int i = 0; i < torso.Count; i++)
            {
                Console.SetCursorPosition(torso[i].posX, torso[i].posY);
                Console.Write("■");
            }
        }

        public static string HandleInput(string currentDirection)
        {
            DateTime startTime = DateTime.Now;
            string buttonPressed = "no";

            while (true)
            {
                DateTime currentTime = DateTime.Now;

                if (currentTime.Subtract(startTime).TotalMilliseconds > 500) { break; }

                if (Console.KeyAvailable && buttonPressed == "no")
                {
                    ConsoleKeyInfo pressedKey = Console.ReadKey(true);

                    if (pressedKey.Key.Equals(ConsoleKey.UpArrow) && currentDirection != "DOWN")
                    {
                        currentDirection = "UP";
                        buttonPressed = "yes";
                    }
                    if (pressedKey.Key.Equals(ConsoleKey.DownArrow) && currentDirection != "UP")
                    {
                        currentDirection = "DOWN";
                        buttonPressed = "yes";
                    }
                    if (pressedKey.Key.Equals(ConsoleKey.LeftArrow) && currentDirection != "RIGHT")
                    {
                        currentDirection = "LEFT";
                        buttonPressed = "yes";
                    }
                    if (pressedKey.Key.Equals(ConsoleKey.RightArrow) && currentDirection != "LEFT")
                    {
                        currentDirection = "RIGHT";
                        buttonPressed = "yes";
                    }
                }
            }

            return currentDirection;
        }

        public static void MoveSnakeHead(Pixel snakeHead, string currentDirection)
        {
            switch (currentDirection)
            {
                case "UP":
                    snakeHead.posY--;
                    break;
                case "DOWN":
                    snakeHead.posY++;
                    break;
                case "LEFT":
                    snakeHead.posX--;
                    break;
                case "RIGHT":
                    snakeHead.posX++;
                    break;
            }
        }

        public static void UpdateTorso(List<Pixel> torso, Pixel snakeHead, int score)
        {
            torso.Add(new Pixel { posX = snakeHead.posX, posY = snakeHead.posY, color = ConsoleColor.Green });

            if (torso.Count > score)
            {
                torso.RemoveAt(0);
            }
        }

        public class Pixel
        {
            public int posX { get; set; }
            public int posY { get; set; }
            public ConsoleColor color { get; set; }
        }

        public static void DrawBorders(int screenWidth, int screenHeight)
        {
            for (int i = 0; i < screenWidth; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write("■");
            }

            for (int i = 0; i < screenWidth; i++)
            {
                Console.SetCursorPosition(i, screenHeight - 1);
                Console.Write("■");
            }

            for (int i = 0; i < screenHeight; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("■");
            }

            for (int i = 0; i < screenHeight; i++)
            {
                Console.SetCursorPosition(screenWidth - 1, i);
                Console.Write("■");
            }
        }

        public static void DrawSnakeHead(Pixel snakeHead)
        {
            Console.SetCursorPosition(snakeHead.posX, snakeHead.posY);
            Console.ForegroundColor = snakeHead.color;
            Console.Write("■");
        }

        public static void DrawBerry(int berryPosX, int berryPosY)
        {
            Console.SetCursorPosition(berryPosX, berryPosY);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("■");
        }
    }
}
