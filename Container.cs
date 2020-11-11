using System;
using System.Collections.Generic;
namespace Vegetable
{
    public class Container
    {

        public Container(double damagelevel, double maxmass, int index)
        {
            this.DamageLevel = damagelevel;
            this.MaxMass = maxmass;
            this.Index = index;
        }
        public int Index;

        public double MaxMass;// 50 1000
        public List<Box> BoxList = new List<Box>();

        private double summass;
        public double CurrentMass
        {
            get
            {
                summass = 0;
                for (int i = 0; i < this.BoxList.Count; i++)
                {
                    summass += this.BoxList[i].Mass;
                }
                return summass;
            }

        }

        public double DamageLevel;

        private double sumcost;

        public double SumCost
        {
            get
            {
                sumcost = 0;
                for (int i = 0; i < this.BoxList.Count; i++)
                {
                    sumcost += this.BoxList[i].SumCost;
                }
                sumcost = sumcost / 100 * (100 - DamageLevel);
                return sumcost;
            }
        }



        public override string ToString()
        {
            string Output = "-------------------------------------------------------\n";
            Output += $"| Container index[{this.Index}]:\n| Damage Level:{this.DamageLevel / 100}; Maximum mass:{this.MaxMass}; Current mass:{this.CurrentMass} \n";
            Output += $"| Sum cost:{this.SumCost}         \nList of boxes:\n";
            for (int i = 0; i < this.BoxList.Count; i++)
            {
                Output += "-> " + this.BoxList[i].ToString();
            }
            return Output;
        }



    }
}