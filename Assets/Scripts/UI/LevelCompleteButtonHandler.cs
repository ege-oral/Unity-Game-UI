using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UI.Popup;
using VContainer.Unity;

namespace UI
{
    public class LevelCompleteButtonHandler : IAsyncStartable, IDisposable
    {
        private readonly LevelCompleteButtonView _view;
        private readonly IPopupService _popupService;

        public LevelCompleteButtonHandler(LevelCompleteButtonView view, IPopupService popupService)
        {
            _view = view;
            _popupService = popupService;
        }

        public async UniTask StartAsync(CancellationToken cancellation)
        {
            _view.Clicked += OnClicked;
            await _view.Show().AttachExternalCancellation(cancellation);
        }

        public void Dispose()
        {
            _view.Clicked -= OnClicked;
        }

        private void OnClicked()
        {
            var data = new LevelCompleteData
            {
                Score = _view.SampleScore,
                Stars = _view.SampleStars,
                Coins = _view.SampleCoins,
                Crowns = _view.SampleCrowns
            };
            _popupService.Show(PopupKeys.LevelComplete, data).Forget();
        }
    }
}
