using System.Diagnostics.Metrics;

namespace FPMain
{
    public static class FiveMProxy
    {
        public static Meter Meter = new Meter("FiveMProxy", "1.0.0");
    }
}
