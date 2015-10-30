using UnityEngine;
using System.Collections;

public class QuitUI : MonoBehaviour {

	public GameObject quitMenu;
	bool open = false;

	void Start () {
		quitMenu.SetActive(false);
	}

	void Update(){
		if(Input.GetButtonDown("Cancel")){
			if(open){
				CancelConfirm();
			}
		}
	}
	
	public void ConfirmQuit() {
		open = true;
		quitMenu.SetActive(true);
	}

	public void CancelConfirm() {
		open = false;
		quitMenu.SetActive(false);
	}

	public void Quit() {
		Application.Quit();
	}
}