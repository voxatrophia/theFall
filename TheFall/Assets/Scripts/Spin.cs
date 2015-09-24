using UnityEngine;
using System.Collections;

public class Spin : MonoBehaviour {
	
	public float rotateSpeed;

	void Update(){
		transform.Rotate (0,0, rotateSpeed * Time.deltaTime);
	}
}
