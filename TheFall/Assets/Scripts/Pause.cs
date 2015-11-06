using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Pause : MonoBehaviour {

	public GameObject pauseMenu;
	public GameObject optionsMenu;
	public GameObject quitMenu;
	public GameObject startFocus;

	enum menu {Normal, Paused, Options, Quit}
	GameObject focus;

	int menuState;

    private Modal modalPanel;

    void Awake () {
        modalPanel = Modal.Instance();
    }


	void Start(){
		menuState = (int)menu.Normal;
		pauseMenu.SetActive(false);
		optionsMenu.SetActive(false);
		quitMenu.SetActive(false);
		focus = startFocus;
		EventSystem.current.SetSelectedGameObject(focus);
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

	public void Options(GameObject highlight){
		EventSystem.current.SetSelectedGameObject(highlight);
		menuState = (int)menu.Options;
		optionsMenu.SetActive(true);
	}

	public void CancelOptions(){
		EventSystem.current.SetSelectedGameObject(focus);
		menuState = (int)menu.Paused;
		optionsMenu.SetActive(false);
	}

	public void CancelOptions(GameObject highlight){
		menuState = (int)menu.Paused;
		EventSystem.current.SetSelectedGameObject(highlight);		
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
	public void Confirm(GameObject FocusHere){
		menuState = (int)menu.Quit;
		quitMenu.SetActive(true);
	}

	public void Restart(){
		AudioManager.Instance.StopSound();
		Time.timeScale = 1;
        MainController.SwitchScene(Scenes.StartScene);
	}

	public void Quit(){
		Application.Quit();
	}


///Modal Window Functions
    public void ConfirmQuit (GameObject returnFocusHere) {
        ModalPanelDetails modalPanelDetails = new ModalPanelDetails {message = "Are you sure?"};
        modalPanelDetails.button1Details = new EventButtonDetails {buttonTitle = "Quit", action = Quit};
        modalPanelDetails.button2Details = new EventButtonDetails {buttonTitle = "Cancel", action = CancelConfirmQuit};

        modalPanel.NewChoice (modalPanelDetails);

        focus = returnFocusHere;
    }

	public void CancelConfirmQuit(){
		menuState = (int)menu.Paused;
		EventSystem.current.SetSelectedGameObject(focus);

//		quitMenu.SetActive(false);
	}

    public void ConfirmRestart (GameObject returnFocusHere) {
        ModalPanelDetails modalPanelDetails = new ModalPanelDetails {message = "Are you sure?"};
        modalPanelDetails.button1Details = new EventButtonDetails {buttonTitle = "Main Menu", action = Restart};
        modalPanelDetails.button2Details = new EventButtonDetails {buttonTitle = "Cancel", action = CancelConfirmRestart};

        modalPanel.NewChoice (modalPanelDetails);

        focus = returnFocusHere;
    }

	public void CancelConfirmRestart(){
		menuState = (int)menu.Paused;
		EventSystem.current.SetSelectedGameObject(focus);

//		quitMenu.SetActive(false);
	}



}