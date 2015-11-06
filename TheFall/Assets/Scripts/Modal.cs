using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.EventSystems;

public class EventButtonDetails {
    public string buttonTitle;
    public Sprite buttonBackground;  // Not implemented
    public UnityAction action;
}

public class ModalPanelDetails {
    public string title; // Not implemented
    public string message;
    public Sprite iconImage; // Not implemented
    public Sprite panelBackgroundImage; // Not implemented
    public EventButtonDetails button1Details;
    public EventButtonDetails button2Details;
}

public class Modal : MonoBehaviour {
    
    public Text message;
    public Image iconImage;
    public Button button1;
    public Button button2;

    public Text button1Text;
    public Text button2Text;
    
    public GameObject modalPanelObject;
    
    private static Modal modal;

    public static Modal Instance () {
        if (!modal) {
            modal = FindObjectOfType(typeof (Modal)) as Modal;
            if (!modal)
                Debug.LogError ("There needs to be one active Modal script on a GameObject in your scene.");
        }
        
        return modal;
    }

    void Start(){
    	modalPanelObject.SetActive(false);
    }

    void Update(){
        if(Input.GetButtonDown("Cancel")){
            ClosePanel();
        }
    }

    public void NewChoice (ModalPanelDetails details){
        modalPanelObject.SetActive (true);

        this.iconImage.gameObject.SetActive(false);
        button1.gameObject.SetActive(false);
        button2.gameObject.SetActive(false);

        this.message.text = details.message;

        if (details.iconImage) {
            this.iconImage.sprite = details.iconImage;
            this.iconImage.gameObject.SetActive(true);
        }

        button1.onClick.RemoveAllListeners();
        button1.onClick.AddListener (details.button1Details.action);
        button1.onClick.AddListener (ClosePanel);
        button1Text.text = details.button1Details.buttonTitle;
        button1.gameObject.SetActive(true);
        //Changes focus to this button
   		EventSystem.current.SetSelectedGameObject(button1.gameObject);

        if (details.button2Details != null) {
            button2.onClick.RemoveAllListeners();
            button2.onClick.AddListener (details.button2Details.action);
            button2.onClick.AddListener (ClosePanel);
            button2Text.text = details.button2Details.buttonTitle;
            button2.gameObject.SetActive(true);
        }
    }
    
    void ClosePanel () {
        modalPanelObject.SetActive (false); 
    }
}