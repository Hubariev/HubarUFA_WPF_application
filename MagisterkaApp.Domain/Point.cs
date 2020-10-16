using MagisterkaApp.Domain.Enums;

namespace MagisterkaApp.Domain
{
    public class Point
    {
        public int Id { get; }
        public PrimaryFieldStrength Primary { get; set; }
        public SecondaryOneFieldStrength SecondaryOne { get; set; }        
        public SecondaryTwoFieldStrength SecondaryTwo { get; set; }
        public PowerLevel PowerLevel { get; set; }
        public BackgroundColor PointBackgroundColor { get; set; }
        public bool IsConsidered { get; set; }
        public TEMdominant IsTEMdominant { get; set; }


        public Point(int id, double primaryInput, double secondaryOneInput, double secondaryTwoInput,
                     string primaryName, string secondaryOneName, string secondaryTwoName)
        {
            this.Id = id;
            this.Primary = new PrimaryFieldStrength(primaryInput, primaryName); 
            this.SecondaryOne = new SecondaryOneFieldStrength(secondaryOneInput, secondaryOneName); 
            this.SecondaryTwo = new SecondaryTwoFieldStrength(secondaryTwoInput, secondaryTwoName); 
        }

        public void AddPowerLevel(double powerLevel)
        {
            this.PowerLevel = new PowerLevel(powerLevel);
        }
    }
}
