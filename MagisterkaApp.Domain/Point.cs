using MagisterkaApp.Domain.Enums;

namespace MagisterkaApp.Domain
{
    public class Point
    {
        public int Id { get; }
        public FieldStrength Primary { get; }
        public FieldStrength SecondaryOne { get; }        
        public FieldStrength SecondaryTwo { get; }
        public PowerLevel PowerLevel { get; set; }
        public BackgroundColor PointBackgroundColor { get; set; }
        public bool IsConsidered { get; set; }
        public TEMdominant IsTEMdominant { get; set; }


        public Point(int id, double primaryInput, double secondaryOneInput, double secondaryTwoInput,
                     string primaryName, string secondaryOneName, string secondaryTwoName)
        {
            this.Id = id;
            this.Primary = new FieldStrength(primaryInput, primaryName); 
            this.SecondaryOne = new FieldStrength(secondaryOneInput, secondaryOneName); 
            this.SecondaryTwo = new FieldStrength(secondaryTwoInput, secondaryTwoName); 
        }

        public void AddPowerLevel(double powerLevel)
        {
            this.PowerLevel = new PowerLevel(powerLevel);
        }
    }
}
