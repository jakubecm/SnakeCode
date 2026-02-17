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
            int gameOver = 0;

            Pixel snakeHead = new Pixel();
            snakeHead.posX = screenWidth / 2;
            snakeHead.posY = screenHeight / 2;
            snakeHead.color = ConsoleColor.Red;

            string currentDirection = "RIGHT";

            List<int> TorsoPosX = new List<int>();
            List<int> TorsoPosY = new List<int>();

            int berryPosX = numberGenerator.Next(0, screenWidth);
            int berryPosY = numberGenerator.Next(0, screenHeight);

            DateTime startTime = DateTime.Now;
            DateTime currentTime = DateTime.Now;

            string buttonPressed = "no";

            while (true)
            {
                Console.Clear();

                if (snakeHead.posX == screenWidth - 1 || snakeHead.posX == 0 || snakeHead.posY == screenHeight - 1 || snakeHead.posY == 0)
                {
                    gameOver = 1;
                }

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

                Console.ForegroundColor = ConsoleColor.Green;

                if (berryPosX == snakeHead.posX && berryPosY == snakeHead.posY)
                {
                    score++;
                    berryPosX = numberGenerator.Next(1, screenWidth - 2);
                    berryPosY = numberGenerator.Next(1, screenHeight - 2);
                }

                for (int i = 0; i < TorsoPosX.Count(); i++)
                {
                    Console.SetCursorPosition(TorsoPosX[i], TorsoPosY[i]);
                    Console.Write("■");
                    if (TorsoPosX[i] == snakeHead.posX && TorsoPosY[i] == snakeHead.posY)
                    {
                        gameOver = 1;
                    }
                }

                if (gameOver == 1)
                {
                    break;
                }

                Console.SetCursorPosition(snakeHead.posX, snakeHead.posY);
                Console.ForegroundColor = snakeHead.color;
                Console.Write("■");

                Console.SetCursorPosition(berryPosX, berryPosY);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("■");

                startTime = DateTime.Now;
                buttonPressed = "no";

                while (true)
                {
                    currentTime = DateTime.Now;
                    if (currentTime.Subtract(startTime).TotalMilliseconds > 500) { break; }
                    if (Console.KeyAvailable)
                    {
                        ConsoleKeyInfo pressedKey = Console.ReadKey(true);

                        if (pressedKey.Key.Equals(ConsoleKey.UpArrow) && currentDirection != "DOWN" && buttonPressed == "no")
                        {
                            currentDirection = "UP";
                            buttonPressed = "yes";
                        }
                        if (pressedKey.Key.Equals(ConsoleKey.DownArrow) && currentDirection != "UP" && buttonPressed == "no")
                        {
                            currentDirection = "DOWN";
                            buttonPressed = "yes";
                        }
                        if (pressedKey.Key.Equals(ConsoleKey.LeftArrow) && currentDirection != "RIGHT" && buttonPressed == "no")
                        {
                            currentDirection = "LEFT";
                            buttonPressed = "yes";
                        }
                        if (pressedKey.Key.Equals(ConsoleKey.RightArrow) && currentDirection != "LEFT" && buttonPressed == "no")
                        {
                            currentDirection = "RIGHT";
                            buttonPressed = "yes";
                        }
                    }
                }

                TorsoPosX.Add(snakeHead.posX);
                TorsoPosY.Add(snakeHead.posY);

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

                if (TorsoPosX.Count() > score)
                {
                    TorsoPosX.RemoveAt(0);
                    TorsoPosY.RemoveAt(0);
                }
            }

            Console.SetCursorPosition(screenWidth / 5, screenHeight / 2);
            Console.WriteLine("Game over, Score: " + score);
            Console.SetCursorPosition(screenWidth / 5, screenHeight / 2 + 1);
        }
        class Pixel
        {
            public int posX { get; set; }
            public int posY { get; set; }
            public ConsoleColor color { get; set; }
        }
    }
}
