using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Proxy.BackendApi.Models
{
    public class ElevatorAuthInfoModel
    {
        public string Name { get; set; }

        public string CompanyName { get; set; }

        public string DepartmentName { get; set; }

        public List<ElevatorAuthModel> AuthInfos { get; set; }
    }
}
