using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Popup
{
    public class LevelCompletePopup : BasePopup
    {
        [Header("Buttons")]
        [SerializeField] private Button closeButton;
        [SerializeField] private Button nextLevelButton;
        [SerializeField] private Button retryButton;

        [Header("Display")]
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private Image[] starImages;

        [Header("Star Animation")]
        [SerializeField] private float starAnimationDelay = 0.2f;
        [SerializeField] private float starAnimationDuration = 0.3f;

        public override string PopupName => PopupKeys.LevelComplete;

        private void OnEnable()
        {
            closeButton.onClick.AddListener(OnCloseButtonPressed);
            nextLevelButton.onClick.AddListener(OnNextLevelButtonPressed);
            retryButton.onClick.AddListener(OnRetryButtonPressed);
        }

        private void OnDisable()
        {
            closeButton.onClick.RemoveListener(OnCloseButtonPressed);
            nextLevelButton.onClick.RemoveListener(OnNextLevelButtonPressed);
            retryButton.onClick.RemoveListener(OnRetryButtonPressed);
        }

        protected override void OnShow(PopupData data)
        {
            if (data is not LevelCompleteData levelData) return;

            scoreText.text = levelData.Score.ToString();
            AnimateStars(levelData.Stars).Forget();
        }

        private async UniTaskVoid AnimateStars(int starCount)
        {
            for (int i = 0; i < starImages.Length; i++)
            {
                starImages[i].transform.localScale = Vector3.zero;
                starImages[i].color = i < starCount ? Color.white : new Color(1f, 1f, 1f, 0.3f);
            }

            for (int i = 0; i < starImages.Length; i++)
            {
                await UniTask.Delay((int)(starAnimationDelay * 1000));
                starImages[i].transform
                    .DOScale(1f, starAnimationDuration)
                    .SetEase(Ease.OutBack)
                    .SetUpdate(true);
            }
        }

        private void OnCloseButtonPressed()
        {
            Close().Forget();
        }

        private void OnNextLevelButtonPressed()
        {
            Close().Forget();
        }

        private void OnRetryButtonPressed()
        {
            Close().Forget();
        }
    }
}
