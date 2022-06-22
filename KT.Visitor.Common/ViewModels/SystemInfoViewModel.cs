using KT.Common.WpfApp.ViewModels;
using KT.Visitor.Common.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace KT.Visitor.Common.ViewModels
{
    public class SystemInfoViewModel : BindableBase
    {
        private string _logoPath;
        private string _systemName;
        private string _systemSecondName;

        private string _nowYear;
        private string _systemCopyright;

        private AppSettings _appSettings;

        public SystemInfoViewModel(AppSettings appSettings)
        {
            _appSettings = appSettings;

            if (_appSettings.LogoPath.Contains(":"))
            {
                LogoPath = _appSettings.LogoPath;
            }
            else
            {
                LogoPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _appSettings.LogoPath);
            }
            SystemName = _appSettings.SystemName;
            SystemSecondName = _appSettings.SystemSecondName;
            SystemCopyright = _appSettings.SystemCopyright;
            NowYear = DateTime.Now.ToString("yyyy");
        }


        public string NowYear
        {
            get
            {
                return _nowYear;
            }

            set
            {
                SetProperty(ref _nowYear, value);
            }
        }

        public string LogoPath
        {
            get
            {
                return _logoPath;
            }

            set
            {
                SetProperty(ref _logoPath, value);
            }
        }

        public string SystemName
        {
            get
            {
                return _systemName;
            }

            set
            {
                SetProperty(ref _systemName, value);
            }
        }

        public string SystemSecondName
        {
            get
            {
                return _systemSecondName;
            }

            set
            {
                SetProperty(ref _systemSecondName, value);
            }
        }

        public string SystemCopyright
        {
            get
            {
                return _systemCopyright;
            }

            set
            {
                SetProperty(ref _systemCopyright, value);
            }
        }
    }
}
