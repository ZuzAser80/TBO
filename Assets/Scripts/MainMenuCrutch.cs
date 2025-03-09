using TMPro;
using UnityEngine;

public class MainMenuCrutch : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI text;
    private void Start() {
        text.text = "РЕКОРД: " + PlayerPrefs.GetInt("highscore");
    }
    public void SwitchToScene(int index) => UnityEngine.SceneManagement.SceneManager.LoadScene(index);
}