using UnityEngine;
using System.Collections;

public class Apple : MonoBehaviour {

	public void HealthUp(){
		Debug.Log("Health Up");
		Health.AddHealth();
	}

}
