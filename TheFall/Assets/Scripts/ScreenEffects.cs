using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScreenEffects : MonoBehaviour {

	CameraShake shake;
	public Image dmgimg;
	public Color flashColour;
	public float flashSpeed = 0.5f;

	float duration = 1f;
	float smoothness = 0.02f;

	void Start(){
		shake = GetComponent<CameraShake> ();
	}

	void OnEnable(){
		EventManager.StartListening("Damage", ScreenEffect);
	}

	void OnDisable(){
		EventManager.StopListening("Damage", ScreenEffect);
	}

	void ScreenEffect(){
		shake.DoShake(0.3f);
		StartCoroutine("ChangeColor");
	}
 
	IEnumerator ChangeColor() {
	    float progress = 0; //This float will serve as the 3rd parameter of the lerp function.
	    float increment = smoothness/duration; //The amount of change to apply.
	    while(progress < 1) {
	    	dmgimg.color = Color.Lerp(flashColour, Color.clear, progress);
	        progress += increment;
	        yield return new WaitForSeconds(smoothness);
	    }
	    return true;
	 }


}
