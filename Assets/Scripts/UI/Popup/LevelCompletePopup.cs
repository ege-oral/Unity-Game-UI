using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Popup
{
    public class LevelCompletePopup : BasePopup
    {
        [Header("Buttons")]
        [SerializeField] private Button homeButton;
        [SerializeField] private Button rewardButton;

        [Header("Display")]
        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private RectTransform star;

        [Header("Optional FX")]
        [SerializeField] private Image flashOverlay;
        [SerializeField] private RectTransform radialShine;
        [SerializeField] private ParticleSystem confettiBurst;
        [SerializeField] private ParticleSystem starBurst;

        [Header("Animation Tuning")]
        [SerializeField] private float scoreCountDuration = 0.8f;
        [SerializeField] private float titlePunchScale = 0.25f;
        [SerializeField] private float titlePunchDuration = 0.5f;
        [SerializeField] private float starAnimationDuration = 0.4f;
        [SerializeField] private float flashDuration = 0.35f;
        [SerializeField] private float radialShineSpinSpeed = 40f;

        public override string PopupName => PopupKeys.LevelComplete;

        private Tween _radialShineSpin;

        private void OnEnable()
        {
            homeButton.onClick.AddListener(OnHomeButtonPressed);
            rewardButton.onClick.AddListener(OnRewardButtonPressed);
        }

        private void OnDisable()
        {
            homeButton.onClick.RemoveListener(OnHomeButtonPressed);
            rewardButton.onClick.RemoveListener(OnRewardButtonPressed);
            _radialShineSpin?.Kill();
        }

        protected override void OnShow(PopupData data)
        {
            if (data is not LevelCompleteData levelData) return;

            PlayFlash();
            StartRadialShine();
            PunchTitle();
            AnimateStar();
            CountScore(levelData.Score).Forget();

            if (confettiBurst != null)
            {
                confettiBurst.Clear(true);
                confettiBurst.Play(true);
            }
        }

        protected override void OnClose()
        {
            _radialShineSpin?.Kill();
        }

        private void PlayFlash()
        {
            if (flashOverlay == null) return;

            var c = flashOverlay.color;
            flashOverlay.color = new Color(c.r, c.g, c.b, 1f);
            flashOverlay.DOFade(0f, flashDuration)
                .SetEase(Ease.OutQuad)
                .SetUpdate(true);
        }

        private void StartRadialShine()
        {
            if (radialShine == null) return;

            _radialShineSpin?.Kill();
            radialShine.localEulerAngles = Vector3.zero;
            _radialShineSpin = radialShine
                .DORotate(new Vector3(0f, 0f, -360f), 360f / radialShineSpinSpeed, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Restart)
                .SetUpdate(true);
        }

        private void PunchTitle()
        {
            if (titleText == null) return;

            titleText.transform.localScale = Vector3.one;
            titleText.transform
                .DOPunchScale(Vector3.one * titlePunchScale, titlePunchDuration, 6, 0.6f)
                .SetUpdate(true);
        }

        private void AnimateStar()
        {
            if (star == null) return;

            star.localScale = Vector3.zero;
            star.localEulerAngles = new Vector3(0f, 0f, -90f);

            var seq = DOTween.Sequence().SetUpdate(true);
            seq.Join(star.DOScale(1f, starAnimationDuration).SetEase(Ease.OutBack));
            seq.Join(star.DORotate(Vector3.zero, starAnimationDuration).SetEase(Ease.OutBack));

            if (starBurst != null)
            {
                starBurst.Clear(true);
                starBurst.Play(true);
            }
        }

        private async UniTaskVoid CountScore(int target)
        {
            if (scoreText == null) return;

            var elapsed = 0f;
            scoreText.text = "0";

            while (elapsed < scoreCountDuration)
            {
                elapsed += Time.unscaledDeltaTime;
                var t = Mathf.Clamp01(elapsed / scoreCountDuration);
                var eased = 1f - Mathf.Pow(1f - t, 3f);
                var current = Mathf.Lerp(0f, target, eased);
                scoreText.text = Mathf.RoundToInt(current).ToString();
                await UniTask.Yield(PlayerLoopTiming.Update);
            }

            scoreText.text = target.ToString();
        }

        private void OnHomeButtonPressed()
        {
            Close().Forget();
        }

        private void OnRewardButtonPressed()
        {
            Close().Forget();
        }
    }
}
