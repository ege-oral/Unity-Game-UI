using Cysharp.Threading.Tasks;
using UI.Popup;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

namespace UI.Settings
{
    public class SettingsButtonHandler : MonoBehaviour
    {
        [SerializeField] private Button settingsButton;

        private IPopupService _popupService;

        private void Awake()
        {
            var container = LifetimeScope.Find<GameLifetimeScope>().Container;
            _popupService = container.Resolve<IPopupService>();
        }

        private void OnEnable()
        {
            settingsButton.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            settingsButton.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            _popupService.Show(PopupKeys.Settings).Forget();
        }
    }
}
