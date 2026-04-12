using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Popup
{
    public class SettingsPopup : BasePopup
    {
        [Header("Buttons")]
        // [SerializeField] private Button closeButton;

        // [Header("Toggles")]
        // [SerializeField] private Toggle soundToggle;
        // [SerializeField] private Toggle musicToggle;
        // [SerializeField] private Toggle vibrationToggle;
        // [SerializeField] private Toggle notificationToggle;

        public override string PopupName => PopupKeys.Settings;

        private void OnEnable()
        {
            // closeButton.onClick.AddListener(OnCloseButtonPressed);
        }

        private void OnDisable()
        {
            // closeButton.onClick.RemoveListener(OnCloseButtonPressed);
        }

        private void OnCloseButtonPressed()
        {
            Close().Forget();
        }
    }
}
