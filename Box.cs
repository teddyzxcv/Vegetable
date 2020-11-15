using System;
using System.Collections.Generic;
namespace Vegetable
{
    public class Box
    {
        public double Mass;
        public double CostPerKilo;

        public double SumCost;

        public Box(double mass, double costperkilo)
        {
            this.Mass = mass;
            this.CostPerKilo = costperkilo;
            this.SumCost = mass * costperkilo;
        }
        public override string ToString()
        {
            string Output = $"Mass: {this.Mass}Rub; Cost per kilogram: {this.CostPerKilo}Rub; Sum cost: {this.SumCost}Rub\n";
            return Output;
        }

    }
}