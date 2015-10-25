using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour {

	AudioSource audioSrc;
	public AudioClip hoverSound;
	public AudioClip clickSound;

	public GameObject quitMenu;
	public GameObject optionsMenu;

	void Start(){
		audioSrc = GetComponent<AudioSource>();
		quitMenu.SetActive(false);
		optionsMenu.SetActive(false);
	}

	public void Hover(){
		audioSrc.PlayOneShot(hoverSound);
	}

	public void PlayGame(){
		StartCoroutine(Play());
	}

	IEnumerator Play(){
		audioSrc.PlayOneShot(clickSound);
		yield return Yielders.Get(0.5f);
		MainController.SwitchScene(Scenes.Main);
	}

	public void Confirm(){
		quitMenu.SetActive(true);
	}

	public void CancelConfirm(){
		quitMenu.SetActive(false);
	}

	public void Quit(){
		Application.Quit();
	}

	public void Options(){
		optionsMenu.SetActive(true);
	}

	public void CancelOptions(){
		optionsMenu.SetActive(false);
	}

}
