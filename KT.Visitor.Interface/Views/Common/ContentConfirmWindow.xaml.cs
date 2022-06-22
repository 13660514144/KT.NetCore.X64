using Panuon.UI.Silver;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KT.Visitor.Interface.Views.Common
{
    /// <summary>
    /// ContentConfirmWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ContentConfirmWindow : WindowX, IDialogWindow
    {
        public ContentConfirmWindow()
        {
            InitializeComponent();
        }

        public IDialogResult Result { get; set; }
    }
}
