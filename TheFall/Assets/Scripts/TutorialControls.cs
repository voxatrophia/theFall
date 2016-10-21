using UnityEngine;
using UnityEngine.UI;

public class TutorialControls : MonoBehaviour {

    public Text jumpText;
    public Text pauseText;
    public Text backText;
    public Text itemText;

    public Color newColor;
    Color origColor;

    bool paused;
    public GameObject pauseScreen;

    void Start() {
        pauseScreen.SetActive(false);
        paused = false;
        origColor = itemText.color;

        jumpText.text = Controls.Instance.jump.ToString();
        pauseText.text = Controls.Instance.pause.ToString();
        backText.text = Controls.Instance.back.ToString();
        itemText.text = Controls.Instance.useItem.ToString();
    }

    void Update() {
        if (Input.GetKeyDown(Controls.Instance.pause)) {
            TogglePause();
        }
        if (Input.GetKeyDown(Controls.Instance.useItem)) {
            ToggleColor();
        }
    }

    void TogglePause() {
        paused = !paused;
        Time.timeScale = (paused) ? 0 : 1;
        pauseScreen.SetActive(paused);
    }

    void ToggleColor() {
        itemText.color = (itemText.color == origColor) ? newColor : origColor;
    }
}
