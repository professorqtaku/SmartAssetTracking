using System;
using System.Collections.Generic;
using System.Text;

namespace SmartAssetTracking.Application.Helpers
{
    internal static class PrintHelper
    {
        public static void PrintColor(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
        Console.ResetColor();
    }

    public static void PrintSuccess(string text)
    {
        PrintColor(text, ConsoleColor.Green);
    }

    public static void PrintError(string text)
    {
        PrintColor(text, ConsoleColor.Red);
    }

    public static void Print(string text)
    {
        PrintColor(text, ConsoleColor.White);
    }
}
}
