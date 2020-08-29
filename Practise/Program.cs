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
