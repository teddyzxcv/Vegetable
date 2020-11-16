using System;
using System.Collections.Generic;
namespace Vegetable
{
    public class Container
    {
        /// <summary>
        /// Initialize the container.
        /// </summary>
        /// <param name="damagelevel"></param>
        /// <param name="maxmass"></param>
        /// <param name="index"></param>
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
        /// <summary>
        ///  Find current mass of all box in the container.
        /// </summary>
        /// <value></value>
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

        /// <summary>
        ///  Find the sum of cost of all boxes in the container.
        /// </summary>
        /// <value></value>
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

        /// <summary>
        /// Simple ToString method.
        /// </summary>
        /// <returns></returns>

        public override string ToString()
        {
            string Output = "-------------------------------------------------------\n";
            Output += $"| Container N.{this.Index}:\n| Damage Level: {this.DamageLevel / 100}; Maximum mass: {this.MaxMass}kg; Current mass: {this.CurrentMass}kg \n";
            Output += $"| Sum cost: {this.SumCost}Rub         \nList of boxes:\n";
            for (int i = 0; i < this.BoxList.Count; i++)
            {
                Output += "-> " + this.BoxList[i].ToString();
            }
            return Output;
        }
        /// <summary>
        /// Show case the info about the container.
        /// </summary>
        /// <returns></returns>
        public string ShowcaseMessage()
        {
            return $">>>Number of boxes: {this.BoxList.Count}; Sum cost: {this.SumCost}Rub; Damage Level: {this.DamageLevel / 100};\n>>>Maximum mass: {this.MaxMass}kg; Current mass: {this.CurrentMass}kg";
        }



    }
}