using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class BottomBarView : MonoBehaviour
    {
        [SerializeField] private BottomBarButton[] buttons;
        [SerializeField] private RectTransform highlightBg;
        [SerializeField] private RectTransform buttonsContainer;
        [SerializeField] private float followSpeed = 15f;

        public event Action<BottomBarTab> ContentActivated;
        public event Action Closed;

        private BottomBarButton _currentButton;

        private void Awake()
        {
            foreach (var btn in buttons)
                btn.OnPressed += HandleButtonPressed;

            highlightBg.gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            foreach (var btn in buttons)
                btn.OnPressed -= HandleButtonPressed;
        }

        private void LateUpdate()
        {
            if (_currentButton == null) return;

            var targetX = GetTargetX();
            var pos = highlightBg.anchoredPosition;
            pos.x = Mathf.MoveTowards(pos.x, targetX, Time.deltaTime * followSpeed);
            highlightBg.anchoredPosition = pos;
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
