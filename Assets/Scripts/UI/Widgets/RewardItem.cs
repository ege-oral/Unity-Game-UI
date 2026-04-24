using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Widgets
{
    public class RewardItem : MonoBehaviour
    {
        [SerializeField] private Image iconImage;
        [SerializeField] private TextMeshProUGUI countText;

        public TextMeshProUGUI CountText => countText;

        public void Setup(Sprite icon, int amount)
        {
            iconImage.sprite = icon;
            countText.text = amount.ToString();
        }
    }
}
