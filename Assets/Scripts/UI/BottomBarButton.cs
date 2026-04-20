using System;
using DG.Tweening;
using TMPro;
using UI.Settings;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class BottomBarButton : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private RectTransform containerTransform;
        [SerializeField] private RectTransform iconTransform;
        [SerializeField] private TextMeshProUGUI label;
        [SerializeField] private Button button;

        [Header("Settings")]
        [SerializeField] private BottomBarTab tab;
        [SerializeField] private BottomBarButtonSettings settings;

        public event Action<BottomBarButton> OnPressed;
        public BottomBarTab Tab => tab;
        public bool IsLocked => tab == BottomBarTab.Locked;

        private bool _isSelected;
        private Sequence _sequence;
        private Tween _lockedShake;

        private void Awake()
        {
            button.onClick.AddListener(HandlePress);
        }

        private void OnDestroy()
        {
            button.onClick.RemoveListener(HandlePress);
            _sequence?.Kill();
            _lockedShake?.Kill();
        }

        private void HandlePress()
        {
            if (IsLocked)
            {
                PlayLockedShake();
                return;
            }

            OnPressed?.Invoke(this);
        }

        public void Select(bool animated = false)
        {
            if (_isSelected || IsLocked) return;
            SetSelected(true, animated);
        }

        public void Deselect(bool animated = false)
        {
            if (!_isSelected) return;
            SetSelected(false, animated);
        }

        private void SetSelected(bool selected, bool animated)
        {
            _isSelected = selected;
            _sequence?.Kill();

            if (animated)
                AnimateVisualState(selected);
            else
                ApplyVisualState(selected);
        }

        private void ApplyVisualState(bool selected)
        {
            var targetWidth = selected ? settings.selectedWidth : settings.normalWidth;
            var size = containerTransform.sizeDelta;
            size.x = targetWidth;
            containerTransform.sizeDelta = size;

            var iconPos = iconTransform.anchoredPosition;
            iconPos.y = selected ? settings.iconSelectedY : settings.iconNormalY;
            iconTransform.anchoredPosition = iconPos;

            label.gameObject.SetActive(selected);
        }

        private void AnimateVisualState(bool selected)
        {
            var targetWidth = selected ? settings.selectedWidth : settings.normalWidth;
            var targetY = selected ? settings.iconSelectedY : settings.iconNormalY;
            var duration = settings.animationDuration;
            var ease = settings.animationEase;

            label.gameObject.SetActive(selected);

            _sequence = DOTween.Sequence();

            _sequence.Join(
                DOTween.To(() => containerTransform.sizeDelta.x,
                    x => containerTransform.sizeDelta = new Vector2(x, containerTransform.sizeDelta.y),
                    targetWidth, duration).SetEase(ease));

            _sequence.Join(
                iconTransform.DOAnchorPosY(targetY, duration).SetEase(ease));

            _sequence.SetUpdate(true);
        }

        private void PlayLockedShake()
        {
            _lockedShake?.Kill();
            iconTransform.localEulerAngles = Vector3.zero;

            var duration = settings.lockedShakeDuration;
            var rot = settings.lockedShakeRotation;
            var inDur = duration * settings.lockedRotateInPortion;
            var outDur = duration * settings.lockedRotateOutPortion;
            var swingDur = Mathf.Max(0f, duration - inDur - outDur);

            _lockedShake = DOTween.Sequence()
                .Join(iconTransform.DOShakeAnchorPos(
                    duration,
                    new Vector2(settings.lockedShakeStrength, 0f),
                    settings.lockedShakeVibrato,
                    0f))
                .Join(DOTween.Sequence()
                    .Append(iconTransform.DOLocalRotate(new Vector3(0f, 0f, rot), inDur).SetEase(Ease.OutQuad))
                    .Append(iconTransform.DOLocalRotate(new Vector3(0f, 0f, -rot), swingDur).SetEase(Ease.InOutQuad))
                    .Append(iconTransform.DOLocalRotate(Vector3.zero, outDur).SetEase(Ease.InQuad)))
                .SetUpdate(true);
        }
    }
}
