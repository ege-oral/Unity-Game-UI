using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace UI.Popup
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class BasePopup : MonoBehaviour
    {
        [SerializeField] private RectTransform popupContainer;
        [SerializeField] private float animationDuration = 0.3f;
        [SerializeField] private Ease showEase = Ease.OutBack;
        [SerializeField] private Ease closeEase = Ease.InBack;

        public abstract string PopupName { get; }

        private CanvasGroup _canvasGroup;
        private Sequence _sequence;

        protected virtual void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public async UniTask Show(IPopupData data = null)
        {
            _sequence?.Kill();
            gameObject.SetActive(true);
            transform.SetAsLastSibling();

            popupContainer.localScale = Vector3.zero;
            _canvasGroup.alpha = 0f;

            _sequence = DOTween.Sequence()
                .Join(popupContainer.DOScale(1f, animationDuration).SetEase(showEase))
                .Join(_canvasGroup.DOFade(1f, animationDuration))
                .SetUpdate(true);

            OnShow(data);
            await _sequence.AsyncWaitForCompletion();
        }

        public async UniTask Close()
        {
            _sequence?.Kill();

            _sequence = DOTween.Sequence()
                .Join(popupContainer.DOScale(0f, animationDuration).SetEase(closeEase))
                .Join(_canvasGroup.DOFade(0f, animationDuration))
                .SetUpdate(true);

            await _sequence.AsyncWaitForCompletion();
            OnClose();
            gameObject.SetActive(false);
        }

        protected virtual void OnShow(IPopupData data) { }
        protected virtual void OnClose() { }

        private void OnDestroy()
        {
            _sequence?.Kill();
        }
    }
}
