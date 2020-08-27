using System;

namespace Practise
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(DateTime.Now);

            string word = "wzduz";

            var t = word.Contains("wzd");

            Console.WriteLine(t);
        }
    }
}
