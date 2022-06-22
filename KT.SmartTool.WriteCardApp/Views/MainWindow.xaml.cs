using KT.SmartTool.WriteCardApp.ViewModels;
using System;
using System.Windows;

namespace KT.SmartTool.WriteCardApp.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        // *******************************注意事项*********************************
        //D3000M读卡器扇区的第四块密码块不能写入或删除数据，不然会导致该块内容无法再读写
        //日立规则文档中说要写入的是底0x05扇区，但文档对应的是第14块，与卡的块对应不上，需要实际测试

        //TODO 因为从读卡器获取物理卡号时，页面没有实时渲染绑定值，需要把控件传入到viewmodel。需要解决异步问题
        MainWindowViewModel mainVM = new MainWindowViewModel();
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = mainVM;
            mainVM.Regist(txt_cardNum);
        }
    }
}
