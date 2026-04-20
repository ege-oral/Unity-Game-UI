using UnityEngine;

namespace UI.Settings
{
    [CreateAssetMenu(fileName = "BottomBarButtonSettings", menuName = "UI Config/Bottom Bar Button Settings")]
    public class BottomBarButtonSettings : ScriptableObject
    {
        [Header("Width")]
        public float normalWidth = 145f;
        public float selectedWidth = 300f;

        [Header("Icon Position")]
        public float iconNormalY = 0f;
        public float iconSelectedY = 47f;

        [Header("Animation")]
        public float animationDuration = 0.3f;
        public DG.Tweening.Ease animationEase = DG.Tweening.Ease.OutQuad;

        [Header("Locked Shake (on tap)")]
        public float lockedShakeStrength = 12f;
        public float lockedShakeDuration = 0.4f;
        public int lockedShakeVibrato = 20;
        public float lockedShakeRotation = 15f;
        [Range(0f, 1f)] public float lockedRotateInPortion = 0.25f;
        [Range(0f, 1f)] public float lockedRotateOutPortion = 0.25f;
    }
}
