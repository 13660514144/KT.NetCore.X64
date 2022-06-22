using KT.Quanta.Kone.ToolApp.Models;
using System.Collections.Generic;

namespace KT.Quanta.Kone.ToolApp.Helper
{
    public class DopGlobalMaskSelectModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        public List<DopGlobalDefaultAccessFloorMaskViewModel> MaskFloors { get; set; }
    }
}