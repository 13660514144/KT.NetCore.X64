using Panuon.UI.Silver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace KT.Common.WpfApp.Helpers
{
    public class WindowHelper
    {
        /// <summary>
        /// 设置全屏
        /// </summary>
        /// <param name="window"></param>
        public static void ReleaseFullWindow(WindowX window, bool isFullScreen)
        {
            if (isFullScreen)
            {

                FullWindow(window);

            }
        }

        /// <summary>
        /// 设置全屏
        /// </summary>
        /// <param name="window"></param>
        private static void FullWindow(WindowX window)
        {
            WindowXCaption.SetHideBasicButtons(window, true);
            WindowX.SetIsDragMoveArea(window, false);
            WindowXCaption.SetHeight(window, 0);

            window.WindowState = System.Windows.WindowState.Normal;
            window.WindowStyle = System.Windows.WindowStyle.None;
            window.ResizeMode = System.Windows.ResizeMode.NoResize;
            window.Topmost = true;

            window.Left = -3;
            window.Top = -3;
            window.Width = System.Windows.SystemParameters.PrimaryScreenWidth + 6;
            window.Height = System.Windows.SystemParameters.PrimaryScreenHeight + 6;
        }
    }
}
