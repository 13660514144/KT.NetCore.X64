using KT.Common.WpfApp.ViewModels;
using KT.Proxy.BackendApi.Models;
using KT.Visitor.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KT.Visitor.Interface.Views.Register
{
    public class SuccessWindowViewModel : BindableBase
    {
        private string _warnText;
        private string _authModeTypeSuccessNames;

        public SuccessWindowViewModel()
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
            WarnText = "请提醒访客收好通行凭证和证件";

            if (authTypes == null || authTypes.FirstOrDefault() == null)
            {
                AuthModeTypeSuccessNames = "授权成功";
                return;
            }

            var typeNames = string.Empty;
            if (authTypes.Contains(AuthModelEnum.FACE.Value))
            {
                typeNames += "人脸";
            }
            if (authTypes.Contains(AuthModelEnum.IC.Value))
            {
                if (!string.IsNullOrEmpty(typeNames))
                {
                    typeNames += "、";
                }
                typeNames += "IC卡";
            }
            if (authTypes.Contains(AuthModelEnum.QR.Value))
            {
                if (!string.IsNullOrEmpty(typeNames))
                {
                    typeNames += "、";
                }
                typeNames += "二维码";
            }
            typeNames += "授权成功";

            AuthModeTypeSuccessNames = typeNames;
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
    }
}
