using MagisterkaApp.Domain.Enums;

namespace MagisterkaApp.Domain
{
    public class Point
    {
        public int Id { get; }
        public double PrimaryEy { get; }
        public double SecondaryEx { get; }        
        public double SecondaryEz { get; }
        public double PowerLevel { get; set; }
        public bool IsConsidered { get; set; }
        public TEMdominant IsTEMdominant { get; set; }


        public Point(int id, double primaryEy, double secondaryEx, double secondaryEz)
        {
            this.Id = id;
            this.PrimaryEy = primaryEy; 
            this.SecondaryEx = secondaryEx; 
            this.SecondaryEz = secondaryEz; 
        }

        public void AddPowerResult(double powerLevel)
        {
            this.PowerLevel = powerLevel;
        }
    }
}
