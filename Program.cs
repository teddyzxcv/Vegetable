using System;
using System.Collections.Generic;
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
            for (int i = 0; i < SumNumber; i++)
            {
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
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            try
            {
                Console.Write("Input warhouse capacity: ");
                Warehouse.Capacity = int.Parse(Console.ReadLine());
                Console.Write("Input warhouse cost per container: ");
                Warehouse.CostPerContainer = double.Parse(Console.ReadLine());
                do
                {
                    Console.Write("Input which operation: ");
                    string Operation = Console.ReadLine();
                    if (Operation == "1")
                    {
                        Console.Write("Input container info: ");
                        AddContainer(Console.ReadLine(), ref ContainerIndex);
                        PrintWarehouseInfo(Warehouse.ContainerList);
                    }
                    else
                    {
                    }
                } while (true);
            }
            catch
            {
                Console.WriteLine("Incorrect input!!!");
            }
        }
    }
}
