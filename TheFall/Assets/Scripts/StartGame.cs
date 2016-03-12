using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class StartGame : Menu {

    AudioSource audioSrc;
	public AudioClip hoverSound;
	public AudioClip clickSound;

	void Start(){
		audioSrc = GetComponent<AudioSource>();
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
        MainController.SwitchScene(Scenes.Main);
    }

    public void Quit() {
        AppHelper.Quit();
    }

    public void ConfirmQuit() {
        StandardModal(Quit);
    }

    public void ClearHighScore() {
        StandardModal(ClearScore);
    }

    public void ClearScore() {
        audioSrc.PlayOneShot(clickSound);
        PlayerPrefs.SetInt("HighScore", 0);
        CloseOpenMenu();
    }
}
