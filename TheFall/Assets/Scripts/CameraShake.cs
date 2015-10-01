using UnityEngine;
using System.Collections;
public class CameraShake : MonoBehaviour {

	public bool Shaking;
	private float ShakeDecay;
	private float ShakeIntensity;
	private Vector3 OriginalPos;
	private Quaternion OriginalRot;

	void Start()
	{
		Shaking = false;
	}

	void Update ()
	{
		if(ShakeIntensity > 0)
		{
			//Original shake, causes problem for 2D game with fixed boundries
			//Solved by remooving the boundries from the camera
			transform.position = OriginalPos + Random.insideUnitSphere * ShakeIntensity;
			transform.rotation = new Quaternion(OriginalRot.x + Random.Range(-ShakeIntensity, ShakeIntensity)*.2f,
			                                    OriginalRot.y + Random.Range(-ShakeIntensity, ShakeIntensity)*.2f,
			                                    OriginalRot.z + Random.Range(-ShakeIntensity, ShakeIntensity)*.2f,
			                                    OriginalRot.w + Random.Range(-ShakeIntensity, ShakeIntensity)*.2f);
			ShakeIntensity -= ShakeDecay;
		}
		else if (Shaking)
		{
			Shaking = false;
			transform.position = OriginalPos;
			transform.rotation = OriginalRot;
		}

	}

	public void DoShake(float intensity)
	{
		OriginalPos = transform.position;
		OriginalRot = transform.rotation;
		ShakeIntensity = intensity;
		ShakeDecay = 0.02f;
		Shaking = true;
	}
}
