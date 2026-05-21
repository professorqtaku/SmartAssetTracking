using System;
using System.Collections.Generic;
using System.Text;

namespace SmartAssetTracking.Entities.Asset
{
    public enum ComputerFormFactor
    {
        Laptop,
        Desktop,
        AllInOne,
        Server,
        Other
    }
    public class ComputerAsset : Asset
    {
        public ComputerFormFactor FormFactor { get; set; } = ComputerFormFactor.Desktop;
    }
}
