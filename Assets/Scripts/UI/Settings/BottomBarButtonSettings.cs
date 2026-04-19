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
    }
}
