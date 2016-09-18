using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.Collections.Generic;

public class Menu : MonoBehaviour {
    protected LinkedList<GameObject> selected = new LinkedList<GameObject>();
    protected LinkedList<GameObject> menus = new LinkedList<GameObject>();
    private Modal modalPanel;

    void Awake() {
        modalPanel = Modal.Instance();
    }

    public void OpenMenu(GameObject menu) {
        menu.SetActive(true);
        menus.AddLast(menu);
    }

    public void FocusHereNext(GameObject focus) {
        EventSystem.current.SetSelectedGameObject(focus);
    }

    //Called on every button that opens a menu
    //Sets clicked button to be the last selected
    //This is where focus will return when menu is closed
    public void ReturnFocusHere(GameObject returnFocus) {
        selected.AddLast(returnFocus);
    }

    //Closes the last opened menu
    //Returns focus to the last selected button
    public void CloseOpenMenu() {
        if (menus.Count != 0) {
            menus.Last.Value.SetActive(false);
            menus.RemoveLast();
        }

        if (selected.Count != 0) {
            EventSystem.current.SetSelectedGameObject(selected.Last.Value);
            selected.RemoveLast();
        }
    }

    void Update() {
        if (Input.GetButtonDown("Cancel")) {
            if (menus.Count != 0) {
                CloseOpenMenu();
            }
        }
    }

    //Just a shorthand funciton for the standard confirmation dialog
    //Pass in the function to call on confirm
    protected void StandardModal(UnityAction act) {
        menus.AddLast(modalPanel.modalPanelObject);

        ModalPanelDetails modalPanelDetails = new ModalPanelDetails { message = "Are you sure?" };
        modalPanelDetails.button1Details = new EventButtonDetails { buttonTitle = "Yes", action = act };
        modalPanelDetails.button2Details = new EventButtonDetails { buttonTitle = "Cancel", action = CloseOpenMenu };

        modalPanel.NewChoice(modalPanelDetails);
    }

    protected void Announcement(UnityAction act) {
        menus.AddLast(modalPanel.modalPanelObject);

        ModalPanelDetails modalPanelDetails = new ModalPanelDetails { message = "Press a key" };
        modalPanelDetails.button1Details = new EventButtonDetails { buttonTitle = "Yes", action = act };

        modalPanel.NewChoice(modalPanelDetails);
    }

    //public void ConfirmClearScore() {
    //    menuState = (int)menu.OptionsModal;

    //    ModalPanelDetails modalPanelDetails = new ModalPanelDetails { message = "This doesn't work yet." };
    //    //Lambda Function to call function and also close menu
    //    modalPanelDetails.button1Details = new EventButtonDetails { buttonTitle = "OK", action = () => { ClearScore(); CancelClearScore(); } };
    //    modalPanelDetails.button2Details = new EventButtonDetails { buttonTitle = "Cancel", action = CancelClearScore };

    //    modalPanel.NewChoice(modalPanelDetails);
    //}

}