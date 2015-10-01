using UnityEngine;
using System.Collections;

public class Apple : MonoBehaviour {

	//Called from UseItem script attached to player
	public void HealthUp(){
		EventManager.TriggerEvent("HealthPickup");
	}

}
