using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {

	public GameObject pauseMenu;
	public GameObject optionsMenu;
	public GameObject quitMenu;

	enum menu {Normal, Paused, Options, Quit}

	int menuState;

	void Start(){
		menuState = (int)menu.Normal;
		pauseMenu.SetActive(false);
		optionsMenu.SetActive(false);
		quitMenu.SetActive(false);
	}

	void Update(){
		if(Input.GetButtonDown("Cancel")){
			switch(menuState){
				case (int)menu.Normal:
					PauseGame();
					break;
				case (int)menu.Paused:
					Resume();
					break;
				case (int)menu.Options:
					CancelOptions();
					break;
				case (int)menu.Quit:
					CancelConfirm();
					break;
			}
		}
	}

	public void PauseGame(){
		AudioManager.Instance.Pause();
		menuState = (int)menu.Paused;
		Time.timeScale = 0;
		pauseMenu.SetActive(true);
	}

	public void Resume(){
		AudioManager.Instance.Play();
		menuState = (int)menu.Normal;
		Time.timeScale = 1;
		pauseMenu.SetActive(false);
	}

	public void Options(){
		menuState = (int)menu.Options;
		optionsMenu.SetActive(true);
	}

	public void CancelOptions(){
		menuState = (int)menu.Paused;
		optionsMenu.SetActive(false);
	}

	public void Confirm(string Where){
		Debug.Log(Where);
	}

	public void Confirm(){
		menuState = (int)menu.Quit;
		quitMenu.SetActive(true);
	}

	public void CancelConfirm(){
		menuState = (int)menu.Paused;
		quitMenu.SetActive(false);
	}

	public void Restart(){
		AudioManager.Instance.StopSound();
		Time.timeScale = 1;
        MainController.SwitchScene(Scenes.StartScene);
	}

	public void Quit(){
		Application.Quit();
	}
}