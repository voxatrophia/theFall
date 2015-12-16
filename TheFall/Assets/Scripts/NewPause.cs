using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class NewPause : MonoBehaviour {

	public GameObject pauseMenu;
	public GameObject optionsMenu;
	public GameObject startFocus;

	enum menu {Normal, Paused, Options, Modal, OptionsModal}

	LinkedList<GameObject> highlight = new LinkedList<GameObject>();
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
		focus = startFocus;
	}

	void Update(){
		//Handles state of Escape Key
		if(Input.GetButtonDown("Cancel")){
			switch(menuState){
				case (int)menu.Normal:
					Pause();
					break;
				case (int)menu.Paused:
					Resume();
					break;
				case (int)menu.Options:
					CancelOptions();
					break;
				case (int)menu.Modal:
					CancelModal();
					break;
				case (int)menu.OptionsModal:
					CancelClearScore();
					break;
			}
		}
	}

	//All buttons that open menus (modal or not) call this function with their own GO as parameter
	public void ReturnFocusHere(GameObject returnFocus){
		focus = returnFocus;
	}

	public void ReturnFocus(GameObject returnFocus){
		highlight.AddLast(returnFocus);
	}

	public void CloseMenu(){
		EventSystem.current.SetSelectedGameObject(highlight.Last.Value);
		highlight.RemoveLast();
	}

	public void Pause(){
		focus = startFocus;
		Time.timeScale = 0;
		AudioManager.Instance.Pause();
		pauseMenu.SetActive(true);
		menuState = (int)menu.Paused;
		EventSystem.current.SetSelectedGameObject(focus);
	}

	public void Resume(){
		AudioManager.Instance.Play();
		menuState = (int)menu.Normal;
		pauseMenu.SetActive(false);
		Time.timeScale = 1;
	}

	public void OpenOptions(GameObject focusHere){
		menuState = (int)menu.Options;
		optionsMenu.SetActive(true);
		EventSystem.current.SetSelectedGameObject(focusHere);
	}

	public void CancelOptions(){
		CloseMenu();
//		EventSystem.current.SetSelectedGameObject(focus);
		menuState = (int)menu.Paused;
		optionsMenu.SetActive(false);
	}

//Modal Window Functions

	//All Cancel buttons for modal windows use the same functionality
	public void CancelModal(){
		menuState = (int)menu.Paused;
		CloseMenu();
//		EventSystem.current.SetSelectedGameObject(focus);
	}

//QUIT GAME

    public void ConfirmQuit () {
    	menuState = (int)menu.Modal;

        ModalPanelDetails modalPanelDetails = new ModalPanelDetails {message = "Are you sure?"};
        modalPanelDetails.button1Details = new EventButtonDetails {buttonTitle = "Quit", action = Quit};
        modalPanelDetails.button2Details = new EventButtonDetails {buttonTitle = "Cancel", action = CancelModal};

        modalPanel.NewChoice (modalPanelDetails);
    }

	public void Quit(){
		AppHelper.Quit();
	}

//CLEAR SCORE

    public void ConfirmClearScore () {
    	menuState = (int)menu.OptionsModal;

        ModalPanelDetails modalPanelDetails = new ModalPanelDetails {message = "This doesn't work yet."};
        //Lambda Function to call function and also close menu
        modalPanelDetails.button1Details = new EventButtonDetails {buttonTitle = "OK", action = () => { ClearScore(); CancelClearScore(); }};
        modalPanelDetails.button2Details = new EventButtonDetails {buttonTitle = "Cancel", action = CancelClearScore};

        modalPanel.NewChoice (modalPanelDetails);
	}

	public void CancelClearScore(){
		menuState = (int)menu.Options;
//		EventSystem.current.SetSelectedGameObject(focusHere);
		CloseMenu();
	}

	public void ClearScore(){
		Debug.Log(focus);
		Debug.Log("Score Not Cleared");
	}

//RESTART GAME

    public void ConfirmRestart () {
    	menuState = (int)menu.Modal;

        ModalPanelDetails modalPanelDetails = new ModalPanelDetails {message = "Are you sure?"};
        modalPanelDetails.button1Details = new EventButtonDetails {buttonTitle = "Main Menu", action = Restart};
        modalPanelDetails.button2Details = new EventButtonDetails {buttonTitle = "Cancel", action = CancelModal};

        modalPanel.NewChoice (modalPanelDetails);
    }

	public void Restart(){
		AudioManager.Instance.StopSound();
		Time.timeScale = 1;
        MainController.SwitchScene(Scenes.StartScene);
	}
}