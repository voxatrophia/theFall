using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class EventTest : MonoBehaviour {

	private UnityAction someListener;

	void Awake(){
		someListener = new UnityAction(SomeFunction);
	}

	void OnEnable(){
		EventManager.StartListening("test", someListener);
	}

	void OnDisable(){
		EventManager.StopListening("test", someListener);
	}

	void SomeFunction(){
		Debug.Log("Some Function was called");
	}
}
