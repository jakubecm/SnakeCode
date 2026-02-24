namespace SnakeGame
{
    public class Game
    {
        private readonly int screenWidth;
        private readonly int screenHeight;
        private readonly Random random = new Random();

        private Snake snake;
        private Pixel berry;
        private int score = 5;

        public Game(int width, int height)
        {
            screenWidth = width;
            screenHeight = height;

            Console.WindowWidth = width;
            Console.WindowHeight = height;

            snake = new Snake(screenWidth / 2, screenHeight / 2);
            berry = new Pixel
            {
                X = random.Next(1, screenWidth - 2),
                Y = random.Next(1, screenHeight - 2),
                Color = ConsoleColor.Cyan
            };
        }

        public void Run()
        {
            while (true)
            {
                Console.Clear();
                Renderer.DrawBorders(screenWidth, screenHeight);

                bool hitWall = snake.CheckWallCollision(screenWidth, screenHeight);
                Renderer.DrawSnake(snake);
                bool hitSelf = snake.CheckSelfCollision();

                if (hitWall || hitSelf)
                    break;

                CheckBerryCollision();
                Renderer.DrawBerry(berry);

                HandleInput();
                snake.UpdateBody(score);
                snake.Move();
            }

            Renderer.DrawGameOver(screenWidth, screenHeight, score);
        }

        private void CheckBerryCollision()
        {
            if (snake.Head.X == berry.X && snake.Head.Y == berry.Y)
            {
                score++;
                berry.X = random.Next(1, screenWidth - 2);
                berry.Y = random.Next(1, screenHeight - 2);
            }
        }

        private void HandleInput()
        {
            DateTime startTime = DateTime.Now;
            bool buttonPressed = false;

            while (true)
            {
                if (DateTime.Now.Subtract(startTime).TotalMilliseconds > 500)
                    break;

                if (Console.KeyAvailable && !buttonPressed)
                {
                    ConsoleKeyInfo pressedKey = Console.ReadKey(true);

                    if (pressedKey.Key == ConsoleKey.UpArrow && snake.Direction != Direction.Down)
                    {
                        snake.Direction = Direction.Up;
                        buttonPressed = true;
                    }
                    else if (pressedKey.Key == ConsoleKey.DownArrow && snake.Direction != Direction.Up)
                    {
                        snake.Direction = Direction.Down;
                        buttonPressed = true;
                    }
                    else if (pressedKey.Key == ConsoleKey.LeftArrow && snake.Direction != Direction.Right)
                    {
                        snake.Direction = Direction.Left;
                        buttonPressed = true;
                    }
                    else if (pressedKey.Key == ConsoleKey.RightArrow && snake.Direction != Direction.Left)
                    {
                        snake.Direction = Direction.Right;
                        buttonPressed = true;
                    }
                }
            }
        }
    }
}
