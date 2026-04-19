using System;
using Core;
using VContainer.Unity;

namespace UI
{
    public class BottomBarHandler : IStartable, IDisposable
    {
        private const string LogTag = "BottomBar";

        private readonly BottomBarView _view;

        public BottomBarHandler(BottomBarView view)
        {
            _view = view;
        }

        public void Start()
        {
            _view.ContentActivated += OnContentActivated;
            _view.Closed += OnClosed;
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
