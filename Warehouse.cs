using System;
using System.Collections.Generic;
namespace Vegetable
{
    public static class Warehouse
    {
        static public double CostPerContainer { get; set; }

        static public int Capacity { get; set; }

        static public List<Container> ContainerList = new List<Container>();


        static public bool CheckCost(Container container)
        {
            if (container.SumCost <= CostPerContainer)
                return false;
            else
                return true;
        }
    }
}