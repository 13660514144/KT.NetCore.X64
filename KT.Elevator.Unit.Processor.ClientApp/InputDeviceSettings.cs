using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Elevator.Unit.Processor.ClientApp
{
    /// <summary>
    /// 输入设备配置
    /// </summary>
    public class InputDeviceSettings
    {
        /// <summary>
        /// 相机设置
        /// </summary>
        public CameraSettings CameraSettings { get; set; }
    }

    /// <summary>
    /// 相机设置
    /// </summary>
    public class CameraSettings
    {
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnable { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
    }
}
