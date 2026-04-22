using Cysharp.Threading.Tasks;
using UI.Popup;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

namespace UI
{
    public class LevelCompleteButtonHandler : MonoBehaviour
    {
        [SerializeField] private Button levelCompleteButton;
        [SerializeField] private int sampleScore = 12345;
        [SerializeField, Range(0, 3)] private int sampleStars = 3;

        private IPopupService _popupService;

        private void Awake()
        {
            var container = LifetimeScope.Find<GameLifetimeScope>().Container;
            _popupService = container.Resolve<IPopupService>();
        }

        private void OnEnable()
        {
            levelCompleteButton.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            levelCompleteButton.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            var data = new LevelCompleteData
            {
                Score = sampleScore,
                Stars = sampleStars
            };
            _popupService.Show(PopupKeys.LevelComplete, data).Forget();
        }
    }
}
