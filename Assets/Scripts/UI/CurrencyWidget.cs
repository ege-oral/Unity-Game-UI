using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CurrencyWidget : MonoBehaviour
    {
        [SerializeField] private Image currencyIcon;
        [SerializeField] protected TextMeshProUGUI countText;
        [SerializeField] private Button plusButton;

        public event Action PlusPressed;

        private void Awake()
        {
            plusButton.onClick.AddListener(HandlePlus);
        }

        private void OnDestroy()
        {
            plusButton.onClick.RemoveListener(HandlePlus);
        }

        public virtual void Setup(int count, bool showPlus)
        {
            countText.text = FormatCount(count);
            plusButton.gameObject.SetActive(showPlus);
        }

        protected virtual string FormatCount(int count) => count.ToString();

        private void HandlePlus()
        {
            PlusPressed?.Invoke();
        }
    }
}
