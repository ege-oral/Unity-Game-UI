using Coffee.UIExtensions;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UI.Configs;
using UI.Util;
using UI.Widgets;
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
        [SerializeField] private UIParticle starBurst;

        [Header("Rewards")]
        [SerializeField] private RewardItem coinReward;
        [SerializeField] private RewardItem crownReward;
        [SerializeField] private IconDatabase iconDatabase;

        [Header("Settings")]
        [SerializeField] private LevelCompletePopupSettings settings;

        public override string PopupName => PopupKeys.LevelComplete;

        private string _titleFull;

        private void OnEnable()
        {
            homeButton.onClick.AddListener(OnHomeButtonPressed);
            rewardButton.onClick.AddListener(OnRewardButtonPressed);
        }

        private void OnDisable()
        {
            homeButton.onClick.RemoveListener(OnHomeButtonPressed);
            rewardButton.onClick.RemoveListener(OnRewardButtonPressed);
        }

        protected override void OnShow(IPopupData data)
        {
            if (data is not LevelCompleteData levelData) return;

            ResetState();
            PlayEntrance(levelData).Forget();
        }

        private void ResetState()
        {
            _titleFull ??= titleText.text;
            titleText.text = "<alpha=#00>" + _titleFull;

            star.localScale = Vector3.zero;
            star.localEulerAngles = new Vector3(0f, 0f, -90f);

            scoreText.text = string.Empty;

            coinReward.transform.localScale = Vector3.zero;
            crownReward.transform.localScale = Vector3.zero;

            homeButton.transform.localScale = Vector3.zero;
            rewardButton.transform.localScale = Vector3.zero;
        }

        private async UniTaskVoid PlayEntrance(LevelCompleteData data)
        {
            await UniTask.Delay((int)(settings.initialDelay * 1000), DelayType.UnscaledDeltaTime);

            await UniTask.Delay((int)(settings.titleDelay * 1000), DelayType.UnscaledDeltaTime);
            RevealTitle().Forget();

            await UniTask.Delay((int)((settings.starDelay - settings.titleDelay) * 1000), DelayType.UnscaledDeltaTime);
            RevealStar().Forget();

            await UniTask.Delay((int)((settings.scoreDelay - settings.starDelay) * 1000), DelayType.UnscaledDeltaTime);
            CountScore(data.Score).Forget();

            await UniTask.Delay((int)((settings.rewardDelay - settings.scoreDelay) * 1000), DelayType.UnscaledDeltaTime);
            RevealRewards(data.Coins, data.Crowns);

            await UniTask.Delay((int)(settings.buttonsDelay * 1000), DelayType.UnscaledDeltaTime);
            RevealButtons();
        }

        private void RevealButtons()
        {
            homeButton.transform.DOScale(1f, settings.buttonsDuration).SetEase(Ease.OutBack).SetUpdate(true);
            rewardButton.transform.DOScale(1f, settings.buttonsDuration).SetEase(Ease.OutBack).SetUpdate(true);
        }

        private async UniTaskVoid RevealTitle()
        {
            _titleFull ??= titleText.text;
            var interval = (int)(settings.titleCharInterval * 1000);

            for (var i = 0; i <= _titleFull.Length; i++)
            {
                titleText.text = _titleFull.Substring(0, i) + "<alpha=#00>" + _titleFull.Substring(i);
                if (i < _titleFull.Length)
                    await UniTask.Delay(interval, DelayType.UnscaledDeltaTime);
            }

            titleText.text = _titleFull;
        }

        private async UniTaskVoid RevealStar()
        {
            var seq = DOTween.Sequence().SetUpdate(true);
            seq.Append(star.DOScale(settings.starOvershootScale, settings.starDuration * 0.6f).SetEase(Ease.OutCubic));
            seq.Append(star.DOScale(1f, settings.starDuration * 0.4f).SetEase(Ease.OutQuad));
            seq.Insert(0f, star.DORotate(Vector3.zero, settings.starDuration).SetEase(Ease.OutBack));
            starBurst.Play();

            await seq.AsyncWaitForCompletion();
        }

        private async UniTaskVoid CountScore(int target)
        {
            await RewardUtil.CountUp(scoreText, target, settings.scoreCountDuration);
        }

        private void RevealRewards(int coins, int crowns)
        {
            RevealReward(coinReward, IconType.Coin, coins);
            RevealReward(crownReward, IconType.Crown, crowns);
        }

        private void RevealReward(RewardItem reward, IconType iconType, int amount)
        {
            reward.Setup(iconDatabase.Get(iconType), 0);
            reward.transform.DOScale(1f, settings.rewardIconDuration).SetEase(Ease.OutBack).SetUpdate(true);
            RewardUtil.CountUp(reward.CountText, amount, settings.rewardCountDuration).Forget();
        }

        protected override void OnClose()
        {
            starBurst.Stop();
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
