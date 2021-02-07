using System;
using System.Runtime.InteropServices;


namespace JJKid.Windows
{
    public class TransparentBackground
    {
        private struct MARGINS
        {
            public int cxLeftWidth;
            public int cxRightWidth;
            public int cyTopHeight;
            public int cyBottomHeight;
        }

        [DllImport("Dwmapi.dll")]
        private static extern uint DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS margins);

        [DllImport("user32.dll")]
        private static extern IntPtr GetActiveWindow();

        private bool hideOnStart = false;
        private WindowsBar windowsBar = null;




        public void run()
        {
            IntPtr hWnd = TransparentBackground.GetActiveWindow();

            MARGINS margins = new MARGINS { cxLeftWidth = -1 };
            DwmExtendFrameIntoClientArea(hWnd, ref margins);
        }
    }
}