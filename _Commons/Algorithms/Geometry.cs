using System;
using System.Linq;
using System.Collections.Generic;
//using System.Xml;


public class FunctionalComparer<T> : IComparer<T>
{
    private Func<T, T, int> comparer;
    public FunctionalComparer(Func<T, T, int> comparer)
    {
        this.comparer = comparer;
    }
    public static IComparer<T> Create(Func<T, T, int> comparer)
    {
        return new FunctionalComparer<T>(comparer);
    }
    public int Compare(T x, T y)
    {
        return comparer(x, y);
    }
}

public static class GeometryUtils
{
    public static double Area(Point a, Point b, Point c, bool allowNegative = false)
    {
        double res = 0;
        res = (a.X + b.X) * (a.Y - b.Y) + (b.X + c.X) * (b.Y - c.Y) + (c.X + a.X) * (c.Y - a.Y);
        if (allowNegative)
            return res / 2;

        return Math.Abs(res) / 2;
    }

}

public struct GridPoint
{
    public int X, Y;
    public GridPoint(int px, int py)
    {
        X = px;
        Y = py;
    }

    public GridPoint(Tuple<int,int> p)
    {
        X = p.Item1;
        Y = p.Item2;
    }

    public static GridPoint operator +(GridPoint a, GridPoint b) 
    {
        return new GridPoint(a.X + b.X, a.Y + b.Y);
    }

    public static GridPoint operator -(GridPoint a, GridPoint b) 
    {
        return new GridPoint(a.X - b.X, a.Y - b.Y);
    }

    public override String ToString()
    {
        return String.Format("x={0} y={1}", X, Y);
    }

    public int ManhattanDistance(GridPoint b)
    {
        GridPoint newPoint = new GridPoint(X - b.X, Y - b.Y);
        return newPoint.ManhattanDistance();
    }

    public int ManhattanDistance(int y, int x)
    {
        return Math.Abs(X -x ) + Math.Abs(Y - y);
    }

    public int ManhattanDistance()
    {
        return Math.Abs(X) + Math.Abs(Y);
    }

    public int EuclidianDistance(GridPoint b)
    {
        int deltaX = X - b.X;
        int deltaY = Y - b.Y;
        return (int)Math.Ceiling(Math.Sqrt(deltaX * deltaX + deltaY * deltaY));
    }

}


public class Point : IEquatable<Point>
{
    public double X, Y;
    public readonly int Quarter;
    public Point(double px, double py)
    {
        X = (double)px;
        Y = (double)py;
        Quarter = CalculateQuarter(); 
    }

    public Point(Tuple<double, double> p)
    {
        X = p.Item1;
        Y = p.Item2;
    }

    //public Point(string data)
    //{
    //    var tokens = data.Split(' ');
    //    x = XmlConvert.ToDouble(tokens[0]);
    //    y = XmlConvert.ToDouble(tokens[1]);
    //}

    public static Point operator+(Point a, Point b) 
    {
        return new Point(a.X+b.X,a.Y+b.Y);
    }

    public static Point operator -(Point a, Point b) 
    {
        return new Point(a.X-b.X,a.Y-b.Y);
    }

    public static Point operator*(Point a, double mult)
    {
        return new Point(a.X * mult, a.Y * mult);
    }

    public static Point operator /(Point a, double mult)
    {
        return new Point(a.X / mult, a.Y / mult);
    }

    public double ScalarMult(Point b)
    {
        return X * b.X + Y * b.Y;
    }

    public double VectorMult(Point b)
    {
        return X * b.Y - Y * b.X;
    }

    public double Cosinus(Point b)
    {
        return ScalarMult(b) / this.Length() / b.Length();
    }

    public override String ToString()
    {
        return String.Format("x={0} y={1}", X, Y);
    }

    public double Distance(Point b)
    {
        Point newPoint = new Point(X - b.X, Y - b.Y);
        return newPoint.Length();
    }

    public void Normalize()
    {
        var len = Length();
        X /= len;
        Y /= len;
    }

    public Point Normalized()
    {
        Point newPoint = new Point(X, Y);
        newPoint.Normalize();
        return newPoint;
    }

    public double Length()
    {
        return (double)Math.Pow((double)(X * X + Y * Y), 0.5);
    }

    public bool IsInBoundingBox(Point bbMin, Point bbMax)
    {
        return bbMin.X <= this.X && this.X <= bbMax.X && bbMin.Y <= this.Y && this.Y <= bbMax.Y;
    }

    private int CalculateQuarter()
    {
        if(X>0 && Y >=0)
            return 1;
        if(X<=0 && Y >0)
            return 2;
        if(X<0 && Y<=0)
            return 3;
        if(X>=0 && Y<0)
            return 4;
        return 0;
        //throw new InvalidOperationException("Where are you point?");
    }

    private class SweepPoint<T>
    {
        public readonly Point Point; 
        public readonly bool Start;
        public readonly T Data;
        public SweepPoint(Point a, bool b, T data)
        {
            this.Point = a;
            this.Start = b;
            this.Data = data;
        }
        public static SweepPoint<T> Create(Point a, bool b, T data)
        {
            return new SweepPoint<T>(a, b, data);
        }
    }

    public IEnumerable<LineSegment> GetVisibleBrute(IEnumerable<LineSegment> segments)
    {
        List<LineSegment> visible = new List<LineSegment>();

        foreach (var lineSeg in segments)
        {
            var poteLine1 = new LineSegment(this, lineSeg.a);
            var poteLine2 = new LineSegment(this, lineSeg.b);

            if (!(segments.Any(pol => pol != lineSeg && pol.Intersects(poteLine1)) || segments.Any(pol => pol != lineSeg && pol.Intersects(poteLine2))))
            {
                visible.Add(lineSeg);
            }
        }
        return visible;
    }

