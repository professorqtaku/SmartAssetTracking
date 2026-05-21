
namespace AssetTracking2.Application
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
                Console.WriteLine("4. Remove an Asset");
                Console.WriteLine("5. Exit");
                Console.Write("\nSelect an option: ");

                //switch (Console.ReadLine())
                //{
                //    case "1":  break;
                //    case "2": CreateComputer(); break;
                //    case "3": CreateMobile(); break;
                //    case "4": DeleteAsset(); break;
                //    case "5": running = false; break;
                //    default: Console.WriteLine("Invalid option. Press Enter to retry."); Console.ReadLine(); break;
                //}
            }
        }

    }
}