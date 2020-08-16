using MagisterkaApp.Domain.Enums;

namespace MagisterkaApp.Domain
{
    public class Point
    {
        public int Id { get; }
        public double PrimaryEy { get; }
        public double SecondaryEx { get; }        
        public double SecondaryEz { get; }
        public double PowerLevel { get; }
        public bool IsConsidered { get; set; }
        public bool IsTEMdominant { get; set; }


        public Point(int id, double primaryEy, double secondaryEx, double secondaryEz, double powerLevel)
        {
            this.Id = id;
            this.PrimaryEy = primaryEy; 
            this.SecondaryEx = secondaryEx; 
            this.SecondaryEz = secondaryEz; 
            this.PowerLevel = powerLevel; 
        }
    }
}
