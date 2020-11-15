using System;
using System.Collections.Generic;
using System.Linq;
namespace Vegetable
{
    public static class Warehouse
    {
        static public double CostPerContainer { get; set; }

        static public int Capacity { get; set; }

        static public List<Container> ContainerList = new List<Container>();
        static public string CotainerToIndex(Container c)
        {
            return "Container N." + c.Index.ToString();
        }


        static public bool CheckCost(Container container)
        {
            if (container.SumCost <= CostPerContainer)
                return false;
            else
                return true;
        }

        static public void SortBy(int Parameter)
        {
            /// Index
            /// Sum mass
            /// Max Mass
            /// Sum cost
            /// Damage Level
            switch (Parameter)
            {
                case (0):
                    ContainerList = ContainerList.OrderBy(o => o.Index).ToList();

                    break;
                case (1):
                    ContainerList = ContainerList.OrderBy(o => o.CurrentMass).ToList();
                    break;
                case (2):
                    ContainerList = ContainerList.OrderBy(o => o.MaxMass).ToList();
                    break;
                case (3):
                    ContainerList = ContainerList.OrderBy(o => o.SumCost).ToList();
                    break;
                case (4):
                    ContainerList = ContainerList.OrderBy(o => o.DamageLevel).ToList();
                    break;
            }
        }
    }
}