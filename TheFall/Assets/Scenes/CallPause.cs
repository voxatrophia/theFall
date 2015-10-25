using UnityEngine;
using System.Collections;

public class CallPause : MonoBehaviour {

	bool paused = false;

	void Update () {
		if(!paused) {
			if(Input.GetKeyDown(KeyCode.Escape)) {
				paused = true;
				Application.LoadLevelAdditive(5);
			}
		}
	}
}
