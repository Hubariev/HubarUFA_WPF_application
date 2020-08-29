using MagisterkaApp.Domain.Notifications;
using System.ComponentModel;

namespace MagisterkaApp.Domain.Enums
{
    public enum TEMdominant
    {
        [Description("Smaller then -6[dB]")]
        TEMdominantSmaller_Minus6dB,
        [Description("Between -6[dB] and -2[dB]")]
        TEMdominantSmaller_Minus2dB,
        [Description("Higher then -2[dB]")]
        TEMdominantHigher_Minus2dB,
    }
}
