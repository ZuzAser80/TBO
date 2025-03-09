using System.Collections.Generic;
using Assets.Scripts.Items;
using Assets.Scripts.Players;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Level {
    public class LevelClass : MonoBehaviour {
        public int CurrentScore = 0;
        public int HighScore = 0;
        [SerializeField] private List<AbstractItem> lootTable = new List<AbstractItem>();

        void Start()
        {
            HighScore = PlayerPrefs.GetInt("highscore");
            PlayerUIManager.Instance.SetNewHighScore(HighScore);
        }

        public void Win() {
            CurrentScore += 1;
            if (CurrentScore >= HighScore) {
                HighScore = CurrentScore;
                PlayerPrefs.SetInt("highscore", HighScore);
                PlayerUIManager.Instance.SetNewHighScore(HighScore);
            }
            PlayerUIManager.Instance.SetNewScore(CurrentScore);
            GetComponent<Player>().inventory.AddItem(lootTable[Random.Range(0, lootTable.Count)]);
            // pull up screen
            Time.timeScale = 0;
            PlayerUIManager.Instance.Win();
        }
        public void SwitchToScene(int index) => SceneManager.LoadScene(index);
        public void Lose() {
            // pull up screen
            Time.timeScale = 0;
            PlayerUIManager.Instance.Lose();
        }
    }
}