namespace SnakeGame
{
    public class Snake
    {
        public Pixel Head { get; }
        public List<Pixel> Body { get; } = new List<Pixel>();
        public Direction Direction { get; set; }

        public Snake(int startX, int startY)
        {
            Head = new Pixel { X = startX, Y = startY, Color = ConsoleColor.Red };
            Direction = Direction.Right;
        }

        public void Move()
        {
            switch (Direction)
            {
                case Direction.Up:    Head.Y--; break;
                case Direction.Down:  Head.Y++; break;
                case Direction.Left:  Head.X--; break;
                case Direction.Right: Head.X++; break;
            }
        }

        public void UpdateBody(int maxLength)
        {
            Body.Add(new Pixel { X = Head.X, Y = Head.Y, Color = ConsoleColor.Green });

            if (Body.Count > maxLength)
            {
                Body.RemoveAt(0);
            }
        }

        public bool CheckWallCollision(int screenWidth, int screenHeight)
        {
            return Head.X == 0 || Head.X == screenWidth - 1
                || Head.Y == 0 || Head.Y == screenHeight - 1;
        }

        public bool CheckSelfCollision()
        {
            for (int i = 0; i < Body.Count; i++)
            {
                if (Body[i].X == Head.X && Body[i].Y == Head.Y)
                    return true;
            }
            return false;
        }
    }
}
