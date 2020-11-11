using System;
using System.Collections.Generic;
using System.IO;
namespace Vegetable
{
    class Program
    {

        static void AddContainer(string InputLine, ref int index)
        {
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
                        return;
                    else
                    {
                        container.BoxList.RemoveAt(container.BoxList.Count - 1);
                        return;
                    }
                }
            }
            index += 1;
            container.Index = index;
            if (Warehouse.CheckCost(container))
                Warehouse.ContainerList.Add(container);
        }

        static void DeleteContainer()
        {

        }

        static void PrintWarehouseInfo(List<Container> containers)
        {
            Console.WriteLine($"Warehouse capacity: {Warehouse.Capacity}");
            Console.WriteLine($"Cost per container: {Warehouse.CostPerContainer}");
            Console.WriteLine($"Amount of container: {Warehouse.ContainerList.Count}");
            for (int i = 0; i < containers.Count; i++)
            {
                Console.WriteLine(containers[i]);
            }
        }
        static int ContainerIndex = 0;
        static void ConsoleAddDelete()
        {

            Console.Write("Choose operation: ");
            string Operation = Console.ReadLine();
            if (Operation == "+")
            {
                Console.Write("Input container info: ");
                AddContainer(Console.ReadLine(), ref ContainerIndex);
                PrintWarehouseInfo(Warehouse.ContainerList);
            }
            else if (Operation == "-")
            {

            }
        }
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            do
            {
                try
                {
                    Console.Write("Input method: ");
                    string OperationInput = Console.ReadLine();
                    if (OperationInput == "file")
                    {
                        string[] WarehouseInfo = File.ReadAllLines("WarehouseInfo.txt");
                        string[] ContainerInfo = File.ReadAllLines("ContainerInfo.txt");
                        string[] OperationInfo = File.ReadAllLines("OperationInfo.txt");
                        if (WarehouseInfo.Length == 2)
                        {
                            Warehouse.Capacity = int.Parse(WarehouseInfo[0]);
                            Warehouse.CostPerContainer = double.Parse(WarehouseInfo[1]);
                        }
                        else
                            throw new Exception();
                        if (ContainerInfo.Length == OperationInfo.Length)
                        {
                        }
                        else
                            throw new Exception();
                    }
                    else
                    if (OperationInput == "console")
                    {
                        Console.Write("Input warhouse capacity: ");
                        Warehouse.Capacity = int.Parse(Console.ReadLine());
                        Console.Write("Input warhouse cost per container: ");
                        Warehouse.CostPerContainer = double.Parse(Console.ReadLine());
                        do
                        {
                            ConsoleAddDelete();
                            Console.WriteLine("Press Enter to continue, press ESC to leave...");
                        } while (Console.ReadKey().Key != ConsoleKey.Escape);
                    }
                    else
                    {
                    }
                }
                catch
                {
                    Console.WriteLine("Incorrect input!!!");
                }
            } while (true);

        }
    }
}
