using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestingGUI
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
            Program.solverUI.DrawState(e.Graphics, Size);
        }


        void UI_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Program.solverUI.Click(e.Button, e.Location);
            this.Refresh();
        }

    }
}
