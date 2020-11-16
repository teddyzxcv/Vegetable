using System;
using System.Collections.Generic;
namespace Vegetable
{
    public class Box
    {
        public double Mass;
        public double CostPerKilo;

        public double SumCost;

        /// <summary>
        /// Initialize the box.
        /// </summary>
        /// <param name="mass"></param>
        /// <param name="costperkilo"></param>
        public Box(double mass, double costperkilo)
        {
            if (mass > 0 && costperkilo > 0)
            {
                this.Mass = mass;
                this.CostPerKilo = costperkilo;
                this.
                SumCost = mass * costperkilo;
            }
            else
                throw new Exception();

        }
        /// <summary>
        /// Simple box ToString method to make program better.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string Output = $"Mass: {this.Mass}Rub; Cost per kilogram: {this.CostPerKilo}Rub; Sum cost: {this.SumCost}Rub\n";
            return Output;
        }

    }
}