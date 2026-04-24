using System;
using System.Threading;
using Core;
using Cysharp.Threading.Tasks;
using VContainer.Unity;

namespace UI.BottomBar
{
    public class BottomBarHandler : IAsyncStartable, IDisposable
    {
        private const string LogTag = "BottomBar";

        private readonly BottomBarView _view;

        public BottomBarHandler(BottomBarView view)
        {
            _view = view;
        }

        public async UniTask StartAsync(CancellationToken cancellation)
        {
            _view.ContentActivated += OnContentActivated;
            _view.Closed += OnClosed;

            await _view.Show().AttachExternalCancellation(cancellation);
        }

        public void Dispose()
        {
            _view.ContentActivated -= OnContentActivated;
            _view.Closed -= OnClosed;
        }

        private void OnContentActivated(BottomBarTab type)
        {
            Logger.Log(LogTag, $"ContentActivated: {type}");
        }

        private void OnClosed()
        {
            Logger.Log(LogTag, "Closed");
        }
    }
}
