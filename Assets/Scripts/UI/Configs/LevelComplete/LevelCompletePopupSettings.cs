using UnityEngine;

namespace UI.Configs
{
    [CreateAssetMenu(fileName = "LevelCompletePopupSettings", menuName = "UI Config/Level Complete Popup Settings")]
    public class LevelCompletePopupSettings : ScriptableObject
    {
        [Header("Timing")]
        public float initialDelay = 0.5f;
        public float titleDelay = 0f;
        public float starDelay = 0.15f;
        public float scoreDelay = 0.35f;
        public float rewardDelay = 0.55f;

        [Header("Title")]
        public float titleCharInterval = 0.06f;

        [Header("Star")]
        public float starDuration = 0.45f;
        public float starOvershootScale = 1.3f;

        [Header("Score")]
        public float scoreCountDuration = 0.8f;

        [Header("Rewards")]
        public float rewardIconDuration = 0.35f;
        public float rewardCountDuration = 0.6f;

        [Header("Buttons")]
        public float buttonsDelay = 0.4f;
        public float buttonsDuration = 0.35f;
    }
}
