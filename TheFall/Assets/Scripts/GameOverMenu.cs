using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

[RequireComponent(typeof(AudioSource))]
public class GameOverMenu : Menu {

    public AudioClip hoverSound;
    AudioSource audioSrc;

    void Start() {
        audioSrc = GetComponent<AudioSource>();
    }

    public void Hover() {
        audioSrc.PlayOneShot(hoverSound);
    }

    public void RetryLevel() {
        MainController.SwitchScene(Scenes.Main);
    }

    public void ConfirmQuit() {
        StandardModal(Quit);
    }

    public void Quit() {
        AppHelper.Quit();
    }

    public void ConfirmMainMenu() {
        StandardModal(ReturnToMainMenu);
    }

    public void ReturnToMainMenu()
    {
        MainController.SwitchScene(Scenes.StartScene);
    }
}