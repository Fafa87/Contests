using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUIs.GDI_Helpers
{
    public class View
    {
        public Size GameBounds;
        public Size ViewBounds;

        public View(Size gb, Size vb)
        {
            GameBounds = gb;
            ViewBounds = vb;
        }

        public Point GameToView(Point game)
        {
            return new Point(ViewBounds.Width * game.X / GameBounds.Width, ViewBounds.Height * game.Y / GameBounds.Height);
        }

        public Point ViewToGame(Point view)
        {
            return new Point(view.X * GameBounds.Width / ViewBounds.Width, view.Y * GameBounds.Height / ViewBounds.Height);
        }

        public static Size GetDrawingBounds(Form form)
        {
            return new Size(form.Size.Width - 10, form.Size.Height - 50);
        }
    }
}
