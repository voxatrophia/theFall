using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour {

	AudioSource audioSrc;
	public AudioClip hoverSound;
	public AudioClip clickSound;

	void Start(){
		audioSrc = GetComponent<AudioSource>();
	}

	public void Hover(){
		audioSrc.PlayOneShot(hoverSound);
	}

	public void PlayGame(){
		StartCoroutine(Play());
	}

	IEnumerator Play(){
		audioSrc.PlayOneShot(clickSound);
		yield return Yielders.Get(1f);
		MainController.SwitchScene(Scenes.Main);
	}
}
