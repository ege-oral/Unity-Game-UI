using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Popup
{
    public class SettingsPopup : BasePopup
    {
        [Header("Buttons")]
        [SerializeField] private Button closeButton;
        [SerializeField] private Button soundToggleButton;
        [SerializeField] private Button musicToggleButton;
        [SerializeField] private Button vibrationToggleButton;
        [SerializeField] private Button notificationToggleButton;

        [Header("Sliders")]
        [SerializeField] private Slider soundToggle;
        [SerializeField] private Slider musicToggle;
        [SerializeField] private Slider vibrationToggle;
        [SerializeField] private Slider notificationToggle;

        [Header("Toggle Settings")]
        [SerializeField] private float toggleDuration = 0.2f;

        public override string PopupName => PopupKeys.Settings;

        private void OnEnable()
        {
            closeButton.onClick.AddListener(OnCloseButtonPressed);
            soundToggleButton.onClick.AddListener(() => ToggleSlider(soundToggle));
            musicToggleButton.onClick.AddListener(() => ToggleSlider(musicToggle));
            vibrationToggleButton.onClick.AddListener(() => ToggleSlider(vibrationToggle));
            notificationToggleButton.onClick.AddListener(() => ToggleSlider(notificationToggle));
        }

        private void OnDisable()
        {
            closeButton.onClick.RemoveListener(OnCloseButtonPressed);
            soundToggleButton.onClick.RemoveAllListeners();
            musicToggleButton.onClick.RemoveAllListeners();
            vibrationToggleButton.onClick.RemoveAllListeners();
            notificationToggleButton.onClick.RemoveAllListeners();
        }

        private void ToggleSlider(Slider slider)
        {
            float target = slider.value < 0.5f ? 1f : 0f;
            slider.DOValue(target, toggleDuration).SetEase(Ease.OutQuad).SetUpdate(true);
        }

        private void OnCloseButtonPressed()
        {
            Close().Forget();
        }
    }
}
