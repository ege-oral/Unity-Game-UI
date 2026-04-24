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
        [SerializeField] private Button button;

        public event Action ButtonPressed;

        private void Awake()
        {
            button.onClick.AddListener(HandleButtonPress);
        }

        private void OnDestroy()
        {
            button.onClick.RemoveListener(HandleButtonPress);
        }

        public virtual void Setup(int count)
        {
            countText.text = FormatCount(count);
        }

        protected virtual string FormatCount(int count) => count.ToString();

        private void HandleButtonPress()
        {
            ButtonPressed?.Invoke();
        }
    }
}
