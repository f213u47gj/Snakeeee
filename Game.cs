using System;
using System.Threading;

namespace SnakeGame
{
    public class Game
    {
        private readonly int width;
        private readonly int height;
        private Snake snake;
        private Food food;
        private Bonus bonus;
        private int bonusTimer;

        public Game(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public void Run()
        {
            Console.CursorVisible = false;
            snake = new Snake(x: width / 2, y: height / 2, size: 5);
            food = new Food(width, height);
            bonus = new Bonus(width, height);

            food.Spawn(snake);
            bonusTimer = 0;

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true).Key;
                    ChangeDirection(key);
                }

                snake.Move(1);

                if (snake[0].X == food.Position.X && snake[0].Y == food.Position.Y)
                {
                    snake.Grow();
                    food.Spawn(snake);
                }

                if (bonus.IsActive && snake[0].X == bonus.Position.X && snake[0].Y == bonus.Position.Y)
                {
                    bonus.Deactivate();
                    snake.Grow();
                    Console.Beep();
                }

                if (++bonusTimer == 50)
                {
                    bonus.Spawn(snake);
                    bonusTimer = 0;
                }

                if (CheckCollision())
                {
                    Console.Clear();
                    Console.WriteLine("Игра окончена!");
                    Console.WriteLine($"Ваш счёт: {snake.Size - 5}");
                    break;
                }

                Draw();
                Thread.Sleep(150);
            }
        }

        private void ChangeDirection(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.UpArrow when snake.Direction != Direction.Down:
                    snake.Direction = Direction.Up;
                    break;
                case ConsoleKey.DownArrow when snake.Direction != Direction.Up:
                    snake.Direction = Direction.Down;
                    break;
                case ConsoleKey.LeftArrow when snake.Direction != Direction.Right:
                    snake.Direction = Direction.Left;
                    break;
                case ConsoleKey.RightArrow when snake.Direction != Direction.Left:
                    snake.Direction = Direction.Right;
                    break;
                case ConsoleKey.Escape:
                    Environment.Exit(0);
                    break;
            }
        }

        private bool CheckCollision()
        {
            var head = snake[0];
            if (head.X < 1 || head.Y < 1 || head.X >= width - 1 || head.Y >= height - 1)
                return true;

            for (int i = 1; i < snake.Size; i++)
            {
                if (snake[i].X == head.X && snake[i].Y == head.Y)
                    return true;
            }

            return false;
        }

        private void Draw()
        {
            Console.Clear();

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (y == 0 || y == height - 1 || x == 0 || x == width - 1)
                    {
                        Console.SetCursorPosition(x, y);
                        Console.Write("#");
                    }
                }
            }

            for (int i = 0; i < snake.Size; i++)
            {
                Console.SetCursorPosition(snake[i].X, snake[i].Y);
                Console.Write(i == 0 ? "*" : "❛");
            }

            Console.SetCursorPosition(food.Position.X, food.Position.Y);
            Console.Write("@");

            if (bonus.IsActive)
            {
                Console.SetCursorPosition(bonus.Position.X, bonus.Position.Y);
                Console.Write("$");
            }

            Console.SetCursorPosition(0, height);
            Console.Write($"Очки: {snake.Size - 5}");
        }
    }
}
