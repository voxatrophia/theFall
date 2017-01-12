using UnityEngine;
using UnityEngine.EventSystems;

public class Pause : Menu {

    public GameObject pauseMenu;
    public GameObject startFocus;
    public GameObject OptionsMenu;

    public AudioClip hoverSound;

    void Start() {
        pauseMenu.SetActive(false);
        OptionsMenu.SetActive(false);
    }

    public void Hover() {
        AudioManager.Instance.PlaySoundEffect(hoverSound);
        //audioSrc.PlayOneShot(hoverSound);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(Controls.Instance.pause)) {
            switch (menus.Count) {
                case 0:
                    PauseGame();
                    break;
                case 1:
                    Resume();
                    break;
                default:
                    CloseOpenMenu();
                    break;
            }
        }

        if (Input.GetKeyDown(Controls.Instance.back)) {
            switch (menus.Count) {
                case 1:
                    Resume();
                    break;
                default:
                    CloseOpenMenu();
                    break;
            }
        }
    }

    public void PauseGame() {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        menus.AddLast(pauseMenu);
        FocusHereNext(startFocus);
        ////Setting to null first seems to fix a bug
        ////Bug: Doesn't initially trigger the highlight color
        //EventSystem.current.SetSelectedGameObject(null);
        //EventSystem.current.SetSelectedGameObject(startFocus);
        //Handled in the Menu class now
        EventManager.TriggerEvent("GamePaused");
        AudioManager.Instance.Pause();
    }

    public void Resume() {
        AudioManager.Instance.Play();
        EventManager.TriggerEvent("UnPaused");
        Time.timeScale = 1;
        CloseOpenMenu();
    }

    public void ConfirmQuit() {
        StandardModal(Quit);
    }

    public void Quit() {
        AppHelper.Quit();
    }

    public void ConfirmClearScore() {
        StandardModal(ClearScore);
    }

    public void ClearScore() {
        PlayerPrefs.SetInt("HighScore", 0);
        CloseOpenMenu();
    }

    public void ConfirmMainMenu() {
        StandardModal(MainMenu);
    }

    public void MainMenu() {
        AudioManager.Instance.StopSound();
        Time.timeScale = 1;
        MainController.SwitchScene(Scenes.StartScene);
    }
}
