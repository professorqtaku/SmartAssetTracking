using SmartAssetTracking.Entities
    ;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartAssetTracking.Application.Helpers
{
    public static class AssetParser
    {

        public static ComputerFormFactor ParseComputerFormFactor(string input, ComputerFormFactor defaultValue)
        {
            if (string.IsNullOrWhiteSpace(input)) return defaultValue;

            return input.Trim().ToLower() switch
            {
                "l" => ComputerFormFactor.Laptop,
                "s" => ComputerFormFactor.Server,
                "d" => ComputerFormFactor.Desktop,
                "a" => ComputerFormFactor.AllInOne,
                _ => ComputerFormFactor.Other
            };
        }

        public static MobileDeviceType ParseMobileDeviceType(string input, MobileDeviceType defaultValue)
        {
            // If the user left it empty, return the existing database value unchanged
            if (string.IsNullOrWhiteSpace(input))
            {
                return defaultValue;
            }

            return input.Trim().ToLower() switch
            {
                "t" => MobileDeviceType.Tablet,
                "p" => MobileDeviceType.Phone,
                _ => MobileDeviceType.Other
            };
        }
    }
}
