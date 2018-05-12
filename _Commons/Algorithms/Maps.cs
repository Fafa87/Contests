using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms
{
    [Serializable]
    public class Map<T> : List<List<T>>
    {
        public Map(GridPoint shape) : this(shape.Y, shape.X)
        {

        }

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

        public void Fill(T value)
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int i2 = 0; i2 < Cols; i2++)
                {
                    this[i][i2] = value;
                }
            }
        }

        public static Map<T> ParseMap<T>(IEnumerable<string> data, Func<char, T> parseField)
        {
            Map<T> map = new Map<T>(data.Count(), data.First().Length);
            map.Fill(data.ToList(), parseField);
            return map;
        }

        public GridPoint Shape
        {
            get { return new GridPoint(Cols, Rows); }
        }

        public int Rows
        {
            get { return this.Count; }
        }

        public int Cols
        {
            get { return this[0].Count; }
        }

        public List<GridPoint> Coordinates(Func<T, bool> filter = null)
        {
            filter = filter ?? new Func<T, bool>((c) => true);

            List<GridPoint> res = new List<GridPoint>();
            for (int i = 0; i < Rows; i++)
            {
                for (int i2 = 0; i2 < Cols; i2++)
                {
                    if (filter(this[i][i2]))
                        res.Add(new GridPoint(i2, i));
                }
            }
            return res;
        }

        public bool IsInside(GridPoint point)
        {
            return point.X >= 0 && point.Y >= 0 && point.X < Cols && point.Y < Rows;
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

        protected virtual string FormatField(T c)
        {
            if (c is bool)
            {
                return (bool)((object)c) ? "1" : "0";
            }
            else
                return c.ToString();
        }

        public override string ToString()
        {
            return String.Join("\n", this.Select(p=>String.Join(" ", 
                p.Select(FormatField)
            )));
        }
    }

    public enum Moves4
    {
        Up, Left, Down, Right
    }

    public enum Moves8
    {
        UpLeft, Up, UpRight,
        Left,       Right,
        DownLeft, Down, DownRight
    }

    public static class Moves
    {
        public static Moves4[] All4 = new[] { Moves4.Up, Moves4.Left, Moves4.Down, Moves4.Right };
        public static Moves8[] All8 = new[] { 
            Moves8.UpLeft,   Moves8.Up,   Moves8.UpRight,
            Moves8.Left,                  Moves8.Right,
            Moves8.DownLeft, Moves8.Down, Moves8.DownRight
        };

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

        public static GridPoint Move(this Moves8 d, GridPoint point)
        {
            switch (d)
            {
                case Moves8.Up:
                    return new GridPoint(point.X, point.Y - 1);
                case Moves8.Down:
                    return new GridPoint(point.X, point.Y + 1);
                case Moves8.Left:
                    return new GridPoint(point.X - 1, point.Y);
                case Moves8.Right:
                    return new GridPoint(point.X + 1, point.Y);
                case Moves8.UpLeft:
                    return new GridPoint(point.X - 1, point.Y - 1);
                case Moves8.UpRight:
                    return new GridPoint(point.X + 1, point.Y - 1);
                case Moves8.DownLeft:
                    return new GridPoint(point.X - 1, point.Y + 1);
                case Moves8.DownRight:
                    return new GridPoint(point.X + 1, point.Y + 1);
                default:
                    throw new ArgumentException("Unsupported move: " + d);
            }
        }
    }
}
