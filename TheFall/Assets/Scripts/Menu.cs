using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class Menu : MonoBehaviour {
    protected LinkedList<GameObject> selected = new LinkedList<GameObject>();
    protected LinkedList<GameObject> menus = new LinkedList<GameObject>();
    private Modal modalPanel;

    protected virtual void OnAwake() { }

    void Awake() {
        modalPanel = Modal.Instance();
        OnAwake();
    }

    public void OpenMenu(GameObject menu) {
        menu.SetActive(true);
        menus.AddLast(menu);
    }

    IEnumerator SetFocus(GameObject focus) {
        //Setting to null first seems to fix a bug
        //Bug: Doesn't initially trigger the highlight color
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(focus);
    }

    public void FocusHereNext(GameObject focus) {
        StartCoroutine(SetFocus(focus));
        //EventSystem.current.SetSelectedGameObject(null);
        //EventSystem.current.SetSelectedGameObject(focus);
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
            StartCoroutine(SetFocus(selected.Last.Value));
            //EventSystem.current.SetSelectedGameObject(selected.Last.Value);
            selected.RemoveLast();
        }
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(Controls.Instance.back)) {
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

    //protected void Announcement(UnityAction act) {
    //    menus.AddLast(modalPanel.modalPanelObject);

    //    ModalPanelDetails modalPanelDetails = new ModalPanelDetails { message = "Press a key" };
    //    modalPanelDetails.button1Details = new EventButtonDetails { buttonTitle = "Yes", action = act };

    //    modalPanel.NewChoice(modalPanelDetails);
    //}

    protected IEnumerator Announcement(string msg) {
        //Add the last button, because it will be called again
        //selected.AddLast(selected.Last.Value);

        yield return Yielders.Get(0.5f);
        menus.AddLast(modalPanel.modalPanelObject);

        ModalPanelDetails modalPanelDetails = new ModalPanelDetails { message = msg };
        modalPanelDetails.button1Details = new EventButtonDetails { buttonTitle = "OK", action = CloseOpenMenu };

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