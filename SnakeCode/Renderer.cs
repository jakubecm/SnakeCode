namespace SnakeGame
{
    public static class Renderer
    {
        public static void DrawBorders(int screenWidth, int screenHeight)
        {
            Console.ForegroundColor = ConsoleColor.White;

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

        public static void DrawSnake(Snake snake)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            foreach (var segment in snake.Body)
            {
                Console.SetCursorPosition(segment.X, segment.Y);
                Console.Write("■");
            }

            Console.SetCursorPosition(snake.Head.X, snake.Head.Y);
            Console.ForegroundColor = snake.Head.Color;
            Console.Write("■");
        }

        public static void DrawBerry(Pixel berry)
        {
            Console.SetCursorPosition(berry.X, berry.Y);
            Console.ForegroundColor = berry.Color;
            Console.Write("■");
        }

        public static void DrawGameOver(int screenWidth, int screenHeight, int score)
        {
            Console.SetCursorPosition(screenWidth / 5, screenHeight / 2);
            Console.WriteLine("Game over, Score: " + score);
            Console.SetCursorPosition(screenWidth / 5, screenHeight / 2 + 1);
        }
    }
}
