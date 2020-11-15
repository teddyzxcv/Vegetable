using System;
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
                    Warehouse.ContainerList.RemoveAt(0);
                    ContainerExcluded++;
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

                string[] ContainerInfo = File.ReadAllLines(@"FileInput\ContainerInfo.txt");
                string[] OperationInfo = File.ReadAllLines(@"FileInput\OperationInfo.txt");
                string[] WarehouseInfo = File.ReadAllLines(@"FileInput\WarehouseInfo.txt");
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
                    ErrorMessage = $"{ContainerExcluded} have been excluded due the lack of space in warehouse.";

            }
            catch
            {
                ErrorMessage = "Incorrect file input!!!";
            }
        }
        static void InputWarehouseInfo()
        {
            Console.Write("Input method(file or console): ");
            string OperationInput = Console.ReadLine();
            if (OperationInput == "console")
            {
                Console.Write("Input warhouse capacity: ");
                Warehouse.Capacity = int.Parse(Console.ReadLine());
                Console.Write("Input warhouse cost per container: ");
                Warehouse.CostPerContainer = double.Parse(Console.ReadLine());
            }
            else
            if (OperationInput == "file")
            {
                string[] WarehouseInfo = File.ReadAllLines(@"FileInput\WarehouseInfo.txt");
                Warehouse.Capacity = int.Parse(WarehouseInfo[0]);
                Warehouse.CostPerContainer = double.Parse(WarehouseInfo[1]);
                FileAddDelete();
            }

        }
        static void Main(string[] args)
        {
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
