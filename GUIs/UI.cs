using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GUIs
{
    public partial class UI : Form
    {
        public UI()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void UI_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                Program.solverUI.DrawState(e.Graphics, new Size(Size.Width - 10, Size.Height - 50));
            }
            catch (Exception ex) { }
        }

        private void UI_MouseDown(object sender, MouseEventArgs e)
        {
			// Not working yet.
			
            try
            {
                Program.solverUI.Click(e.Button, new System.Drawing.Point(e.X, e.Y));
                //if(Program.solverUI.Selected == null) 
                //    Program.solverUI.Select( new Point(e.X,e.Y));
                //else if (Program.solverUI.order == null)
                //{
                //    Program.solverUI.Order(new Point(e.X, e.Y));
                //}
                //else
                //{
                //    //Program.solverUI.selected = Program.solverUI.order = null;
                //}
            }
            catch (Exception ex) { }
			
        }

        private void UI_Load(object sender, EventArgs e)
        {

        }


    }
}
