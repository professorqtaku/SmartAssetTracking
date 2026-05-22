using SmartAssetTracking.Application.Helpers;
using SmartAssetTracking.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartAssetTracking.Application.Menus
{
    public class DeleteAssetMenu
    {

        public static void Show()
        {
            Console.Clear();
            string assetType = MainMenu.GetAssetType();

            Console.WriteLine($"\nProceeding to delete from {assetType} assets.");
            Console.Write("Enter the Asset ID to remove: ");

            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var assetService = new AssetService();
                bool isDeleted = assetService.DeleteAsset(id, assetType);

                if(isDeleted) PrintHelper.PrintSuccess("\nAsset removed successfully! Press Enter to go back...");
                    else Console.WriteLine("\nAsset ID not found. Press Enter to go back to menu...");
            }
            else
            {
                Console.WriteLine("\nInvalid ID structure.");
            }
            Console.ReadLine();
        }
    }
}