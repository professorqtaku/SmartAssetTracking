using Microsoft.IdentityModel.Tokens;
using SmartAssetTracking.Entities.Asset;
using SmartAssetTracking.Services;
using System;
namespace SmartAssetTracking.Application.Menus
{
    class MainMenu
    {

        public static void Show()
        {
            bool running = true;
            while (running)
            {
                Console.ResetColor();
                Console.Clear();
                Console.WriteLine("=== Enterprise Asset Tracking System ===");
                Console.WriteLine("1. View All Assets (Sorted & Highlighted)");
                Console.WriteLine("2. Register New Computer");
                Console.WriteLine("3. Register New Mobile Device");
                Console.WriteLine("4. Update an Asset");
                Console.WriteLine("5. Remove an Asset");
                Console.WriteLine("0. Exit");
                Console.Write("\nSelect an option: ");

                switch (Console.ReadLine())
                {
                    case "1": AssetListMenu.Show(); break;
                    case "2": CreateAssetMenu.CreateComputer(); break;
                    case "3": CreateAssetMenu.CreateMobile(); break;
                    case "4": UpdateAssetMenu.Show(); break;
                    case "5": DeleteAssetMenu.Show(); break;
                    case "0": running = false; break;
                    default: Console.WriteLine("Invalid option. Press Enter to retry."); Console.ReadLine(); break;
                }
            }
        }

        public static string GetAssetType()
        {
            Console.WriteLine("Is this a Computer or Mobile asset? (Press 'c' for Computer, or any other key for Mobile):");
            string input = Console.ReadLine() ?? string.Empty;
                // Checks if input equals "c" (ignoring casing and extra spaces)
                if (input.Trim().Equals("c", StringComparison.OrdinalIgnoreCase))
                {
                    return "Computer";
                }
                // Default fallback
                return "Mobile";
        }
    }
}