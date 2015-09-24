using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Light))]
public class LightFlash : MonoBehaviour {
	public float flashLength = 0.05f;
	public float flashRangeMin = 3f;
	public float flashRangeMax = 5f;

	private Light lt;

	void Start () {
		lt = GetComponent<Light>();
		lt.enabled = false;
		StartCoroutine("FlashRepeat");
	}
	
	void Update () {
	}

	public void Flash(){
		StopAllCoroutines();
		StartCoroutine("FlashOnce");
		StartCoroutine("FlashRepeat");
	}

	// void OnGUI(){
	// 	if(GUI.Button(new Rect(Mathf.Abs(transform.position.x)*20,Mathf.Abs(transform.position.y)*20,50,25), "Flash")){
	// 		Flash();
	// 	}
	// }

	IEnumerator FlashOnce() {
		lt.enabled = true;
		yield return new WaitForSeconds(flashLength);
		lt.enabled = false;
		yield break;
	}

	IEnumerator FlashRepeat() {
		while(true){
			yield return new WaitForSeconds(Random.Range(flashRangeMin,flashRangeMax));
			lt.enabled = true;
			yield return new WaitForSeconds(flashLength);
			lt.enabled = false;
		}
	}
}
