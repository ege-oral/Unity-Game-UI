using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(RectTransform))]
    public class AnimatedButton : Button
    {
        [Header("Animation Settings")]
        [SerializeField] [Range(0.1f, 1.0f)] private float scaleDownValue = 0.8f;
        [SerializeField] private float animationDuration = 0.1f;

        [Header("Cooldown")]
        [SerializeField] private float clickCooldown = 0.3f;

        private bool _isPressed;
        private float _lastClickTime;
        private Vector3 _initialScale;
        private Tweener _scaleTween;

        protected override void Awake()
        {
            base.Awake();
            _initialScale = transform.localScale;
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            bool isInCooldown = Time.unscaledTime - _lastClickTime < clickCooldown;
            if (!IsInteractable() || isInCooldown) return;

            base.OnPointerDown(eventData);
            _isPressed = true;
            ScaleDown();
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);

            if (!_isPressed) return;

            _isPressed = false;
            ScaleUp();
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            bool isInCooldown = Time.unscaledTime - _lastClickTime < clickCooldown;
            if (isInCooldown) return;

            base.OnPointerClick(eventData);
            _lastClickTime = Time.unscaledTime;
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);

            if (_isPressed)
            {
                _isPressed = false;
                ScaleUp();
            }
        }

        protected void CancelPress()
        {
            if (_isPressed)
            {
                _isPressed = false;
                ScaleUp();
            }
        }

        protected void StartPress()
        {
            _isPressed = true;
            ScaleDown();
        }

        private void ScaleDown()
        {
            _scaleTween?.Kill();
            _scaleTween = transform.DOScale(_initialScale * scaleDownValue, animationDuration)
                .SetUpdate(true)
                .SetEase(Ease.OutQuad);
        }

        private void ScaleUp()
        {
            _scaleTween?.Kill();
            _scaleTween = transform.DOScale(_initialScale, animationDuration)
                .SetUpdate(true)
                .SetEase(Ease.OutQuad);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _scaleTween?.Kill();
        }
    }
}
