using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour {
	//Singleton class
	public static ItemManager i;
	public GameObject[] items; 	//Array of prefab items
	private Image image;		//UI Image
	private GameObject item;	//current item

	void Awake(){
		if(i == null){
			i = this;
		} else if(i != this){
			Destroy(gameObject);
		}
		image = GetComponent<Image>();
	}

	public GameObject GetItem(){
		//return item.name;
		if(item != null){
			return item;
		}
		else{
			return null;
		}
	}

	public void SetItem(GameObject obj){
		if(checkItem(obj)){
			SpriteRenderer spr = obj.GetComponent<SpriteRenderer>();
			image.sprite = spr.sprite;
			item = obj;
		}
	}

	//checks to make sure item is in the array
	bool checkItem(GameObject obj){
		foreach(GameObject itm in items){
			if(obj.name == itm.name){
				return true;
			}
		}
		return false;
	}

	public void UsedItem(){
		image.sprite = null;
		item = null;
	}
}
