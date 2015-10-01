using UnityEngine;
using System.Collections;

public class CameraSwitch : MonoBehaviour {

	public GameObject mainCamera;
	public GameObject secondaryCamera;
	Camera cam;

	void Start(){
		if(secondaryCamera != null){
			secondaryCamera.SetActive(false);
		}
		cam = secondaryCamera.GetComponent<Camera>();
	}

	void OnEnable(){
		EventManager.StartListening("Death", SwitchCamera);
	}

	void OnDisable(){
		EventManager.StopListening("Death", SwitchCamera);
	}

    IEnumerator Zoom() {
        float lerpTransitionTime = 2f;
        float lerpStartTime = Time.realtimeSinceStartup;
        float lerpEndTime = lerpStartTime + lerpTransitionTime;

        while (lerpEndTime >= Time.realtimeSinceStartup)
        {
	        cam.orthographicSize = Mathf.Lerp(8, 2, (Time.realtimeSinceStartup - lerpStartTime)/lerpTransitionTime);
            yield return null;
        }
	}

	void SwitchCamera(){
		if(mainCamera.activeSelf){
			mainCamera.SetActive(false);
			secondaryCamera.SetActive(true);
			StartCoroutine("Zoom");
		}
		else{
			mainCamera.SetActive(true);
			secondaryCamera.SetActive(false);
		}
	}
}
