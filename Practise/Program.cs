using System;
using System.ComponentModel;

namespace Practise
{
    class Program
    {
        static void Main(string[] args)
        {
            var t = MyEnum.Name1.GetDescription();

            Console.WriteLine(t);

            double primary = 5;
            double powerResult = 39.72;
            double correctedPower = 36.1865;

            var powerDifference = 0.1 * (powerResult - correctedPower);
            var powResult = Math.Pow(10, powerDifference);

            Console.WriteLine(primary * Math.Sqrt(powResult)); 
        }
    }
    public enum MyEnum
    {
        [Description("Ololo Name1")]
        Name1,
        [Description("Pizdets")]
        HereIsAnother,
        [Description("Bobo")]
        LastOne
    }
}
