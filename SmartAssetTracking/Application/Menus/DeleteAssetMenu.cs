using SmartAssetTracking.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartAssetTracking.Application.Menus
{
    public class DeleteAssetMenu
    {
        // Added the service instance so it can be called below
        private static readonly ComputerAssetService _computerService = new ComputerAssetService();
        private static readonly MobileAssetService _mobileService = new MobileAssetService();


        public static void Show()
        {
            Console.Clear();
            string assetType = MainMenu.GetAssetType();

            Console.WriteLine($"\nProceeding to delete from {assetType} assets.");
            Console.Write("Enter the Asset ID to remove: ");

            if (int.TryParse(Console.ReadLine(), out int id))
            {
                bool isDeleted = false;
                if (assetType.Equals("Computer", StringComparison.OrdinalIgnoreCase))
                {
                    isDeleted = _computerService.DeleteAsset(id);
                } else
                    isDeleted = _mobileService.DeleteAsset(id);
                {

                if(isDeleted) Console.WriteLine("\nAsset removed successfully! Press Enter to go back...");
                    else Console.WriteLine("\nAsset ID not found. Press Enter to go back to menu...");
                }
            }
            else
            {
                Console.WriteLine("\nInvalid ID structure.");
            }
            Console.ReadLine();
        }
    }
}