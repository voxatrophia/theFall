using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class UseItem : MonoBehaviour {

	GameObject item;
	
	void Update () {
		if(CrossPlatformInputManager.GetButtonDown("Submit")){
			//Make sure ItemManager is active
			if(ItemManager.i != null){
				item = ItemManager.i.GetItem();
			}
			if(item != null){
				switch(item.tag){
					case "Bomb":
						Bomb useBomb = item.GetComponent<Bomb>();
						useBomb.Explode();
						break;
					case "Apple":
						Apple useApple = item.GetComponent<Apple>();
						useApple.HealthUp();
						break;
					case "Stopwatch":
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
