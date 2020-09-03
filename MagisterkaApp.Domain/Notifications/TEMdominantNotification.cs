namespace MagisterkaApp.Domain.Notifications
{
    public static class TEMdominantNotification
    {
        public const string Correct = "TEM dominant jest spełniony < -6 [dB]";
        public const string ConfirmFirstRequirement = "TEM dominant jest spełniony dla 75% < -6 [dB]";
        public const string ConfirmSecondRequirement = "TEM dominant jest spełniony dla 75% -6[dB] < TEM < -2[db]";
        public const string ErrorDominant = "TEM dominant NIE jest spełniony";
    }
}
