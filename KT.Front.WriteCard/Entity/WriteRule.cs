using KT.Proxy.BackendApi.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Front.WriteCard.Entity
{
    public abstract class WriteRule
    {
        public CommonClass.WriteRuleEnum RuleType { get; set; }
        public string Name { get; set; }
        public abstract Task<bool> WriteCardAsync(List<int> elevatorFloorIds, string deviceCode);
        public abstract Task<bool> InitCardAsync();
    }
}
