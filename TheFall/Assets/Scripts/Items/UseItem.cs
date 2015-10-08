using UnityEngine;
using System.Collections;
//using UnityStandardAssets.CrossPlatformInput;

public class UseItem : MonoBehaviour {

	GameObject item;
	AudioSource audioSrc;

	public AudioClip bombSE;
	public AudioClip appleSE;
	public AudioClip watchSE;

	void Start(){
		audioSrc = GetComponent<AudioSource>();
	}

	void Update () {
//		if(CrossPlatformInputManager.GetButtonDown("Submit")){
		if(Input.GetButtonDown("Fire1")){
			//Make sure ItemManager is active
			if(ItemManager.i != null){
				item = ItemManager.i.GetItem();
			}
			if(item != null){
				switch(item.tag){
					case "Bomb":
						Bomb useBomb = item.GetComponent<Bomb>();
						useBomb.Explode();
						audioSrc.PlayOneShot(bombSE);
						break;
					case "Apple":
						Apple useApple = item.GetComponent<Apple>();
						useApple.HealthUp();
						audioSrc.PlayOneShot(appleSE);
						break;
					case "Stopwatch":
						audioSrc.PlayOneShot(watchSE);
						Stopwatch useStopwatch = item.GetComponent<Stopwatch>();
						useStopwatch.StopTime();
						break;
					default:
						break;

				}
				//Remove item from image
				ItemManager.i.UsedItem();
			}
			else {
				Debug.Log("No Item");
			}
		}
	}
}
