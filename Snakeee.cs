using System.Collections.Generic;

namespace SnakeGame
{
    public class Snake
    {
        private readonly List<Segment> segments;

        public Snake(int x, int y, uint size)
        {
            if (size == 0)
                throw new ArgumentException("Размер змейки не может быть равен 0", nameof(size));

            segments = new List<Segment>();
            for (int i = 0; i < size; i++)
            {
                segments.Add(new Segment(x, y + i));
            }

            Direction = Direction.Up;
        }

        public Direction Direction { get; set; }

        public int Size => segments.Count;

        public Segment this[int index] => segments[index];

        public void Move(int speed)
        {
            int x = 0, y = 0;

            switch (Direction)
            {
                case Direction.Up:
                    y = -speed;
                    break;
                case Direction.Down:
                    y = speed;
                    break;
                case Direction.Left:
                    x = -speed;
                    break;
                case Direction.Right:
                    x = speed;
                    break;
            }

            for (int i = segments.Count - 1; i > 0; i--)
            {
                segments[i].X = segments[i - 1].X;
                segments[i].Y = segments[i - 1].Y;
            }

            segments[0].X += x;
            segments[0].Y += y;
        }

        public void Grow()
        {
            segments.Add(new Segment(segments[^1].X, segments[^1].Y));
        }
    }
}
