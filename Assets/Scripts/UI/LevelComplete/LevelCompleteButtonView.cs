using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UI.Configs;
using UnityEngine;
using UnityEngine.UI;

namespace UI.LevelComplete
{
    public class LevelCompleteButtonView : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private RectTransform scaleContainer;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Button button;

        [Header("Settings")]
        [SerializeField] private LevelCompleteButtonViewSettings settings;

        [Header("Sample Data")]
        [SerializeField] private int sampleScore = 12345;
        [SerializeField, Range(0, 3)] private int sampleStars = 3;
        [SerializeField] private int sampleCoins = 100;
        [SerializeField] private int sampleCrowns = 8;

        public event Action Clicked;

        public int SampleScore => sampleScore;
        public int SampleStars => sampleStars;
        public int SampleCoins => sampleCoins;
        public int SampleCrowns => sampleCrowns;

        private Sequence _transitionSequence;

        private void Awake()
        {
            scaleContainer.localScale = Vector3.zero;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }

        private void OnEnable()
        {
            button.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            button.onClick.RemoveListener(OnClick);
        }

        private void OnDestroy()
        {
            _transitionSequence?.Kill();
        }

        private void OnClick()
        {
            Clicked?.Invoke();
        }

        public async UniTask Show()
        {
            _transitionSequence?.Kill();

            gameObject.SetActive(true);

            scaleContainer.localScale = Vector3.zero;
            canvasGroup.alpha = 1f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;

            _transitionSequence = DOTween.Sequence()
                .AppendInterval(settings.showDelay)
                .Append(scaleContainer.DOScale(1f, settings.showDuration).SetEase(settings.showEase))
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
                .Append(scaleContainer.DOScale(0f, settings.hideDuration).SetEase(settings.hideEase))
                .SetUpdate(true);

            await _transitionSequence.AsyncWaitForCompletion();

            gameObject.SetActive(false);
        }
    }
}
