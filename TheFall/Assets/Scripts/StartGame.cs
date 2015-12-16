using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;


public class StartGame : MonoBehaviour {

	AudioSource audioSrc;
	public AudioClip hoverSound;
	public AudioClip clickSound;

	public GameObject optionsMenu;

    private Modal modalPanel;
    private GameObject highlight;

    void Awake () {
        modalPanel = Modal.Instance();
    }

	void Start(){
		highlight = optionsMenu;
		audioSrc = GetComponent<AudioSource>();
		optionsMenu.SetActive(false);
	}

    public void Announce () {
        ModalPanelDetails modalPanelDetails = new ModalPanelDetails ();
        modalPanelDetails.message = "This is an announcement!\nIf you don't like it, shove off!";
        modalPanelDetails.button1Details = new EventButtonDetails ();
        modalPanelDetails.button1Details.buttonTitle = "Ok";
        modalPanelDetails.button1Details.action = CancelFunction;

        modalPanel.NewChoice (modalPanelDetails);
    }

    public void ConfirmQuit () {
        ModalPanelDetails modalPanelDetails = new ModalPanelDetails {message = "Are you sure?"};
        modalPanelDetails.button1Details = new EventButtonDetails {buttonTitle = "Yes", action = Quit};
        modalPanelDetails.button2Details = new EventButtonDetails {buttonTitle = "Cancel", action = CancelFunction};

        modalPanel.NewChoice (modalPanelDetails);
    }

    public void ConfirmQuit (GameObject returnFocusHere) {
        ModalPanelDetails modalPanelDetails = new ModalPanelDetails {message = "Are you sure?"};
        modalPanelDetails.button1Details = new EventButtonDetails {buttonTitle = "Yes", action = Quit};
        modalPanelDetails.button2Details = new EventButtonDetails {buttonTitle = "Cancel", action = CancelFunction};
        highlight = returnFocusHere;

        modalPanel.NewChoice (modalPanelDetails);
    }


    public void ClearHighScore(GameObject returnFocusHere){
        ModalPanelDetails modalPanelDetails = new ModalPanelDetails {message = "Are you sure you want to delete the high score?"};
        modalPanelDetails.button1Details = new EventButtonDetails {buttonTitle = "Yes", action = ScoreClear};
        modalPanelDetails.button2Details = new EventButtonDetails {buttonTitle = "Cancel", action = CancelFunction};
        modalPanel.NewChoice (modalPanelDetails);
        highlight = returnFocusHere;
    }

    public void ScoreClear(){
    	ShowAnnouncement("Your score was cleared");
    	Debug.Log("Score not actually cleared");
    }

    //Doesn't work
    public void ShowAnnouncement(string announcement){
        ModalPanelDetails modalPanelDetails = new ModalPanelDetails {message = announcement};
        modalPanelDetails.button1Details = new EventButtonDetails {buttonTitle = "OK", action = CancelFunction};
        modalPanel.NewChoice (modalPanelDetails);
    }

    void CancelFunction(){
		EventSystem.current.SetSelectedGameObject(highlight);
    	//closes modal window
    }

	public void Hover(){
		audioSrc.PlayOneShot(hoverSound);
	}

	public void PlayGame(){
		PlayerPrefs.SetInt("GameMode", Modes.Story);
		StartCoroutine(Play());
	}

	public void ArcadeMode(){
		PlayerPrefs.SetInt("GameMode", Modes.Arcade);
		StartCoroutine(Play());
	}

	IEnumerator Play(){
		audioSrc.PlayOneShot(clickSound);
		yield return Yielders.Get(0.5f);
		MainController.SwitchScene(Scenes.Main);
	}

	// public void Confirm(){
	// 	quitMenu.SetActive(true);
	// }

	// public void CancelConfirm(){
	// 	quitMenu.SetActive(false);
	// }

	public void Quit(){
		Debug.Log("quit");
		AppHelper.Quit();
	}

	public void Options(){
		optionsMenu.SetActive(true);
	}

	public void Options(GameObject focus){
		highlight = focus;
		optionsMenu.SetActive(true);
		EventSystem.current.SetSelectedGameObject(focus);
	}


	public void CancelOptions(){
		optionsMenu.SetActive(false);
	}

	public void CancelOptions(GameObject focus){
		highlight = focus;
		optionsMenu.SetActive(false);
		EventSystem.current.SetSelectedGameObject(highlight);
	}

	public void ClearScore(){
		audioSrc.PlayOneShot(clickSound);
		PlayerPrefs.SetInt("HighScore", 0);
	}

}
