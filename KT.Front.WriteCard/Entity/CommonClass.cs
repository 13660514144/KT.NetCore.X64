using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Front.WriteCard.Entity
{
    public class CommonClass
    {
        public class DeviceType 
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
 
        public enum WriteCardEnum
        {
            UnWrite = -1,
            Success = 0,
            Writing = 1,
            Faild = 2
        }

        public enum WriteRuleEnum
        {
            HITACHI = 1,
        }
    }
}
