using System;
using System.Collections.Generic;
using System.IO;
namespace Vegetable
{
    class Program
    {

        static void AddContainer(string InputLine, ref int index)
        {
            try
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
                        Console.WriteLine("Cant Add");
                        if (container.CurrentMass == container.MaxMass)
                            return;
                        else
                        {
                            container.BoxList.RemoveAt(container.BoxList.Count - 1);
                        }
                    }

                }
                index += 1;
                container.Index = index;
                if (Warehouse.Capacity == Warehouse.ContainerList.Count)
                    Warehouse.ContainerList.RemoveAt(0);
                if (Warehouse.CheckCost(container))
                    Warehouse.ContainerList.Add(container);
            }
            catch
            {
                Console.WriteLine("Incorrect input!!");
            }
        }

        static void DeleteContainer(int delIndex)
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
                int DelIndex = int.Parse(Console.ReadLine());
                DeleteContainer(DelIndex);
            }
            else
                throw new Exception();
        }
        static void FileAddDelete()
        {
            try
            {

                string[] WarehouseInfo = File.ReadAllLines(@"FileInput\WarehouseInfo.txt");
                string[] ContainerInfo = File.ReadAllLines(@"FileInput\ContainerInfo.txt");
                string[] OperationInfo = File.ReadAllLines(@"FileInput\OperationInfo.txt");

                if (WarehouseInfo.Length == 2 && ContainerInfo.Length == OperationInfo.Length)
                {
                    Warehouse.Capacity = int.Parse(WarehouseInfo[0]);
                    Warehouse.CostPerContainer = double.Parse(WarehouseInfo[1]);
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
                    PrintWarehouseInfo(Warehouse.ContainerList);
                }
                else
                    throw new Exception();
            }
            catch
            {
                Console.WriteLine("Incorrect input!");
            }
        }
        static void InputWarehouseInfo()
        {
            Console.Write("Input warhouse capacity: ");
            Warehouse.Capacity = int.Parse(Console.ReadLine());
            Console.Write("Input warhouse cost per container: ");
            Warehouse.CostPerContainer = double.Parse(Console.ReadLine());
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
                        FileAddDelete();
                    }
                    else
                    if (OperationInput == "console")
                    {
                        if (Warehouse.Capacity != 0)
                            InputWarehouseInfo();
                        do
                        {
                            ConsoleAddDelete();
                            Console.WriteLine("Press Enter to continue, press ESC to leave...");
                        } while (Console.ReadKey().Key != ConsoleKey.Escape);
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
