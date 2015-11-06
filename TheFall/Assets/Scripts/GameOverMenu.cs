using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class GameOverMenu : MonoBehaviour {

	enum menu {Normal, Paused, Options, Modal, OptionsModal}

	LinkedList<GameObject> highlight = new LinkedList<GameObject>();
	int menuState;

    private Modal modalPanel;

    void Awake () {
        modalPanel = Modal.Instance();
    }

	void Start(){
		menuState = (int)menu.Normal;
	}

	void Update(){
		//Handles state of Escape Key
		if(Input.GetButtonDown("Cancel")){
			switch(menuState){
				case (int)menu.Normal:
					break;
				case (int)menu.Modal:
					CancelModal();
					break;
			}
		}
	}

	public void ReturnFocus(GameObject returnFocus){
		highlight.AddLast(returnFocus);
	}

	public void CloseMenu(){
		EventSystem.current.SetSelectedGameObject(highlight.Last.Value);
		highlight.RemoveLast();
	}

//Modal Window Functions

	//All Cancel buttons for modal windows use the same functionality
	public void CancelModal(){
		menuState = (int)menu.Normal;
		CloseMenu();
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
}