using KT.Quanta.Kone.ToolApp.Models;
using System.Collections.Generic;

namespace KT.Quanta.Kone.ToolApp.Helper
{
    public class DopSpecificMaskSelectModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        public List<DopSpecificDefaultAccessFloorMaskViewModel> MaskFloors { get; set; }
    }
}