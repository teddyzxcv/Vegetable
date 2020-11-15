using System;
using System.Collections.Generic;
namespace Vegetable
{
    public class Menu
    {
        public static int OutMenuChoosenOne { get; set; }

        public static List<string> OutMenuList { get; set; }
        public static int InMenuChoosenOne { get; set; }
        public static List<string> CurrentMenuList { get; set; }

        public static bool MenuEndPoint { get; set; }

        public static void InitailizeMenu()
        {
            MenuEndPoint = false;
            Menu.OutMenuList = new List<string>() { $"Warehouse\nCapacity: {Warehouse.Capacity}\nCost per container: {Warehouse.CostPerContainer}", "Container" };
            Menu.InMenuChoosenOne = 0;
            Menu.OutMenuChoosenOne = 0;
            Console.BackgroundColor = ConsoleColor.Black;
            Menu.CurrentMenuList = Warehouse.ContainerList.ConvertAll(new Converter<Container, string>(Warehouse.CotainerToIndex));
        }

        public static void PrintOutUpAndDown(string ShowCaseMessage)
        {
            Console.WriteLine(OutMenuList[OutMenuChoosenOne] + "    Press ESC to exit program...");
            Console.WriteLine("------------------------------------------");

            // The loop that goes through all of the menu items.
            for (int i = 0; i < CurrentMenuList.Count; i++)
            {
                // Print all output;
                if (i == InMenuChoosenOne)
                {
                    // HighLight the choosen one.
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine($">>{CurrentMenuList[i]}");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"  {CurrentMenuList[i]}");
                }
            }
            Console.WriteLine(ShowCaseMessage);
        }

        public static void GetInToMenu()
        {
            switch (OutMenuChoosenOne)
            {
                case (0):
                    if (MenuEndPoint)
                    {
                        CurrentMenuList = Warehouse.ContainerList.ConvertAll(new Converter<Container, string>(Warehouse.CotainerToIndex));
                        MenuEndPoint = false;
                    }
                    break;
                case (1):
                    if (!MenuEndPoint)
                    {
                        OutMenuList[OutMenuChoosenOne] = CurrentMenuList[InMenuChoosenOne];
                        CurrentMenuList = new List<string>(Warehouse.ContainerList[InMenuChoosenOne].ToString().Split("\n"));
                        MenuEndPoint = true;
                    }
                    break;
            }

        }



        public static void MenuControl(out bool ExitCode)
        {
            ExitCode = false;
            ConsoleKeyInfo key = Console.ReadKey(true);
            // Simple switch, if uparrow then decrease, downarrow then increase.
            switch (key.Key)
            {
                case (ConsoleKey.UpArrow):
                    // Move cursor up.
                    if (!MenuEndPoint)
                        if (InMenuChoosenOne == 0)
                            InMenuChoosenOne = CurrentMenuList.Count - 1;
                        else
                            InMenuChoosenOne--;
                    break;
                case (ConsoleKey.DownArrow):
                    // Move cursor down.
                    if (!MenuEndPoint)
                        if (InMenuChoosenOne == CurrentMenuList.Count - 1)
                            InMenuChoosenOne = 0;
                        else
                            InMenuChoosenOne++;
                    break;
                case (ConsoleKey.RightArrow):
                    if (OutMenuChoosenOne < OutMenuList.Count && !MenuEndPoint)
                    {
                        OutMenuChoosenOne += 1;
                        GetInToMenu();
                        InMenuChoosenOne = 0;

                    }
                    break;
                case (ConsoleKey.LeftArrow):
                    if (OutMenuChoosenOne != 0)
                    {
                        OutMenuChoosenOne -= 1;
                        InMenuChoosenOne = 0;
                        GetInToMenu();
                    }
                    break;
                case (ConsoleKey.Escape):
                    ExitCode = true;
                    break;

                case (ConsoleKey.A):
                    Console.Write("Input container info: ");
                    Program.AddContainer(Console.ReadLine(), ref Program.ContainerIndex);
                    CurrentMenuList = Warehouse.ContainerList.ConvertAll(new Converter<Container, string>(Warehouse.CotainerToIndex));
                    break;
                case (ConsoleKey.D):
                    Program.DeleteContainer(Warehouse.ContainerList[InMenuChoosenOne].Index);
                    CurrentMenuList = Warehouse.ContainerList.ConvertAll(new Converter<Container, string>(Warehouse.CotainerToIndex));
                    InMenuChoosenOne--;
                    break;
                case (ConsoleKey.F):
                    Program.FileAddDelete();
                    CurrentMenuList = Warehouse.ContainerList.ConvertAll(new Converter<Container, string>(Warehouse.CotainerToIndex));
                    break;

            }
        }
    }
}