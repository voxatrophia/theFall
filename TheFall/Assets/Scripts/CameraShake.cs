using UnityEngine;
using System.Collections;
public class CameraShake : MonoBehaviour {
//	public static CameraShake i;

	public bool Shaking;
	private float ShakeDecay;
	private float ShakeIntensity;
	private Vector3 OriginalPos;
	private Quaternion OriginalRot;
//	private bool shook = false;


	// void Awake(){
	// 	if(i == null){
	// 		i = this;
	// 	}
	// }

	void Start()
	{
		Shaking = false;
	}

	void Update ()
	{
		if(ShakeIntensity > 0)
		{
			//Original shake, causes problem for 2D game with fixed boundries
			transform.position = OriginalPos + Random.insideUnitSphere * ShakeIntensity;
			transform.rotation = new Quaternion(OriginalRot.x + Random.Range(-ShakeIntensity, ShakeIntensity)*.2f,
			                                    OriginalRot.y + Random.Range(-ShakeIntensity, ShakeIntensity)*.2f,
			                                    OriginalRot.z + Random.Range(-ShakeIntensity, ShakeIntensity)*.2f,
			                                    OriginalRot.w + Random.Range(-ShakeIntensity, ShakeIntensity)*.2f);

			// transform.rotation = new Quaternion(OriginalRot.x + Random.Range(-ShakeIntensity, ShakeIntensity)*.2f,
			//                                     OriginalRot.y,
			//                                     OriginalRot.z,
			//                                     OriginalRot.w);

			ShakeIntensity -= ShakeDecay;
		}
		else if (Shaking)
		{
			Shaking = false;
			transform.position = OriginalPos;
			transform.rotation = OriginalRot;
		}

	}
	// void OnGUI() {
	// 	if (GUI.Button (new Rect (10, 200, 50, 30), "Shake")) {
	// 		DoShake();
	// 		Debug.Log("Shake");
	// 	}
	// }
	public void DoShake(float intensity)
	{
		OriginalPos = transform.position;
		OriginalRot = transform.rotation;
		ShakeIntensity = intensity;
		ShakeDecay = 0.02f;
		Shaking = true;
	}
}
