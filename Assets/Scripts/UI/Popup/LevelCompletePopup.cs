using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Popup
{
    public class LevelCompletePopup : BasePopup
    {
        [Header("Buttons")]
        [SerializeField] private Button homeButton;
        [SerializeField] private Button rewardButton;

        [Header("Display")]
        [SerializeField] private TextMeshProUGUI scoreText;

        public override string PopupName => PopupKeys.LevelComplete;

        private void OnEnable()
        {
            homeButton.onClick.AddListener(OnHomeButtonPressed);
            rewardButton.onClick.AddListener(OnRewardButtonPressed);
        }

        private void OnDisable()
        {
            homeButton.onClick.RemoveListener(OnHomeButtonPressed);
            rewardButton.onClick.RemoveListener(OnRewardButtonPressed);
        }

        protected override void OnShow(PopupData data)
        {
            if (data is not LevelCompleteData levelData) return;
            scoreText.text = levelData.Score.ToString();
        }

        private void OnHomeButtonPressed()
        {
            Close().Forget();
        }

        private void OnRewardButtonPressed()
        {
            Close().Forget();
        }
    }
}
