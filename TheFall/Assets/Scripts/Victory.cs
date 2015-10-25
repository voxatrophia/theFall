using UnityEngine;
using System.Collections;

public class Victory : MonoBehaviour {

	public GameObject quitMenu;

	void Start(){
		quitMenu.SetActive(false);
	}

	public void Restart(){
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
}
