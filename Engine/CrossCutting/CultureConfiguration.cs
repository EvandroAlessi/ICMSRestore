using System.Globalization;
using System.Threading;

namespace CrossCutting
{
    public class CultureConfiguration
    {
        public static void ConfigureCulture()
        {
            var culture = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            if (culture.NumberFormat.NumberDecimalSeparator != ".")
            {
                culture.NumberFormat.NumberDecimalSeparator = ".";
                culture.NumberFormat.CurrencyDecimalSeparator = ".";
                culture.NumberFormat.PercentDecimalSeparator = ".";
                CultureInfo.DefaultThreadCurrentCulture = culture;
                CultureInfo.DefaultThreadCurrentUICulture = culture;

                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;
            }
        }
    }
}
