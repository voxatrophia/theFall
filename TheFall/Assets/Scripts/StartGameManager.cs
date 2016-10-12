using UnityEngine;
using System.Collections;

public class StartGameManager : MonoBehaviour {
    AudioSource audioSrc;

    void Start() {
        audioSrc = GetComponent<AudioSource>();
        StartCoroutine(StartMusic());
    }

    IEnumerator StartMusic() {
        yield return Yielders.Get(0.5f);
        audioSrc.Play();
    }
}
