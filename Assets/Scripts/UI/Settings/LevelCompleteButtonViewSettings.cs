using UnityEngine;

namespace UI.Settings
{
    [CreateAssetMenu(fileName = "LevelCompleteButtonViewSettings", menuName = "UI Config/Level Complete Button View Settings")]
    public class LevelCompleteButtonViewSettings : ScriptableObject
    {
        [Header("Appear")]
        public float showDelay = 0.2f;
        public float showDuration = 0.5f;
        public DG.Tweening.Ease showEase = DG.Tweening.Ease.OutBack;

        [Header("Disappear")]
        public float hideDuration = 0.3f;
        public DG.Tweening.Ease hideEase = DG.Tweening.Ease.InBack;
    }
}
