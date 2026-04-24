using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace UI.Util
{
    public static class RewardUtil
    {
        public static async UniTask CountUp(TextMeshProUGUI text, int target, float duration)
        {
            var elapsed = 0f;
            text.text = "0";

            while (elapsed < duration)
            {
                elapsed += Time.unscaledDeltaTime;
                var t = Mathf.Clamp01(elapsed / duration);
                var eased = 1f - Mathf.Pow(1f - t, 3f);
                var current = Mathf.Lerp(0f, target, eased);
                text.text = Mathf.RoundToInt(current).ToString();
                await UniTask.Yield(PlayerLoopTiming.Update);
            }

            text.text = target.ToString();
        }
    }
}
