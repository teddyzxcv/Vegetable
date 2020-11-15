﻿using System;
using System.Collections.Generic;
using System.IO;
namespace Vegetable
{
    class Program
    {
        public static string ErrorMessage;
        static int ContainerExcluded = 0;

        public static void AddContainer(string InputLine, ref int index)
        {
            try
            {
                int BoxExcluded = 0;
                Random rn = new Random();
                string[] InputInfo = InputLine.Split("->");
                int SumNumber = int.Parse(InputInfo[0]);
                string[] BoxListInfo = InputInfo[1].Split(';');
                Container container = new Container(rn.Next(0, 50), rn.Next(50, 1000), index);
                if (!InputLine.Contains("->") || !InputLine.Contains(',') || SumNumber != BoxListInfo.Length)
                    throw new Exception();
                for (int i = 0; i < SumNumber; i++)
                {
                    if (BoxListInfo[i].Split(',').Length != 2)
                        throw new Exception();
                    double boxmass = double.Parse(BoxListInfo[i].Split(',')[0]);
                    double boxcost = double.Parse(BoxListInfo[i].Split(',')[1]);
                    Box box = new Box(boxmass, boxcost);
                    container.BoxList.Add(box);
                    if (!(container.CurrentMass < container.MaxMass))
                    {

                        if (container.CurrentMass == container.MaxMass)
                        {
                            ErrorMessage = "Can't add more box, because limit of container was reached";
                            return;
                        }

                        else
                        {
                            container.BoxList.RemoveAt(container.BoxList.Count - 1);
                            BoxExcluded++;
                        }
                    }

                }
                index += 1;
                container.Index = index;
                if (BoxExcluded != 0)
                    if (BoxExcluded != SumNumber)
                        ErrorMessage += $"Can't add more box in the Container N.{index}, because limit of container was reached,\n {BoxExcluded} boxes wasn't put into the container.\n";
                    else
                        ErrorMessage += $"Can't add Container N.{index}, because no box can't be add into the container(they are all so heavy).\n";
                if (Warehouse.Capacity == Warehouse.ContainerList.Count)
                {
                    Warehouse.SortBy(0);
                    Warehouse.ContainerList.RemoveAt(0);
                    ContainerExcluded++;
                    Warehouse.SortBy(Menu.CurrentSortBy);
                }
                if (Warehouse.CheckCost(container))
                    Warehouse.ContainerList.Add(container);
                else
                if (BoxExcluded != SumNumber)
                    ErrorMessage += $"Container N.{index} can't put into the warehouse, because cost of container is less than cost of rent in warehouse\n";


            }
            catch
            {
                ErrorMessage = "Incorrect console input!!!";
            }
        }

        public static void DeleteContainer(int delIndex)
        {
            for (int i = 0; i < Warehouse.ContainerList.Count; i++)
            {
                if (Warehouse.ContainerList[i].Index == delIndex)
                {
                    Warehouse.ContainerList.RemoveAt(i);
                    return;
                }
            }
        }

        public static int ContainerIndex = 0;

        public static void FileAddDelete()
        {
            try
            {
                Console.WriteLine("At OperationInfo: For each line: '+' to add a container,'-' to delete a container");
                Console.WriteLine("At ContainerInfo: For each line: if line in 'OperationInfo' are '+', then info of container must write in format:");
                Console.WriteLine("Example:        2->2,3;4,10 ");
                Console.WriteLine("                ^  ^ ^ ^-^");
                Console.WriteLine("                |  | |  |");
                Console.WriteLine("  Numbers of boxes | |  |");
                Console.WriteLine("     Mass of one box |  |");
                Console.WriteLine("     Cost per kilogram  |");
                Console.WriteLine("Other boxes in the same format");
                Console.WriteLine("if line are '-', then just the N. of container that need to delete. All line and opertation must correspond each other.");
                Console.WriteLine("In the folder 'FileInput' has all example, plz, change the file there and press enter..");
                Console.ReadKey(true);
                string[] ContainerInfo = File.ReadAllLines(@"FileInput" + Path.DirectorySeparatorChar + "ContainerInfo.txt");
                string[] OperationInfo = File.ReadAllLines(@"FileInput" + Path.DirectorySeparatorChar + "OperationInfo.txt");
                string[] WarehouseInfo = File.ReadAllLines(@"FileInput" + Path.DirectorySeparatorChar + "WarehouseInfo.txt");
                if (WarehouseInfo.Length == 2 && ContainerInfo.Length == OperationInfo.Length)
                {
                    ContainerExcluded = 0;
                    for (int i = 0; i < ContainerInfo.Length; i++)
                    {
                        if (OperationInfo[i] == "+")
                        {
                            AddContainer(ContainerInfo[i], ref ContainerIndex);
                        }
                        else if (OperationInfo[i] == "-")
                        {
                            DeleteContainer(int.Parse(ContainerInfo[i]));
                        }
                        else
                            throw new Exception();
                    }
                }
                else
                    throw new Exception();
                if (ContainerExcluded != 0)
                    ErrorMessage += $"{ContainerExcluded} have been excluded due the lack of space in warehouse.";

            }
            catch
            {
                ErrorMessage = "Incorrect file input!!!";
            }
        }
        static void InputWarehouseInfo()
        {
            do
            {
                Console.Clear();
                try
                {
                    Console.Write("Input method(file or console): ");
                    string OperationInput = Console.ReadLine();
                    if (OperationInput == "console")
                    {
                        Console.Write("Input warehouse capacity: ");
                        Warehouse.Capacity = int.Parse(Console.ReadLine());
                        Console.Write("Input warehouse cost per container: ");
                        Warehouse.CostPerContainer = double.Parse(Console.ReadLine());
                    }
                    else
                    if (OperationInput == "file")
                    {
                        Console.WriteLine("You may change the file 'WarehouseInfo.txt'");
                        Console.WriteLine("At WarehouseInfo: First line: Capacity of warehouse, second line: Cost per container");
                        Console.WriteLine("Example of file are in the folder 'FileInput', plz, change the FILE IN THE FOLDER and press enter.");
                        Console.ReadKey(true);
                        string[] WarehouseInfo = File.ReadAllLines($"FileInput{Path.DirectorySeparatorChar}WarehouseInfo.txt");
                        Warehouse.Capacity = int.Parse(WarehouseInfo[0]);
                        Warehouse.CostPerContainer = double.Parse(WarehouseInfo[1]);
                    }
                }
                catch
                {
                    Console.WriteLine("Incorrect Input!!!, press enter to try again..");
                    Console.ReadKey(true);
                }
            } while (Warehouse.Capacity == 0);

        }
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            bool ExitCode = false;
            InputWarehouseInfo();
            Menu.InitailizeMenu();

            do
            {
                Console.Clear();
                try
                {
                    string ShowCase;
                    if (Menu.OutMenuChoosenOne == 0 && Warehouse.ContainerList.Count != 0)
                        ShowCase = Warehouse.ContainerList[Menu.InMenuChoosenOne].ShowcaseMessage();
                    else
                        ShowCase = "";
                    Menu.PrintOutUpAndDown(ShowCase, ErrorMessage);
                    ErrorMessage = "";
                    Menu.MenuControl(out ExitCode);
                }
                catch
                {
                    ErrorMessage = "Something went wrong, plz try again...";
                }
            } while (!ExitCode);

        }
    }
}
