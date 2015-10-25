using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFlash : MonoBehaviour {

	public Image flashImg;

	float duration = 1f;
	float smoothness = 0.02f;
	bool isFlashing = false;

	public void FlashScreen(Color img){
		if(!isFlashing){
			StartCoroutine(FlashColor(img));
		}
	}

	IEnumerator FlashColor(Color flashColor) {
		isFlashing = true;
	    float progress = 0; //This float will serve as the 3rd parameter of the lerp function.
	    float increment = smoothness/duration; //The amount of change to apply.

	    while(progress < 1) {
	    	flashImg.color = Color.Lerp(flashColor, Color.clear, progress);
	        progress += increment;
	        yield return Yielders.Get(smoothness);
	    }
	    isFlashing = false;
	    return true;
	 }
}