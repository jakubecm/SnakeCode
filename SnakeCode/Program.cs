// Source - https://codereview.stackexchange.com/q/127515
// Posted by Wagacca, modified by community. See post 'Timeline' for change history
// Retrieved 2026-02-17, License - CC BY-SA 3.0

namespace SnakeGame
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new Game(32, 16);
            game.Run();
        }
    }
}
