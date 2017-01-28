using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace agent
{
    public static class ProcessExtensions
    {
        public static Process ParentProcess(this Process process)
        {
            try
            {
                int parentPid = 0;
                int processPid = process.Id;
                uint TH32CS_SNAPPROCESS = 2;
                IntPtr hSnapshot = CreateToolhelp32Snapshot(TH32CS_SNAPPROCESS, 0);

                if (hSnapshot == IntPtr.Zero)
                    return null;

                var procInfo = new PROCESSENTRY32();
                procInfo.dwSize = (uint)Marshal.SizeOf(typeof(PROCESSENTRY32));

                if (Process32First(hSnapshot, ref procInfo) == false)
                    return null;

                do
                {
                    if (processPid == procInfo.th32ProcessID)
                        parentPid = (int)procInfo.th32ParentProcessID;
                }
                while (parentPid == 0 && Process32Next(hSnapshot, ref procInfo));

                if (parentPid > 0)
                    return Process.GetProcessById(parentPid);

                return null;
            }
            catch { return null; }
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr CreateToolhelp32Snapshot(uint dwFlags, uint th32ProcessID);

        [DllImport("kernel32.dll")]
        private static extern bool Process32First(IntPtr hSnapshot, ref PROCESSENTRY32 lppe);

        [DllImport("kernel32.dll")]
        private static extern bool Process32Next(IntPtr hSnapshot, ref PROCESSENTRY32 lppe);

        [StructLayout(LayoutKind.Sequential)]
        private struct PROCESSENTRY32
        {
            public uint dwSize;
            public uint cntUsage;
            public uint th32ProcessID;
            public IntPtr th32DefaultHeapID;
            public uint th32ModuleID;
            public uint cntThreads;
            public uint th32ParentProcessID;
            public int pcPriClassBase;
            public uint dwFlags;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szExeFile;
        }
    }
}
