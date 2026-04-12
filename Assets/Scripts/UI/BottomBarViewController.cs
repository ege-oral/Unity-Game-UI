using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class BottomBarViewController : MonoBehaviour
    {
        [SerializeField] private TabButton[] tabButtons;
        [SerializeField] private RectTransform highlightBg;
        [SerializeField] private RectTransform buttonsContainer;
        [SerializeField] private float followSpeed = 15f;

        private TabButton _currentTab;
        private const int DefaultTabIndex = 2;

        private void Awake()
        {
            foreach (var tab in tabButtons)
            {
                tab.OnTabPressed += HandleTabPressed;
            }
        }

        private void Start()
        {
            if (tabButtons.Length <= 0 || DefaultTabIndex >= tabButtons.Length) return;

            SelectTab(tabButtons[DefaultTabIndex]);
            SnapHighlight();
        }

        private void LateUpdate()
        {
            if (_currentTab == null) return;

            var targetX = GetTargetX();
            var pos = highlightBg.anchoredPosition;
            pos.x = Mathf.MoveTowards(pos.x, targetX, Time.deltaTime * followSpeed);
            highlightBg.anchoredPosition = pos;
        }

        private void OnDestroy()
        {
            foreach (var tab in tabButtons)
            {
                tab.OnTabPressed -= HandleTabPressed;
            }
        }

        private void HandleTabPressed(TabButton pressedTab)
        {
            if (_currentTab == pressedTab) return;

            SelectTab(pressedTab, true);
        }

        private void SelectTab(TabButton tab, bool animated = false)
        {
            foreach (var tabButton in tabButtons)
                tabButton.Deselect(animated);

            _currentTab = tab;
            _currentTab.Select(animated);

            highlightBg.gameObject.SetActive(true);
        }

        private void SnapHighlight()
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(buttonsContainer);

            var pos = highlightBg.anchoredPosition;
            pos.x = GetTargetX();
            highlightBg.anchoredPosition = pos;
        }

        private float GetTargetX()
        {
            return _currentTab.transform.localPosition.x;
        }
    }
}
