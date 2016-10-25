using UnityEngine; 
using System.Collections;

public class ScreenShake : MonoBehaviour { 

	public bool shakePosition;
	public bool shakeRotation;

	public float shakeIntensity = 1f;
	public float shakeDecay = 0.02f;

	private Vector3 OriginalPos;
	private Quaternion OriginalRot;

	private bool isShakeRunning = false;

    void Start() {
        if (Application.platform == RuntimePlatform.WindowsEditor) {
            shakeDecay = 0.025f;
        }
        OriginalPos = transform.position;
        OriginalRot = transform.rotation;
    }

    public void DoShake() {
        StartCoroutine(ProcessShake());
    }

    public void DoShake(float intensity)
	{
		shakeIntensity = intensity;
		//OriginalPos = transform.position;
		//OriginalRot = transform.rotation;

		StartCoroutine (ProcessShake());
	}

	IEnumerator ProcessShake()
	{
		if (!isShakeRunning) {
			isShakeRunning = true;
			float currentShakeIntensity = shakeIntensity;

			while (currentShakeIntensity > 0) {
				if (shakePosition) {
					transform.position = OriginalPos + Random.insideUnitSphere * currentShakeIntensity;
				}
				if (shakeRotation) {
					transform.rotation = new Quaternion (OriginalRot.x + Random.Range (-currentShakeIntensity, currentShakeIntensity) * .2f,
						OriginalRot.y + Random.Range (-currentShakeIntensity, currentShakeIntensity) * .2f,
						OriginalRot.z + Random.Range (-currentShakeIntensity, currentShakeIntensity) * .2f,
						OriginalRot.w + Random.Range (-currentShakeIntensity, currentShakeIntensity) * .2f);
				}
				currentShakeIntensity -= shakeDecay;
				yield return null;
			}
            transform.rotation = OriginalRot;
            transform.position = OriginalPos;
			isShakeRunning = false;
		}
	}
}