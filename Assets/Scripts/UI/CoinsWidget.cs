using System.Globalization;

namespace UI
{
    public class CoinsWidget : CurrencyWidget
    {
        protected override string FormatCount(int count)
            => count.ToString("N0", CultureInfo.InvariantCulture);
    }
}
