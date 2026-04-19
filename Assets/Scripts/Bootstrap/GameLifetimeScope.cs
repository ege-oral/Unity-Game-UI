using UI;
using UI.Popup;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] private PopupRegistry popupRegistry;
    [SerializeField] private BottomBarView bottomBarView;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponent(popupRegistry);
        builder.Register<PopupService>(Lifetime.Singleton).AsImplementedInterfaces();

        builder.RegisterComponent(bottomBarView);
        builder.RegisterEntryPoint<BottomBarHandler>();
    }
}
