using System;
using System.Collections.Generic;

namespace KT.WinPak.SDK.V48.Entities
{
    public partial class HwindependentDevices
    {
        public int RecordId { get; set; }
        public int? AccountId { get; set; }
        public DateTime? TimeStamp { get; set; }
        public int? UserId { get; set; }
        public int? NodeId { get; set; }
        public byte? Deleted { get; set; }
        public short? UserPriority { get; set; }
        public int? DeviceId { get; set; }
        public int? CommServerId { get; set; }
        public int? DeviceType { get; set; }
        public short? DeviceSubType1 { get; set; }
        public short? DeviceSubType2 { get; set; }
        public short? DeviceSubType3 { get; set; }
        public int? HwdeviceId { get; set; }
        public short? HwdeviceSubId1 { get; set; }
        public short? HwdeviceSubId2 { get; set; }
        public short? HwdeviceSubId3 { get; set; }
        public short? HwdeviceSubId4 { get; set; }
        public short? HwdeviceSubId5 { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Desc2 { get; set; }
        public string Desc3 { get; set; }
        public string Desc4 { get; set; }
        public string Desc5 { get; set; }
        public int? CommandFileId { get; set; }
        public int? FloorPlanId { get; set; }
        public int? ActionGroupId { get; set; }
        public int? LandisHwdeviceId { get; set; }
        public int? LandisPointId { get; set; }
        public int? LandisOldPointId { get; set; }
        public byte? InControlTree { get; set; }
        public short? SpareW1 { get; set; }
        public short? SpareW2 { get; set; }
        public short? SpareW3 { get; set; }
        public short? SpareW4 { get; set; }
        public int? SpareDw1 { get; set; }
        public int? SpareDw2 { get; set; }
        public int? SpareDw3 { get; set; }
        public int? SpareDw4 { get; set; }
        public string SpareStr1 { get; set; }
        public string SpareStr2 { get; set; }
    }
}
