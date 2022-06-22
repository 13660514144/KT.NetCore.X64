using KT.Proxy.BackendApi.Helpers;
using Newtonsoft.Json;

namespace KT.Proxy.BackendApi.Models
{
    public class VisitorSettingModel
    {
        private string _systemLogo;
        public string SystemLogo
        {
            get
            {
                return _systemLogo;
            }
            set
            {
                _systemLogo = value;
                if (!string.IsNullOrEmpty(_systemLogo))
                {
                    ServerSystemLogo = StaticInfo.ServerAddress + value;
                }
                else
                {
                    ServerSystemLogo = string.Empty;
                }
            }
        }

        [JsonIgnore]
        public string ServerSystemLogo { get; set; }

        public string SystemName { get; set; }
    }
}
