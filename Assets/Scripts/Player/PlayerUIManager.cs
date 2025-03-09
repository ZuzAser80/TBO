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
        [SerializeField] private TextMeshProUGUI itemNameText;
        [SerializeField] private GameObject itemInfoPanel;
        [Space]
        [SerializeField] private TextMeshProUGUI atkText;
        [SerializeField] private TextMeshProUGUI defText;
        [SerializeField] private TextMeshProUGUI mgcText;
        [Space]
        [SerializeField] private TextMeshProUGUI abilityText;
        [Space]
        [SerializeField] private GameObject winScreen;
        [SerializeField] private GameObject loseScreen;
        [Space]
        [SerializeField] private TextMeshProUGUI highScoreText;
        [SerializeField] private TextMeshProUGUI currentScoreText;

        public void Awake() {
            Instance = this;
        }

        public void UpdateCellInfo(Stats stats) {
            cellAtkText.text = stats.Attack.ToString();
            cellDefText.text = stats.Defence.ToString();
            cellMagText.text = stats.Magic.ToString();
        }

        public void UpdatecellVisibility(bool val, bool can) {
            CellInfoPanel.SetActive(val); 
            CellInfoPanel.GetComponent<Image>().color = can ? Color.green : Color.red;
        }

        public void UpdatePlayerStats(Stats stats) {
            atkText.text = stats.Attack.ToString();
            defText.text = stats.Defence.ToString();
            mgcText.text = stats.Magic.ToString();
        }

        public void UpdateItemInfoPanel(AbstractItem item) {
            itemNameText.text = item.Name;
            itemInfoText.text = item.Description;
        }
        public void SetNewScore(int score) => currentScoreText.text = score.ToString();
        public void SetNewHighScore(int score) => highScoreText.text = score.ToString();
        public void Win() => winScreen.SetActive(true);
        public void Lose() => loseScreen.SetActive(true);
        public void SetAbilityText(string abl) => abilityText.text = "КЛАСС: " + abl;
        public void ToggleItemInfoPanel(bool val) => itemInfoPanel.SetActive(val);

    }
}