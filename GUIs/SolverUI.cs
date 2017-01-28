using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using GUIs.GDI_Helpers;
using Deadline;

namespace GUIs
{
    public class SolverUI : Solution
    {
        public System.Drawing.Point Selected;
        Grid grid;
        public SolverUI(IClient c)
            : base(c)
        {
        }

        //public SolverUI(IServer c)
        //    : base(c)
        //{
        //}

        public void DrawState(Graphics g, Size s)
        {
            try
            {
                grid = new Grid(g, s, 10, 10);
                grid.Draw(Pens.Aqua);
                grid.FillField(Brushes.Black, 2, 2);
                grid.FillField(Brushes.Black, Selected.Y, Selected.X);
            }
            catch (Exception e) { }
        }

        public void Click(System.Windows.Forms.MouseButtons mouseButtons, System.Drawing.Point point)
        {
            Selected = grid.GetField(point.Y, point.X).Value;
            //Selected = point;
        }
    }
}
