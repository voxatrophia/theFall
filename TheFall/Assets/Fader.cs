using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Fader : MonoBehaviour {
//	public static Fader i;

	public Image fadeImg;

	float duration = 1f;
	float smoothness = 0.02f;
	bool isFading = false;

	void Start(){
		FadeToClear();
	}

	void OnEnable(){
		EventManager.StartListening("DeathFade", FadeToBlack);
	}

	void OnDisable(){
		EventManager.StopListening("DeathFade", FadeToBlack);
	}

	public void FadeToClear(){
		if(!isFading){
			StartCoroutine(FadeScreen(Color.clear, Color.black));
		}
	}

	public void FadeToBlack(){
		if(!isFading){
			StartCoroutine(FadeScreen(Color.black, Color.clear));
		}
	}

	// public void FadeTo(Color toColor, Color fromColor = default(Color)){
	// 	if(!isFading){
	// 		StartCoroutine(FadeScreen(toColor,fromColor));
	// 	}
	// }

	IEnumerator FadeScreen(Color toColor, Color fromColor) {
//        yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(4));
		//Bug here
		//fadeImg is destroyed somewhere or gets set to null
		//This prevents raising the error and the fade still works
		//would be best to get to the underlying problem though
		if(fadeImg != null){
			isFading = true;
		    float progress = 0; //This float will serve as the 3rd parameter of the lerp function.
		    float increment = smoothness/duration; //The amount of change to apply.

		    while(progress < 1) {
		    	fadeImg.color = Color.Lerp(fromColor, toColor, progress);
		        progress += increment;
		        yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(smoothness));
		    }
		    isFading = false;
		}
	    yield return null;
	 }
}
