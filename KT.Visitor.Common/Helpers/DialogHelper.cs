using Panuon.UI.Silver;
using System.Collections.Generic;

namespace KT.Visitor.Common.Helpers
{
    public class DialogHelper
    {
        public Stack<WindowX> _windows;

        public DialogHelper()
        {
            _windows = new Stack<WindowX>();
        }

        public void AddFirst(WindowX window)
        {
            _windows.Push(window);
        }

        public void RemoveFirst()
        {
            if (_windows.Count > 0)
            {
                _windows.Pop();
            }
        }

        /// <summary>
        /// 子窗口弹窗
        /// </summary>
        /// <param name="window"></param>
        /// <returns></returns>
        public bool? ShowDialog(WindowX window)
        {
            //1.获取父窗口
            var parentWindow = PeekParent(window);

            //2.将当前窗口加入窗口队列
            _windows.Push(window);

            //显示蒙版
            if (parentWindow != null)
            {
                parentWindow.IsMaskVisible = true;
                window.Owner = parentWindow;
            }
            else
            {
                //没有父类时直接返回，否则会弹窗到底下一层关不掉
                return false;
            }

            //3.设备并弹出当前操作窗口
            var result = ShowDialog(window, parentWindow);

            //隐藏蒙版
            if (parentWindow != null)
            {
                parentWindow.IsMaskVisible = false;
            }

            //4.清除队列中的操作窗口
            _windows.Pop();

            //5.返回结果
            return result;
        }

        /// <summary>
        /// 父窗口弹窗
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="window"></param>
        public void Show<T>(T window) where T : WindowX
        {
            //1.将当前窗口加入窗口队列
            _windows.Push(window);

            //2.设备并弹出当前操作窗口 
            window.Show();
        }

        private WindowX PeekParent(WindowX child)
        {
            if (_windows.Count == 0)
            {
                return null;
            }

            var parent = _windows.Peek();
            //窗口未激活不能作父类，目前界面未知激活状态
            if (!parent.IsActive)
            {
                if (_windows.Count == 1)
                {
                    if ((child.Name.EndsWith(".Views.MainWindow") && parent.Name.EndsWith(".Views.LoginWindow"))
                        || (parent.Name.EndsWith(".Views.MainWindow") && child.Name.EndsWith(".Views.LoginWindow")))
                    {
                        _windows.Pop();
                        return PeekParent(child);
                    }
                }
                else
                {
                    _windows.Pop();
                    return PeekParent(child);
                }

            }
            return parent;
        }

        private bool? ShowDialog(WindowX window, WindowX parentWindow)
        {
            //显示蒙版
            if (parentWindow != null)
            {
                window.Owner = parentWindow;
                parentWindow.IsMaskVisible = true;
            }

            //3.设备并弹出当前操作窗口
            var result = window.ShowDialog();

            //隐藏蒙版
            if (parentWindow != null)
            {
                parentWindow.IsMaskVisible = false;
            }

            return result;
        }
    }
}
