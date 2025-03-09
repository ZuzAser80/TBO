using Assets.Scripts.Level;
using Assets.Scripts.Players;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Items {
    public class ItemSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {
        public AbstractItem item;
        public int Count;

        [SerializeField] private Image itemIcon;
        [SerializeField] private TextMeshProUGUI countText;
        
        private void Awake() {
            UpdateIcon();
        }

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
            item = null;
            countText.gameObject.SetActive(false);
        }

        public void UpdateIcon() {
            if (Count >= 1) {
                itemIcon.sprite = item.Sprite; 
                itemIcon.color = Color.white;
                countText.text = Count.ToString();
                countText.gameObject.SetActive(true);
            } else {
                itemIcon.sprite = null; 
                itemIcon.color = new Color(0, 0, 0, 0);
                countText.gameObject.SetActive(false);
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            item.OnUse?.Invoke(item.targetedAtEnemy ? GridManager.Instance.gameObject.GetComponent<Player>() : GridManager.Instance.gameObject.GetComponent<Player>());
            Decrement();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (item == null) return;
            UpdateIcon();
            PlayerUIManager.Instance.ToggleItemInfoPanel(true);
            PlayerUIManager.Instance.UpdateItemInfoPanel(item);
        }

        public void OnPointerExit(PointerEventData eventData) { 
            if (item == null) return;
            PlayerUIManager.Instance.ToggleItemInfoPanel(false); 
        }
    }
}