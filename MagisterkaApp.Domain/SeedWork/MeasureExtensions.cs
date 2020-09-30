using MagisterkaApp.Domain.Enums;

namespace MagisterkaApp.Domain.SeedWork
{
    public static class MeasureExtensions
    {
        public static NumberOfPoints GetNumberOfPoints(TypeOfGTEM typeOfGTEM)
        {
            switch (typeOfGTEM)
            {
                case TypeOfGTEM.None:
                    return NumberOfPoints.None;

                case TypeOfGTEM.GTEM_0_5:
                    return NumberOfPoints.Five;

                case TypeOfGTEM.GTEM_1_735:
                    return NumberOfPoints.Six;

                default:
                    return NumberOfPoints.None;
            }
        }
    }
}
