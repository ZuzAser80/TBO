using System;
using Assets.Scripts.Players;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Level {
    [RequireComponent(typeof(Button))]
    public class GridCell : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public CellState currentState;
        
        public Vector3 GridPos = new Vector3();
        public Stats StatsToTake;
        public UnityEvent onDoneClick;
        public bool isProtected = false;
        public bool isAvailable = true;
        [SerializeField] private Image cellIcon;
        [SerializeField] private Sprite TIC;
        [SerializeField] private Sprite TAC;
        private bool canTake => ((IEntity)GridManager.Instance.GetComponent<Player>()).CurrentStats.MoreThen(StatsToTake) > 1;

        private void Awake() {
            currentState = CellState.NONE;
            GridPos = new Vector2((int)(GetComponent<RectTransform>().localPosition.x / GetComponent<RectTransform>().sizeDelta.x), (int)(GetComponent<RectTransform>().localPosition.y / GetComponent<RectTransform>().sizeDelta.y));
        }

        public bool TrySetState(CellState newState) { 
            if (!isAvailable || isProtected || !canTake) { return false; }
            currentState = newState; 
            return true;
        }

        public void Protect() { isProtected = true; }
        public void setAvailable(bool value) => isAvailable = value;

        public void OnClick() { 
            if(TrySetState(GridManager.Instance.currentMove)) {
                UpdateColor();
                onDoneClick?.Invoke(); 
            }
        }

        public void UpdateColor() {
            Color color = Color.gray;
                switch (currentState) {
                    case CellState.NONE:
                        color = new Color(0, 0, 0, 0);
                        break;
                    case CellState.TIC:
                        cellIcon.sprite = TIC;
                        color = Color.white;
                        break;
                    case CellState.TAC:
                        cellIcon.sprite = TAC;
                        color = Color.white;
                        break;
                }
            cellIcon.color = isProtected ? Color.blue : color; 
        }

        public void OnPointerEnter(PointerEventData eventData) {
            PlayerUIManager.Instance.UpdatecellVisibility(true, canTake);
            PlayerUIManager.Instance.UpdateCellInfo(StatsToTake);
        }

        public void OnPointerExit(PointerEventData eventData) => PlayerUIManager.Instance.UpdatecellVisibility(false, canTake);
    }

    public enum CellState { NONE, TIC, TAC }
}
