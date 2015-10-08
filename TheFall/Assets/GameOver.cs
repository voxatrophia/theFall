using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {

	public void Restart(){
		MainController.SwitchScene("Main");
	}

}
