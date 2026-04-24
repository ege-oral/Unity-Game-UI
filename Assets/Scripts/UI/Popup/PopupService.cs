using Cysharp.Threading.Tasks;

namespace UI.Popup
{
    public class PopupService : IPopupService
    {
        private readonly PopupRegistry _registry;

        public PopupService(PopupRegistry registry)
        {
            _registry = registry;
        }

        public async UniTask<BasePopup> Show(string key, IPopupData data = null)
        {
            if (!_registry.TryGet(key, out var popup))
            {
                Core.Logger.Error("PopupService", $"Popup not found: {key}");
                return null;
            }

            await popup.Show(data);
            return popup;
        }

        public async UniTask Close(BasePopup popup)
        {
            if (popup == null) return;

            await popup.Close();
        }
    }
}
