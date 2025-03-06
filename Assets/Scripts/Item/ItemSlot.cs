using Assets.Scripts.Players;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Items {
    public class ItemSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {
        public AbstractItem item;
        public int Count { get; private set; }

        [SerializeField] private Image itemIcon;
        [SerializeField] private TextMeshProUGUI countText;

        public void Increment() {
            Count += 1;
            if (!countText.gameObject.activeSelf) { UpdateIcon(); countText.gameObject.SetActive(true); }
            countText.text = Count.ToString();
        }

        public void Decrement() {
            if (Count > 1) { Count -= 1; UpdateIcon(); }
            else { ClearSlot(); }
            countText.text = Count.ToString();
        }
        private void ClearSlot() { 
            itemIcon.color = new Color(0, 0, 0, 0);
            itemIcon.sprite = null;
            countText.gameObject.SetActive(false);
        }

        private void UpdateIcon() {
            if (Count >= 1) {
                itemIcon.sprite = item.Sprite; 
                itemIcon.color = new Color(1, 1, 1, 1);
            } else {
                itemIcon.sprite = null; 
                itemIcon.color = new Color(0, 0, 0, 0);
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            item.OnUse?.Invoke();
            Decrement();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            UpdateIcon();
            PlayerUIManager.Instance.ToggleItemInfoPanel(true);
            PlayerUIManager.Instance.UpdateItemInfoPanel(item);
        }

        public void OnPointerExit(PointerEventData eventData) => PlayerUIManager.Instance.ToggleItemInfoPanel(false);
    }
}