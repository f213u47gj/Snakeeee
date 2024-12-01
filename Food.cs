using System;
using System.Collections.Generic;

namespace SnakeGame
{
    public class Food
    {
        private readonly int width;
        private readonly int height;
        private readonly Random random;

        public Food(int width, int height)
        {
            this.width = width;
            this.height = height;
            random = new Random();
        }

        public Segment Position { get; private set; }

        public void Spawn(Snake snake)
        {
            Segment newPosition;
            do
            {
                newPosition = new Segment(random.Next(1, width - 1), random.Next(1, height - 1));
            } while (IsPositionOnSnake(newPosition, snake));

            Position = newPosition;
        }

        private bool IsPositionOnSnake(Segment position, Snake snake)
        {
            for (int i = 0; i < snake.Size; i++)    
            {
                if (position.X == snake[i].X && position.Y == snake[i].Y)
                {
                    return true;
                }
            }
            return false;
        }

    }
}
