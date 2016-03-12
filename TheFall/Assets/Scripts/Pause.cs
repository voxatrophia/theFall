using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(AudioSource))]
public class Pause : Menu {

    public GameObject pauseMenu;
    public GameObject startFocus;

    public AudioClip hoverSound;
    AudioSource audioSrc;

    void Start() {
        audioSrc = GetComponent<AudioSource>();
        pauseMenu.SetActive(false);
    }

    public void Hover() {
        audioSrc.PlayOneShot(hoverSound);
    }

    void Update() {
        if (Input.GetButtonDown("Cancel")) {
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
    }

    public void PauseGame() {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        menus.AddLast(pauseMenu);
        //Setting to null first seems to fix a bug
        //Dug: Doesn't initially trigger the highlight color
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(startFocus);
        AudioManager.Instance.Pause();
    }

    public void Resume() {
        AudioManager.Instance.Play();
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