    public IEnumerable<LineSegment> GetBestVisibleBrute(IEnumerable<LineSegment> segments, Func<LineSegment,double> bester)
    {
        List<LineSegment> visible = new List<LineSegment>();
        double bestVal = double.MaxValue;

        foreach (var lineSeg in segments)
        {
            var poteLine1 = new LineSegment(this, lineSeg.a);
            var poteLine2 = new LineSegment(this, lineSeg.b);

            var newVal = bester(lineSeg);
            if(bestVal > newVal)
            { 

            if (!(segments.Any(line => line != lineSeg && line.Intersects(poteLine1)) || segments.Any(line => line != lineSeg && line.Intersects(poteLine2))))
            {
                visible.Add(lineSeg);
                bestVal = Math.Min(bestVal, newVal);
            }

        }
        }
        return visible;
    }

    public bool Equals(Point other)
    {
        return X == other.X && Y == other.Y;
    }

    public class EqualityComparer : IEqualityComparer<Point>
    {

        public bool Equals(Point x, Point y)
        {
            return x.X == y.X && x.Y == y.Y;
        }

        public int GetHashCode(Point x)
        {
            return (int)x.X ^ (int)x.Y;
        }

    }
}

public class Rectangle   
{
    public double Top,Bottom,Left,Right;
    public Rectangle(Point a, Point b)
    {
        Top = Math.Max(a.Y, b.Y);
        Bottom = Math.Min(a.Y, b.Y);
        Left = Math.Min(a.X, b.X);
        Right = Math.Max(a.X, b.X);
    }

    public void Enlarge(int dx, int dy)
    {
        Top += dy / 2;
        Bottom -= dy - dy / 2;
        Left -= dx / 2;
        Right += dx - dx / 2;
    }

    public Rectangle Clone()
    {
        return (Rectangle)this.MemberwiseClone();
    }

    public void MergeWith(Rectangle other)
    {
        Top = Math.Max(Top, other.Top);
        Bottom = Math.Min(Bottom, other.Bottom);
        Left = Math.Min(Left, other.Left);
        Right = Math.Max(Right, other.Right);
    }

    public bool Intersects(Rectangle other)
    {
        return !(other.Left > this.Right ||
           other.Right < this.Left ||
           other.Top < this.Bottom ||
           other.Bottom > this.Top);
    }

    public bool Inside(Point size)
    {
        return Left >= 0 && Right < size.X && Bottom >= 0 && Top < size.X;
    }

    public override string ToString()
    {
        return Left + " " + Top + " " + Right + " " + Bottom; 
    }
}

public class LineSegment
{
    public readonly Point a, b;
    public readonly Rectangle Box;

    public LineSegment(double x1, double y1, double x2, double y2) : this(new Point(x1,y1), new Point(x2,y2))
    {}

    public LineSegment(Point a, Point b)
    {
        this.a = a;
        this.b = b;
        this.Box = new Rectangle(a, b);
    }

    public Point GetVector()
    {
        return b - a;
    }

    public bool BoundingBoxIntersects(LineSegment other)
    {
        return other.Box.Intersects(Box);
    }

    public bool IsCutting(LineSegment other)
    {
        var vector = GetVector();
        var vectorToA = other.a - a;
        var vectorToB = other.b - a;
        if (other.a.Equals(a) || other.b.Equals(a) || other.a.Equals(b) || other.b.Equals(b))
            return false;
        var signA = Math.Sign(vector.VectorMult(vectorToA));
        var signB = Math.Sign(vector.VectorMult(vectorToB));
        return signA == -signB || (signA == 0 || signB == 0);
    }

    public bool Intersects(LineSegment other, bool checkBB = true)
    {
        if (checkBB && BoundingBoxIntersects(other) == false)
            return false;

        return IsCutting(other) && other.IsCutting(this);
    }

    public override String ToString()
    {
        return String.Format("Segment: ({0}->{1})", a, b);
    }

    //public double ExtrapolateY(double x)
    //{
    //    if(Box.Left == Box.Right)
    //    {
    //        return 
    //    }

    //}
}

public class Polygon
{
    public List<Point> points = new List<Point>();
    private Rectangle boundingBox;
    public Rectangle BoundingBox
    {
        get
        {
            return boundingBox ?? (boundingBox = new Rectangle(
                new Point(points.Max(p => p.X), points.Max(p => p.Y)), new Point(points.Min(p => p.X), points.Min(p => p.Y))));
        }
    }

    public Polygon()
    {
    }

    public Polygon(IEnumerable<Point> points)
    {
        this.points = points.ToList();
        if (Area(true) < 0)
            this.points.Reverse();
    }

    public int n
    {
        get { return points.Count; }
    }

    public IList<LineSegment> GetLineSegments()
    {
        List<LineSegment> lines = new List<LineSegment>();
        for (int i = 0; i < n; i++)
        {
            var next = (i + 1) % n;
            lines.Add(new LineSegment(points[i], points[next]));
        }
        return lines;
    }

    public IEnumerable<LineSegment> GetIntersecting(LineSegment line)
    {
        // do not return edges with common points
        return GetLineSegments().Where(ls => ls.Intersects(line));
    }

    public double Area(bool allowNegative = false)
    {
        double res = 0;
        for (int i = 0; i < points.Count; i++)
        {
            int next = (i + 1) % points.Count;
            res += ((points[i].X + points[next].X) * (points[i].Y - points[next].Y));
        }
        if (allowNegative)
            return res / 2;
        return Math.Abs(res) / 2;
    }

    public override string ToString()
    {
        return String.Format("Polygon: ({0})", String.Join(";", points.Select(p => p.ToString()).ToArray()));
    }
}