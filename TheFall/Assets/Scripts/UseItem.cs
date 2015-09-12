using UnityEngine;
using System.Collections;

public class UseItem : MonoBehaviour {

	GameObject item;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Fire1") ){
			if(ItemManager.i != null){
				item = ItemManager.i.GetItem();				
			}
			if(item != null){
				switch(item.name){
					case "Bomb":
						Bomb useBomb = item.GetComponent<Bomb>();
						useBomb.Explode();
						break;
					case "Apple":
						Apple useApple = item.GetComponent<Apple>();
						useApple.Health();
						break;
					default:
						break;

				}
				//Destroy item (or recycle for object pooling)
				ItemManager.i.UsedItem();
			}
			else {
				Debug.Log("No Item");
			}
		}
	}
}
