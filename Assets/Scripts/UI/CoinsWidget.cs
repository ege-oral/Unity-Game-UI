using System.Globalization;
using UnityEngine;

namespace UI
{
    public class CoinsWidget : CurrencyWidget
    {
        [SerializeField] private GameObject plusIcon;

        public override void Setup(int count)
        {
            base.Setup(count);
            plusIcon.SetActive(true);
        }

        protected override string FormatCount(int count)
            => count.ToString("N0", CultureInfo.InvariantCulture);
    }
}
