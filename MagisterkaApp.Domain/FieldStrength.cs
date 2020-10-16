namespace MagisterkaApp.Domain
{
    public abstract class FieldStrength
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
