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

        public static List<string> SortByList = new List<string>() { "Index(add order)", "Current mass", "Max Mass", "Sum cost", "Damage level" };

        public static int CurrentSortBy = 0;
        /// <summary>
        /// Let the menu initailize.
        /// </summary>
        public static void InitailizeMenu()
        {
            MenuEndPoint = false;
            Menu.OutMenuList = new List<string>() { $"Warehouse\nCapacity: {Warehouse.Capacity} Container\nCost per container: {Warehouse.CostPerContainer}Rub", "Container", "Sort parameter" };
            Menu.InMenuChoosenOne = 0;
            Menu.OutMenuChoosenOne = 0;
            Console.BackgroundColor = ConsoleColor.Black;
            Menu.CurrentMenuList = Warehouse.ContainerList.ConvertAll(new Converter<Container, string>(Warehouse.CotainerToIndex));
        }
        /// <summary>
        /// Print the main menu.
        /// </summary>
        /// <param name="ShowCaseMessage"></param>
        /// <param name="ErrorMessage"></param>
        public static void PrintOutUpAndDown(string ShowCaseMessage, string ErrorMessage)
        {
            Console.WriteLine(OutMenuList[OutMenuChoosenOne]);
            Console.WriteLine($"Sort by: {SortByList[CurrentSortBy]}");
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
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("A - add container, D - delete CHOOSEN container \nF - Read operaion and container info from file in the folder 'FileInput', S - Sort by menu");
            Console.WriteLine("Press ESC to exit program...");
            Console.WriteLine("Right arrow - Get in, Left arrow - get out, Up and Down arrow - Menu choose");
            // Some color change.
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(ShowCaseMessage);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(ErrorMessage);
        }
        /// <summary>
        /// Get all info of target menu to the current menu.
        /// </summary>
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
        /// <summary>
        /// Control the menu using keyboard.
        /// </summary>
        /// <param name="ExitCode"></param>

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
                    // Get in to the choosen menu.
                    if (OutMenuChoosenOne < OutMenuList.Count - 1 && CurrentMenuList.Count != 0)
                    {
                        if (!MenuEndPoint)
                        {
                            OutMenuChoosenOne += 1;
                            GetInToMenu();
                            InMenuChoosenOne = 0;
                        }
                    }
                    else if (OutMenuChoosenOne == 2 && CurrentMenuList.Count != 0)
                    {
                        CurrentSortBy = InMenuChoosenOne;
                        Warehouse.SortBy(InMenuChoosenOne);
                        OutMenuChoosenOne = 0;
                        CurrentMenuList = Warehouse.ContainerList.ConvertAll(new Converter<Container, string>(Warehouse.CotainerToIndex));
                    }


                    break;
                case (ConsoleKey.LeftArrow):
                    // Get out from current menu.
                    if (OutMenuChoosenOne != 0 && CurrentMenuList.Count != 0)
                    {
                        if (OutMenuChoosenOne != 2)
                        {
                            OutMenuChoosenOne -= 1;
                            InMenuChoosenOne = 0;
                            GetInToMenu();
                        }
                        else
                        {
                            OutMenuChoosenOne -= 2;
                            InMenuChoosenOne = 0;
                            GetInToMenu();
                        }
                    }
                    break;
                case (ConsoleKey.Escape):
                    // Escape.
                    ExitCode = true;
                    break;
                case (ConsoleKey.A):
                    // Example.
                    Console.WriteLine("Example:        2->2,3;4,10 ");
                    Console.WriteLine("                ^  ^ ^ ^-^");
                    Console.WriteLine("                |  | |  |");
                    Console.WriteLine("  Numbers of boxes | |  |");
                    Console.WriteLine("     Mass of one box |  |");
                    Console.WriteLine("     Cost per kilogram  |");
                    Console.WriteLine("Other boxes in the same format");
                    Console.Write("Input container info: ");
                    Program.AddContainer(Console.ReadLine(), ref Program.ContainerIndex);
                    CurrentMenuList = Warehouse.ContainerList.ConvertAll(new Converter<Container, string>(Warehouse.CotainerToIndex));
                    break;
                case (ConsoleKey.D):
                    // Delete Container.
                    Program.DeleteContainer(Warehouse.ContainerList[InMenuChoosenOne].Index);
                    CurrentMenuList = Warehouse.ContainerList.ConvertAll(new Converter<Container, string>(Warehouse.CotainerToIndex));
                    if (InMenuChoosenOne != 0)
                        InMenuChoosenOne--;
                    break;
                case (ConsoleKey.F):
                    // Input all form file.
                    Program.FileAddDelete();
                    CurrentMenuList = Warehouse.ContainerList.ConvertAll(new Converter<Container, string>(Warehouse.CotainerToIndex));
                    break;
                case (ConsoleKey.S):
                    /// Sum cost
                    /// Sum mass
                    /// Max Mass
                    /// Index
                    /// Damage Level
                    if (OutMenuChoosenOne < OutMenuList.Count && !MenuEndPoint)
                    {
                        OutMenuChoosenOne = 2;
                        InMenuChoosenOne = 0;
                        CurrentMenuList = SortByList;
                    }
                    break;

            }
        }
    }
}