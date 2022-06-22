using KT.Proxy.BackendApi.Models;
using KT.Visitor.Data.Enums;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Linq;

namespace KT.Visitor.SelfApp.Views.Common
{
    public class OperateSucessPageViewModel : BindableBase
    {
        private string _warnText;
        private string _authModeTypeSuccessNames;
        private string _hasQrCodeAuth;

        public OperateSucessPageViewModel()
        {

        }

        public void Init(List<RegisterResultModel> results)
        {
            var authModelTypes = new List<string>();
            if (results?.FirstOrDefault() != null)
            {
                foreach (var item in results)
                {
                    if (item.AuthTypes?.FirstOrDefault() != null)
                    {
                        authModelTypes.AddRange(item.AuthTypes);
                    }
                    if (item.AuthType != null)
                    {
                        authModelTypes.Add(item.AuthType);
                    }
                }
            }

            Init(authModelTypes);
        }

        private void Init(List<string> authTypes)
        {
            if (authTypes == null || authTypes.FirstOrDefault() == null)
            {
                AuthModeTypeSuccessNames = "授权成功";
                WarnText = "请收好证件";
                return;
            }

            var warnStr = string.Empty;
            var typeNames = string.Empty;
            if (authTypes.Contains(AuthModelEnum.FACE.Value))
            {
                typeNames += "人脸";
                warnStr += "刷脸";
            }
            if (authTypes.Contains(AuthModelEnum.IC.Value))
            {
                if (!string.IsNullOrEmpty(typeNames))
                {
                    typeNames += "、";
                    warnStr += "、";
                }
                typeNames += "IC卡";
                warnStr += "刷卡";
            }
            if (authTypes.Contains(AuthModelEnum.QR.Value))
            {
                if (!string.IsNullOrEmpty(typeNames))
                {
                    typeNames += "、";
                    warnStr += "、";
                }
                typeNames += "二维码";
                warnStr += "刷二维码";
            }
            typeNames += "授权成功";
            warnStr += "即可通行";

            AuthModeTypeSuccessNames = typeNames;
            WarnText = warnStr;
        }

        public string WarnText
        {
            get
            {
                return _warnText;
            }
            set
            {
                SetProperty(ref _warnText, value);
            }
        }

        public string AuthModeTypeSuccessNames
        {
            get
            {
                return _authModeTypeSuccessNames;
            }

            set
            {
                SetProperty(ref _authModeTypeSuccessNames, value);
            }
        }

        public string HasQrCodeAuth
        {
            get
            {
                return _hasQrCodeAuth;
            }

            set
            {
                SetProperty(ref _hasQrCodeAuth, value);
            }
        }
    }
}

