namespace MagisterkaApp.Domain
{
    public class PrimaryFieldStrength: FieldStrength
    {
        //created this class, cause you can't read three objects with type of FieldStrength in LiteDb, so i created 3 objects inheritance type FS
        public PrimaryFieldStrength(double input, string name) : base(input, name) { }
    }
}
