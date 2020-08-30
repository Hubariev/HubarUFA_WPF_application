using System;
using System.Collections.Generic;
using System.Text;

namespace MagisterkaApp.Domain
{
    public class PowerLevel
    {
        public double Input { get;}
        public double Corrected { get; set; }
        public double Test { get; set; }

        public PowerLevel(double input)
        {
            this.Input = input;
        }

        public void AddCorrectedPower(double correctedPower)
        {
            this.Corrected = correctedPower;
        }
    }

}
