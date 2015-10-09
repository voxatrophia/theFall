using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour {

	AudioSource audioSrc;
	public AudioClip hoverSound;
	public AudioClip clickSound;

	void Start(){
		audioSrc = GetComponent<AudioSource>();
	}

	void OnMouseEnter(){
		audioSrc.PlayOneShot(hoverSound);
	}

	public void PlayGame(){
		StartCoroutine("Play");
	}

	IEnumerator Play(){
		audioSrc.PlayOneShot(clickSound);
		yield return new WaitForSeconds(1f);
		MainController.SwitchScene("Main");
	}
}
