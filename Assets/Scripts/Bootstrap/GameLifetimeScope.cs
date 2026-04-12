using UI.Popup;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] private PopupRegistry popupRegistry;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponent(popupRegistry);
        builder.Register<PopupService>(Lifetime.Singleton).AsImplementedInterfaces();
    }
}
