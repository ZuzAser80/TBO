using System;
using System.Collections.Generic;
using Assets.Scripts.Items;
using Assets.Scripts.Level;
using Assets.Scripts.Players.Class;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Players {
    public class Player : MonoBehaviour, IEntity {
        public Classes playerClass;
        public bool canUse = false;
        public bool hasUsed = false;
        public Inventory inventory;
        public Dictionary<Classes, IClass> AbilitiesDict = new Dictionary<Classes, IClass> {
            { Classes.WARRIOR, new WarriorClass() },
            { Classes.MAGE, new MageClass() },
            { Classes.ROGUE, new RogueClass() },
            { Classes.PALADIN, new PaladinClass() }
        };
        [SerializeField] private Stats stats;

        Stats IEntity.CurrentStats { get => stats; set => stats = value; }


        private void Start() { 
            PlayerUIManager.Instance.UpdatePlayerStats(stats); 
        }

        public void UpdateClassText() {
            string s = "";
            switch (playerClass) {
                case Classes.WARRIOR:
                    s = "ВОИН";
                    break;
                case Classes.MAGE:
                    s = "МАГ";
                    break;
                case Classes.ROGUE:
                    s = "ПЛУТ";
                    break;
                case Classes.PALADIN:
                    s = "ПАЛАДИН";
                    break;
            }
            PlayerUIManager.Instance.SetAbilityText(s); 
        }

        public void ApplyStats(Stats stats) {
            this.stats.Attack += stats.Attack;
            this.stats.Defence += stats.Defence;
            this.stats.Magic += stats.Magic;
            PlayerUIManager.Instance.UpdatePlayerStats(this.stats);
        }

        private void Update() {
            if(Input.GetKeyDown(KeyCode.Q) && !hasUsed) { AbilitiesDict[playerClass].OnUse(); hasUsed = true; }
        }

    }
    [Serializable]
    public enum Classes { 
        WARRIOR, //2 marks on 1 move #GridManager done
        MAGE, //bomb #GridManager done
        ROGUE, //Erase opponent's mark #GridManager done
        PALADIN //Protect 1 cell for entire game #GridManager done
    }

    [Serializable]
    public class Stats {
        public int Attack = 0;
        public int Defence = 0;
        public int Magic = 0;

        public int MoreThen(Stats stats) => (Attack > stats.Attack ? 1 : 0) + (Defence > stats.Defence ? 1 : 0) + (Magic > stats.Magic ? 1 : 0); 

        public void GenerateRandom(int min, int max) {
            Attack = UnityEngine.Random.Range(min, max);
            Defence = UnityEngine.Random.Range(min, max);
            Magic = UnityEngine.Random.Range(min, max);
        }
    }
}