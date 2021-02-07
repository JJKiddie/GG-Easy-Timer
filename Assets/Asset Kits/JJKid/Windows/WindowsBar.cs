using System;
using System.Runtime.InteropServices;
using UnityEngine;


namespace JJKid.Windows
{
    public class WindowsBar
    {
        private const int SWP_HIDEWINDOW    = 0x80;         // hide window flag.
        private const int SWP_SHOWWINDOW    = 0x40;         // show window flag.
        private const int SWP_NOMOVE        = 0x0002;       // don't move the window flag.
        private const int SWP_NOSIZE        = 0x0001;       // don't resize the window flag.
        private const uint WS_SIZEBOX       = 0x00040000;
        private const int GWL_STYLE         = -16;
        private const int WS_BORDER         = 0x00800000;   // window with border
        private const int WS_DLGFRAME       = 0x00400000;   // window with double border but no title
        private const int WS_CAPTION        = WS_BORDER | WS_DLGFRAME;  // window with a title bar
        private const int WS_EX_LAYERED     = 0x00080000;   // window with no borders etc.
        private const int WS_EX_TRANSPARENT = 0x00000020;
        private const int WS_MAXIMIZEBOX    = 0x00010000;
        private const int WS_MINIMIZEBOX    = 0x00020000;   // window with minimizebox

        [DllImport("user32.dll")]
        static extern IntPtr GetActiveWindow();

        [DllImport("user32.dll")]
        static extern bool SetWindowPos(
            IntPtr hWnd,            // window handle
            IntPtr hWndInsertAfter, // placement order of the window
            short X,                // x position
            short Y,                // y position
            short cx,               // width
            short cy,               // height
            uint uFlags             // window flags.
        );

        [DllImport("user32.dll")]
        static extern IntPtr SetWindowLong(
             IntPtr hWnd, // window handle
             int nIndex,
             uint dwNewLong
        );

        [DllImport("user32.dll")]
        static extern IntPtr GetWindowLong(
            IntPtr hWnd,
            int nIndex
        );

        private IntPtr hWnd;
        private IntPtr HWND_TOP = new IntPtr(0);
        private IntPtr HWND_TOPMOST = new IntPtr(-1);
        private IntPtr HWND_NOTOPMOST = new IntPtr(-2);




        public WindowsBar()
        {
            //Gets the currently active window handle for use in the user32.dll functions.
            this.hWnd = WindowsBar.GetActiveWindow();
        }

        public void showWindowBorders(bool show)
        {
            // We don't want to hide the toolbar from our editor!
            if(Application.isEditor)
                return;

            // gets current style
            int style = WindowsBar.GetWindowLong(this.hWnd, GWL_STYLE).ToInt32();

            if(show)
            {
                // Adds caption and the sizebox back.
                WindowsBar.SetWindowLong(this.hWnd, GWL_STYLE, (uint)(style | WS_CAPTION | WS_SIZEBOX));
                // Make the window normal.
                WindowsBar.SetWindowPos(this.hWnd, HWND_NOTOPMOST, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE | SWP_SHOWWINDOW);
            }
            else
            {
                // removes caption and the sizebox from current style.
                WindowsBar.SetWindowLong(this.hWnd, GWL_STYLE, (uint)(style & ~(WS_CAPTION | WS_SIZEBOX)));
                // Make the window render above toolbar.
                WindowsBar.SetWindowPos(this.hWnd, HWND_TOPMOST, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE | SWP_SHOWWINDOW);
            }
        }
    }
}