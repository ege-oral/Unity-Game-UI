using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UI.Settings;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class BottomBarView : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private BottomBarButton[] buttons;
        [SerializeField] private RectTransform highlightBg;
        [SerializeField] private RectTransform buttonsContainer;
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private CanvasGroup canvasGroup;

        [Header("Settings")]
        [SerializeField] private BottomBarViewSettings settings;

        public event Action<BottomBarTab> ContentActivated;
        public event Action Closed;

        private BottomBarButton _currentButton;
        private Sequence _transitionSequence;
        private float _baseAnchorY;

        private void Awake()
        {
            _baseAnchorY = rectTransform.anchoredPosition.y;

            foreach (var btn in buttons)
                btn.OnPressed += HandleButtonPressed;

            highlightBg.gameObject.SetActive(false);

            var offPos = rectTransform.anchoredPosition;
            offPos.y = _baseAnchorY - settings.slideOffset;
            rectTransform.anchoredPosition = offPos;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }

        private void OnDestroy()
        {
            foreach (var btn in buttons)
                btn.OnPressed -= HandleButtonPressed;

            _transitionSequence?.Kill();
        }

        private void LateUpdate()
        {
            if (_currentButton == null) return;

            var targetX = GetTargetX();
            var pos = highlightBg.anchoredPosition;
            pos.x = Mathf.MoveTowards(pos.x, targetX, Time.deltaTime * settings.followSpeed);
            highlightBg.anchoredPosition = pos;
        }

        public async UniTask Show()
        {
            _transitionSequence?.Kill();

            gameObject.SetActive(true);

            var startPos = rectTransform.anchoredPosition;
            startPos.y = _baseAnchorY - settings.slideOffset;
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
                .Append(rectTransform.DOAnchorPosY(_baseAnchorY - settings.slideOffset, settings.hideDuration).SetEase(settings.hideEase))
                .SetUpdate(true);

            await _transitionSequence.AsyncWaitForCompletion();

            gameObject.SetActive(false);
        }

        private void HandleButtonPressed(BottomBarButton pressed)
        {
            if (_currentButton == pressed)
            {
                DeactivateCurrent();
                return;
            }

            ActivateButton(pressed);
        }

        private void ActivateButton(BottomBarButton button)
        {
            foreach (var btn in buttons)
            {
                if (btn == button) continue;
                btn.Deselect(true);
            }

            _currentButton = button;
            _currentButton.Select(true);

            if (!highlightBg.gameObject.activeSelf)
            {
                highlightBg.gameObject.SetActive(true);
                SnapHighlight();
            }

            ContentActivated?.Invoke(button.Tab);
        }

        private void DeactivateCurrent()
        {
            _currentButton.Deselect(true);
            _currentButton = null;
            highlightBg.gameObject.SetActive(false);

            Closed?.Invoke();
        }

        private void SnapHighlight()
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(buttonsContainer);

            var pos = highlightBg.anchoredPosition;
            pos.x = GetTargetX();
            highlightBg.anchoredPosition = pos;
        }

        private float GetTargetX() => _currentButton.transform.localPosition.x;
    }
}
