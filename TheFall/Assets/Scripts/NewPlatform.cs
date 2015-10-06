using UnityEngine;
using System.Collections;
using System;

public class NewPlatform : MonoBehaviour {

	float totalSize;
	public GameObject[] rocks;
	float[] sizes;

	void Start(){
		float ratio = (float)Screen.width / (float)Screen.height;
		totalSize = ratio * Camera.main.orthographicSize * 2;
		Debug.Log(Math.Round(totalSize, 2));
	}

}
