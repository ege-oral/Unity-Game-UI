# Unity Game UI

Unity **6.3 LTS (6000.3.6f1)** · URP · Single scene (`Assets/Scenes/MainMenu.unity`).

## Tasks

### 1. Home Screen & Bottom Bar
Top bar (coins / lives / stars / settings) and a 5-button bottom bar (3 unlocked, 2 locked). `BottomBarView` raises `ContentActivated(BottomBarTab)` / `Closed` events. Locked buttons shake on tap; selected button plays a subtle idle bob+squash.

### 2. Settings Popup
Inherits a shared `BasePopup` with scale+fade open/close. Full-screen blurred backdrop is a reusable component (`BlurredBackdrop`) any popup can drop in. Click the overlay to dismiss.

### 3. Level Completed Screen
Triggered from a Home Screen button. Staggered opening: typewriter title → star pop (overshoot + rotate + sparkle particle burst) → score count-up → coin/crown reveal + count-up → action buttons pop. Home button closes.

## Architecture

- **VContainer DI** — `GameLifetimeScope` registers views and entry-point handlers.
- **View + Handler split** — `*View` is a MonoBehaviour (animation + events), `*Handler` is the DI entry point (`IAsyncStartable`).
- **Popup system** — `BasePopup` + `IPopupService` + `PopupRegistry`. Add a new popup by deriving `BasePopup`, adding a key in `PopupKeys`, and dropping the prefab in the registry.
- **ScriptableObject-driven tuning** — all timings / offsets / eases live in `Scripts/UI/Configs/` SOs so designers can iterate without code changes.
- **Async** — `UniTask` for flow, DOTween for tweens, sequences killed in `OnDestroy`.
- **Feature-grouped folders** — `Scripts/UI/{BottomBar,TopBar,LevelComplete,Settings,Popup,Widgets,Configs,Util}` with matching namespaces.

## Third-party

DOTween · UniTask · VContainer · UI Particle

## Run

1. Open in Unity 6.3 LTS (6000.3.6f1).
2. Open `Assets/Scenes/MainMenu.unity` and press Play.
