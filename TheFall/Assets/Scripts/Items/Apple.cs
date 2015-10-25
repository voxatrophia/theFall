using UnityEngine;
using System.Collections;

public class Apple : MonoBehaviour {

	//Function called from UseItem script attached to player
	public void HealthUp(){
		EventManager.TriggerEvent(Events.HealthPickup);
	}

}
