using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextTyper : MonoBehaviour {

	public float letterPause = 0.2f;
	public AudioClip clickSound;
	AudioSource aud;

	public string message;
	Text textComp;
	void Start () {
		textComp = GetComponent<Text>();
		aud = GetComponent<AudioSource>();
		message = textComp.text;
		textComp.text = "";
		StartCoroutine(TypeText ());
	}

	IEnumerator TypeText () {
		foreach (char letter in message.ToCharArray()) {
			textComp.text += letter;
			aud.pitch = Random.Range(1f,2f);
			aud.PlayOneShot(clickSound);
			yield return Yielders.Get(letterPause);
		}
	}
}
