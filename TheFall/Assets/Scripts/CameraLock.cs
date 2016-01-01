using UnityEngine;
using System.Collections;

public class CameraLock : MonoBehaviour {
	float yTop = 7.5f;
	float yBot = -7.5f;
	float xLeft = -14.5f;
	float xRight = 14.5f;
	public Transform Player;

	 void Update(){

        transform.position = new Vector3(Player.position.x, Player.position.y, -10);
//		transform.position = new Vector3(Mathf.Clamp(Player.position.x, xLeft, xRight), Mathf.Clamp(Player.position.y, yBot, yTop), -10);
    }
}
