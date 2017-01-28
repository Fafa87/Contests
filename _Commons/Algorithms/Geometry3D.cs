using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class Point3D : IEquatable<Point3D>
{
    public Point Point;
    public double X { get { return Point.X; } set { Point.X = value; } }
    public double Y { get { return Point.Y; } set { Point.Y = value; } }
    public double Z;

    public Point3D(double px, double py, double pz)
    {
        Point = new Point(px, py);
        Z = pz;
    }

    public Point3D(Tuple<double, double, double> p)
        : this(p.Item1, p.Item2, p.Item3)
    {
    }

    public double Length()
    {
        return Math.Sqrt((double)(X * X + Y * Y + Z * Z));
    }

    public bool Equals(Point3D other)
    {
        return X == other.X && Y == other.Y && Z == other.Z;
    }
}

// TODO: maybe cuboid rotation is worth adding
public class Cuboid
{
    public Point3D Center;
    public double A;
    public double B;
    public double C;

    public Cuboid(Point3D center, double a, double b, double c)
    {
        Center = center;
        A = a;
        B = b;
        C = c;
    }

    public Cuboid(Point3D vertexA, Point3D vertexB)
    {
        Center = new Point3D(
            (vertexB.X + vertexA.X) / 2.0,
            (vertexB.Y + vertexA.Y) / 2.0,
            (vertexB.Z + vertexA.Z) / 2.0);
        A = vertexB.X - VertexA.X;
        B = vertexB.Y - VertexA.Y;
        C = vertexB.Z - VertexA.Z;
    }

    public Point3D Vertex1 { get { return new Point3D(Center.X - A / 2.0, Center.Y - B / 2.0, Center.Z - C / 2.0); } }
    public Point3D Vertex2 { get { return new Point3D(Center.X - A / 2.0, Center.Y - B / 2.0, Center.Z + C / 2.0); } }
    public Point3D Vertex3 { get { return new Point3D(Center.X - A / 2.0, Center.Y + B / 2.0, Center.Z - C / 2.0); } }
    public Point3D Vertex4 { get { return new Point3D(Center.X - A / 2.0, Center.Y + B / 2.0, Center.Z + C / 2.0); } }
    public Point3D Vertex5 { get { return new Point3D(Center.X + A / 2.0, Center.Y - B / 2.0, Center.Z - C / 2.0); } }
    public Point3D Vertex6 { get { return new Point3D(Center.X + A / 2.0, Center.Y - B / 2.0, Center.Z + C / 2.0); } }
    public Point3D Vertex7 { get { return new Point3D(Center.X + A / 2.0, Center.Y + B / 2.0, Center.Z - C / 2.0); } }
    public Point3D Vertex8 { get { return new Point3D(Center.X + A / 2.0, Center.Y + B / 2.0, Center.Z + C / 2.0); } }

    public Point3D VertexA { get { return Vertex1; } }
    public Point3D VertexB { get { return Vertex8; } }

    public Point3D[] Vertices
    {
        get { return new[] { Vertex1, Vertex2, Vertex3, Vertex4, Vertex5, Vertex6, Vertex7, Vertex8 }; }
    }
}
