using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class EventManager : MonoBehaviour {

	private Dictionary <string, UnityEvent> eventDictionary;

	private static EventManager eventManager;

	//Singleton, make sure there is always an instance
	public static EventManager instance	{
		get	{
			if(!eventManager){
				eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

				if(!eventManager){
					Debug.LogError("Need active EventManager");
				}
				else{
					eventManager.Init();
				}
			}
			return eventManager;
		}
	}

	void Init(){
		if(eventDictionary == null){
			eventDictionary = new Dictionary<string, UnityEvent>();
		}
	}

	public static void StartListening(string eventName, UnityAction listener){
		UnityEvent thisEvent = null;
		//check to see if event is already in dictionary
		if(instance.eventDictionary.TryGetValue(eventName, out thisEvent)){
			thisEvent.AddListener(listener);
		}
		//if not, add it to the dictionary
		else{
			thisEvent = new UnityEvent();
			thisEvent.AddListener(listener);
			instance.eventDictionary.Add(eventName, thisEvent);
		}
	}

	public static void StopListening(string eventName, UnityAction listener){
		//Catch for NullReferenceException
		if(eventManager == null) return;

		UnityEvent thisEvent = null;

		if(instance.eventDictionary.TryGetValue(eventName, out thisEvent)){
			thisEvent.RemoveListener(listener);
		}
	}

	public static void TriggerEvent(string eventName){
		UnityEvent thisEvent = null;
		if(instance.eventDictionary.TryGetValue(eventName, out thisEvent)){
			thisEvent.Invoke();
		}
	}
}
