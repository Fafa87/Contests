using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace GUIs.GDI_Helpers
{
    public class Grid
    {
        Graphics _g;
        Size _bounds;
        public int N;
        public int M;
        public Grid(Graphics g, Size s, int n, int m)
        {
            _g = g;
            _bounds = s;//new Size((int)g.ClipBounds.Width, (int)g.ClipBounds.Height);
            //_bounds = form;
            N = n;
            M = m;
        }

        public Grid(Graphics g, int n, int m) : this(g,new Size((int)g.ClipBounds.Width, (int)g.ClipBounds.Height), n, m)
        {}

        public void Draw(Pen pen)
        {
            for (int i = 0; i <= N; i++)
            {
                _g.DrawLine(pen, GetCorner(i, 0), GetCorner(i, M));
            }

            for (int i = 0; i <= M; i++)
            {
                _g.DrawLine(pen, GetCorner(0, i), GetCorner(N, i));
            }
        }

        public System.Drawing.Point? GetField(int py, int px)
        {
            return new System.Drawing.Point(px * M / _bounds.Width, py * N / _bounds.Height);
        }

        public System.Drawing.Point GetCorner(int y, int x)
        {
            if (M == 0)
                M = 1;
            if (N == 0)
                N = 1;
            return new System.Drawing.Point(_bounds.Width * x / M, _bounds.Height * y / N);
        }

        public System.Drawing.Point GetCenter(int y, int x)
        {
            System.Drawing.Point p1 = GetCorner(y, x);
            Size p2 = new Size(GetCorner(y+1, x+1));
            System.Drawing.Point res = p1 + p2;
            return new System.Drawing.Point(res.X / 2, res.Y / 2);
        }

        public System.Drawing.Rectangle GetRectangle(int y, int x)
        {
            var p1 = GetCorner(y,x);
            var p2 = GetCorner(y+1,x+1);
            
            return System.Drawing.Rectangle.FromLTRB(p1.X, p1.Y, p2.X, p2.Y);
        }

        public void DrawText(string text, int y, int x)
        {
            StringFormat format = new StringFormat()
            {
                LineAlignment = StringAlignment.Center,
                Alignment = StringAlignment.Center
            };
            _g.DrawString(text, SystemFonts.CaptionFont, Brushes.Black, GetCorner(x, y).X, GetCorner(x, y).Y);
        }

        public void FillField(Brush brush, int y, int x)
        {
            _g.FillRectangle(brush, GetRectangle(y, x));
        }
    }
}
