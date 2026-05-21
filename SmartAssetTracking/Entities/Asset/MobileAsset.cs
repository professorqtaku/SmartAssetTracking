using System;
using System.Collections.Generic;
using System.Text;

namespace SmartAssetTracking.Entities.Asset
{
    public enum MobileDeviceType
    {
        Phone,
        Tablet,
        Other
    }
    public class MobileAsset : Asset
    {
        public MobileDeviceType DeviceType { get; set; } = MobileDeviceType.Other;
    }
}
