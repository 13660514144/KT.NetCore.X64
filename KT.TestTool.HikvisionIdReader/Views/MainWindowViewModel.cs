using IDevices;
using KT.Common.WpfApp.Helpers;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Windows.Input;

namespace KT.TestTool.HikvisionIdReader.Views
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "海康读卡器测试";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        private string _devices;
        public string Devices
        {
            get { return _devices; }
            set { SetProperty(ref _devices, value); }
        }

        private ICommand _searchDeviceCommand;
        public ICommand SearchDeviceCommand => _searchDeviceCommand ??= new DelegateCommand(SearchDevice);
        private ICommand _readIdCardCommand;
        public ICommand ReadIdCardCommand => _readIdCardCommand ??= new DelegateCommand(ReadIdCard);

        private IDevice _device;
        public MainWindowViewModel()
        {

        }
        public void ViewLoaded(object sender, EventArgs e)
        {
            _device = ContainerHelper.Resolve<DSK5022>();
            _device.InitComm();
        }

        private void SearchDevice()
        {
            Devices = string.Empty;
            _device.ResultCallBack = ShowDevice;
            _device.Authenticate();
        }

        private void ReadIdCard()
        {
            _device.ReadContent();
        }


        private void ShowDevice(object obj)
        {
            Devices += obj + Environment.NewLine;
        }
    }
}
