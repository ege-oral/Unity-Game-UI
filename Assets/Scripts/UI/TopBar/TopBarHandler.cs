using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using VContainer.Unity;

namespace UI.TopBar
{
    public class TopBarHandler : IAsyncStartable, IDisposable
    {
        private readonly TopBarView _view;

        public TopBarHandler(TopBarView view)
        {
            _view = view;
        }

        public async UniTask StartAsync(CancellationToken cancellation)
        {
            await _view.Show().AttachExternalCancellation(cancellation);
        }

        public void Dispose()
        {
        }
    }
}
