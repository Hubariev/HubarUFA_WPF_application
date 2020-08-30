using System;
using System.Collections.Generic;
using System.Text;

namespace MagisterkaApp.Domain
{
    public class FieldStrength
    {
        public string Name { get; set; }
        public double Input { get; set; }
        public double Corrected { get; set; }
        public double Test { get; set; }

        public FieldStrength(double input, string name)
        {
            this.Name = name;
            this.Input = input;
        }
    }
}
