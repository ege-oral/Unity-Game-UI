using Cysharp.Threading.Tasks;
using DG.Tweening;
using UI.Settings;
using UnityEngine;

namespace UI
{
    public class TopBarView : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private CanvasGroup canvasGroup;

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
        }

        private void OnDestroy()
        {
            _transitionSequence?.Kill();
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
