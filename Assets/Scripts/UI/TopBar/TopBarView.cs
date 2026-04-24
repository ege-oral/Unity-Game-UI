using Cysharp.Threading.Tasks;
using DG.Tweening;
using UI.Configs;
using UI.Widgets;
using UnityEngine;
using Logger = Core.Logger;

namespace UI.TopBar
{
    public class TopBarView : MonoBehaviour
    {
        private const string LogTag = "TopBar";

        [Header("References")]
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private CanvasGroup canvasGroup;

        [Header("Widgets")]
        [SerializeField] private CoinsWidget coins;
        [SerializeField] private LivesWidget lives;
        [SerializeField] private CurrencyWidget stars;

        [Header("Settings")]
        [SerializeField] private TopBarViewSettings settings;

        private Sequence _transitionSequence;
        private float _baseAnchorY;

        private void Awake()
        {
            _baseAnchorY = rectTransform.anchoredPosition.y;

            var offPos = rectTransform.anchoredPosition;
            offPos.y = _baseAnchorY + settings.slideOffset;
            rectTransform.anchoredPosition = offPos;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;

            coins.Setup(2850);
            lives.Setup(5);
            stars.Setup(165);

            coins.ButtonPressed += OnCoinsButtonPressed;
            lives.ButtonPressed += OnLivesButtonPressed;
            stars.ButtonPressed += OnStarsButtonPressed;
        }

        private void OnDestroy()
        {
            coins.ButtonPressed -= OnCoinsButtonPressed;
            lives.ButtonPressed -= OnLivesButtonPressed;
            stars.ButtonPressed -= OnStarsButtonPressed;
            _transitionSequence?.Kill();
        }

        private void OnCoinsButtonPressed()
        {
            Logger.Log(LogTag, "Coins + pressed");
        }

        private void OnLivesButtonPressed()
        {
            Logger.Log(LogTag, "Lives + pressed");
        }

        private void OnStarsButtonPressed()
        {
            Logger.Log(LogTag, "Stars + pressed");
        }

        public async UniTask Show()
        {
            _transitionSequence?.Kill();

            gameObject.SetActive(true);

            var startPos = rectTransform.anchoredPosition;
            startPos.y = _baseAnchorY + settings.slideOffset;
            rectTransform.anchoredPosition = startPos;
            canvasGroup.alpha = 1f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;

            _transitionSequence = DOTween.Sequence()
                .AppendInterval(settings.showDelay)
                .Append(rectTransform.DOAnchorPosY(_baseAnchorY, settings.showDuration).SetEase(settings.showEase))
                .SetUpdate(true);

            await _transitionSequence.AsyncWaitForCompletion();

            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }

        public async UniTask Hide()
        {
            _transitionSequence?.Kill();

            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;

            _transitionSequence = DOTween.Sequence()
                .Append(rectTransform.DOAnchorPosY(_baseAnchorY + settings.slideOffset, settings.hideDuration).SetEase(settings.hideEase))
                .SetUpdate(true);

            await _transitionSequence.AsyncWaitForCompletion();

            gameObject.SetActive(false);
        }
    }
}
