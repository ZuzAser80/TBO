using System;
using Assets.Scripts.Items;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.Players {
    public class PlayerUIManager : MonoBehaviour {        
        public static PlayerUIManager Instance {get;private set;}
        [SerializeField] private GameObject CellInfoPanel;
        [SerializeField] private TextMeshProUGUI cellAtkText;
        [SerializeField] private TextMeshProUGUI cellDefText;
        [SerializeField] private TextMeshProUGUI cellMagText;
        [Space]
        [SerializeField] private TextMeshProUGUI itemInfoText;
        [SerializeField] private GameObject itemInfoPanel;

        public void Awake() {
            Instance = this;
        }

        public void UpdateCellInfo(Stats stats) {
            cellAtkText.text = "ATK: " + stats.Attack.ToString();
            cellDefText.text = "DEF: " + stats.Defence.ToString();
            cellMagText.text = "MGC: " + stats.Magic.ToString();
        }

        public void UpdatecellVisibility(bool val, bool can) {
            CellInfoPanel.SetActive(val); 
            CellInfoPanel.GetComponent<Image>().color = can ? Color.green : Color.red;
        }

        public void UpdateItemInfoPanel(AbstractItem item) {
            itemInfoText.text = item.Description;
        }

        public void ToggleItemInfoPanel(bool val) => itemInfoPanel.SetActive(val);

    }
}