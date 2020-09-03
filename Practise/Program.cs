using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Practise
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] arrayOne = new int[] { 1, 2, 3 };
            int[] arrayTwo = new int[] { 3, 2 };

       

            List<string> result = new List<string>();

            for (int i = 0; i < arrayOne.Length; i++)
            {
                for (int j = 0; j < arrayTwo.Length; j++)
                {
                    if (arrayOne[i] == arrayTwo[j])
                        result.Add(arrayOne[i].ToString());
                    else
                        continue;
                }
            }

            foreach (var number in result)
            {
                Console.WriteLine(number);
            }
           
        }
    }
}
