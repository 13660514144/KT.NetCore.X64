using KT.Common.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Elevator.Unit.Secondary.ClientApp.Service.Network.Helpers
{
    /// <summary>
    /// 操作模块
    /// </summary>
    public class ModuleEnum : BaseEnum
    {
        public ModuleEnum(int code, string value, string text) : base(code, value, text)
        {
        }

        public static ModuleEnum All => new ModuleEnum(1, "All", "所有");

        public const string AllValue = "All";

        public const int AllCode = 1;
    }
}
