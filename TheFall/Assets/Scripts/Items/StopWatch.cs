using UnityEngine;
using System.Collections;

public class Stopwatch : MonoBehaviour {

	//Called from UseItem script attached to player
	public void StopTime(){
        EventManager.TriggerEvent("StopMoving");
	}
}
