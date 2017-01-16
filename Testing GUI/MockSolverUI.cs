using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using GUIs.GDI_Helpers;
using Deadline;
using GUIs;

namespace TestingGUI
{
    // Just a stub, actually SolverUI should be able to work using only interface IServer
    public class MockSolverUI : SolverUI
    {
        public MockSolverUI(IServer c)
            : base(null)
        {
        }
    }
}
