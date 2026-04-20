using UnityEngine;

namespace UI.Settings
{
    [CreateAssetMenu(fileName = "TopBarViewSettings", menuName = "UI Config/Top Bar View Settings")]
    public class TopBarViewSettings : ScriptableObject
    {
        [Header("Appear")]
        public float showDelay = 0.2f;
        public float showDuration = 0.75f;
        public DG.Tweening.Ease showEase = DG.Tweening.Ease.OutBack;

        [Header("Disappear")]
        public float hideDuration = 0.4f;
        public DG.Tweening.Ease hideEase = DG.Tweening.Ease.InCubic;

        [Header("Slide")]
        public float slideOffset = 400f;
    }
}
