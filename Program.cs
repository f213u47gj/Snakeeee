using System;

namespace SnakeGame
{
    internal class Program
    {
        private static void Main()
        {
            Game game = new Game(30, 20);
            game.Run();
        }
    }
}
