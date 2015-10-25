using UnityEngine;
using System.Collections;

public class MessTest : MonoBehaviour {

	void OnEnable(){
		Messenger.AddListener("Test", Test);
	}

	void OnDisable(){
		Messenger.RemoveListener("Test", Test);
	}

	void Test(){
		Debug.Log("Test Message Received");
	}
}
