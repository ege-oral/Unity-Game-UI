using Cysharp.Threading.Tasks;

namespace UI.Popup
{
    public interface IPopupService
    {
        UniTask<BasePopup> Show(string key, IPopupData data = null);
        UniTask Close(BasePopup popup);
    }
}
