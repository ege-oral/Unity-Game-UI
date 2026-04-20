using TMPro;
using UnityEngine;

namespace UI
{
    public class LivesWidget : CurrencyWidget
    {
        private const string FullLabel = "Full";
        private const int MaxLives = 5;

        [SerializeField] private TMP_Text currentLivesText;

        public override void Setup(int count, bool showPlus)
        {
            base.Setup(count, showPlus);
            currentLivesText.text = count.ToString();
        }

        protected override string FormatCount(int count)
            => count >= MaxLives ? FullLabel : count.ToString();
    }
}
