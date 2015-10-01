using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour {

	void OnEnable(){
		foreach(Transform child in transform){
			child.gameObject.SetActive(true);
		}
	}

}
