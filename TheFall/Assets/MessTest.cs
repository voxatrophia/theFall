using UnityEngine;
using System.Collections;

public class MessTest : MonoBehaviour {

	void OnEnable(){
		Messenger.AddListener("Test", Test);
	}

	void OnDisable(){
		Messenger.RemoveListener("Test", Test);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Test(){
		
	}
}
