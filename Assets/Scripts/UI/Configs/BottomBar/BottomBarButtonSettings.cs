using UnityEngine;

namespace UI.Configs
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

        [Header("Selected Idle (Bob + Squash)")]
        public float idleBobHeight = 8f;
        public float idleBobDuration = 0.55f;
        public float idlePauseDuration = 0.35f;
        public float idleStretchX = 0.94f;
        public float idleStretchY = 1.08f;
        public float idleSquashX = 1.10f;
        public float idleSquashY = 0.90f;

        [Header("Locked Shake (on tap)")]
        public float lockedShakeStrength = 12f;
        public float lockedShakeDuration = 0.4f;
        public int lockedShakeVibrato = 20;
        public float lockedShakeRotation = 15f;
        [Range(0f, 1f)] public float lockedRotateInPortion = 0.25f;
        [Range(0f, 1f)] public float lockedRotateOutPortion = 0.25f;
    }
}
