using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public class Map<T> : List<List<T>>
    {
        public Map(int rows, int cols)
        {
            var oneRow = Enumerable.Repeat(default(T), cols);
            for (int i = 0; i < rows; i++)
                this.Add(oneRow.ToList());
        }

        public void Fill(List<string> data, Func<char, T> parseField)
        {
            for (int i = 0; i < Rows; i++)
            {
                var curRow = data[i];
                for (int i2 = 0; i2 < Cols; i2++)
                {
                    this[i][i2] = parseField(curRow[i2]);
                }
            }
        }

        public static Map<T> ParseMap<T>(List<string> data, Func<char, T> parseField)
        {
            Map<T> map = new Map<T>(data.Count, data[0].Length);
            map.Fill(data, parseField);
            return map;
        }

        public int Rows
        {
            get { return this.Count; }
        }

        public int Cols
        {
            get { return this[0].Count; }
        }

        public T this[GridPoint point]
        {
            get
            {
                return this[point.Y][point.X];
            }
            set
            {
                this[point.Y][point.X] = value;
            }
        }
    }

    public enum Moves4
    {
        Up, Left, Down, Right
    }

    public static class Moves
    {
        public static IEnumerable<Moves4> All4()
        {
            return new[] { Moves4.Up, Moves4.Left, Moves4.Down, Moves4.Right };
        }

        public static GridPoint Move(this Moves4 d, GridPoint point)
        {
            switch (d)
            {
                case Moves4.Up:
                    return new GridPoint(point.X, point.Y - 1);
                case Moves4.Down:
                    return new GridPoint(point.X, point.Y + 1);
                case Moves4.Left:
                    return new GridPoint(point.X - 1, point.Y);
                case Moves4.Right:
                    return new GridPoint(point.X + 1, point.Y);
                default:
                    throw new ArgumentException("Unsupported move: " + d);
            }
        }
    }
}
