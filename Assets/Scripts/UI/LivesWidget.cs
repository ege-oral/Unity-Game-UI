using TMPro;
using UnityEngine;

namespace UI
{
    public class LivesWidget : CurrencyWidget
    {
        private const string FullLabel = "Full";
        private const int MaxLives = 5;

        [SerializeField] private TMP_Text remainingLivesText;

        public override void Setup(int count, bool showPlus)
        {
            base.Setup(count, showPlus);
            remainingLivesText.text = count >= MaxLives ? FullLabel : "15:00"; // placeholder — drive from a regeneration timer service later
        }
    }
}
