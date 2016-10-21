using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.Audio;

public class StartGame : Menu {
    AudioSource audioSrc;
	public AudioClip hoverSound;
	public AudioClip clickSound;
    public GameObject startFocus;

    protected override void OnAwake() {
    }

    void Start() {
        audioSrc = GetComponent<AudioSource>();
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(startFocus);
    }

    public void Hover() {
        audioSrc.PlayOneShot(hoverSound);
    }

    public void StoryMode() {
        PlayerPrefs.SetInt("GameMode", Modes.Story);
        StartCoroutine(Play());
    }

    public void ArcadeMode() {
        PlayerPrefs.SetInt("GameMode", Modes.Arcade);
        StartCoroutine(Play());
    }

    IEnumerator Play() {
        audioSrc.PlayOneShot(clickSound);
        yield return Yielders.Get(0.5f);
        if (PlayerPrefsX.GetBool("NewPlayer", true)) {
            PlayerPrefsX.SetBool("NewPlayer", true);
            MainController.SwitchScene("Tutorial_1");
        }
        else {
            MainController.SwitchScene(Scenes.Main);
        }
    }

    public void Quit() {
        AppHelper.Quit();
    }

    public void ConfirmQuit() {
        StandardModal(Quit);
    }

    public void ConfirmClearScore() {
        StandardModal(ClearScore);
    }

    public void StartTutorial() {
        MainController.SwitchScene("Tutorial_1");
    }

    public void ClearScore() {
        audioSrc.PlayOneShot(clickSound);
        //PlayerPrefs.SetInt("HighScore", 0);
        DataAccess.Clear(Data.ScoreTable);

        selected.AddLast(selected.Last.Value);

        CloseOpenMenu();

        StartCoroutine(Announcement("High Scores Cleared!"));
    }
}
