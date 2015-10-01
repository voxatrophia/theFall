using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour {
	//Singleton class
	public static ItemManager i;
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

	void Start(){
		changeAlpha(0);
	}

	public GameObject GetItem(){
		if(item != null){
			return item;
		}
		else{
			return null;
		}
	}

	public void SetItem(GameObject obj){
		SpriteRenderer spr = obj.GetComponent<SpriteRenderer>();
		image.sprite = spr.sprite;
		changeAlpha(255);

		item = obj;
	}

	public void UsedItem(){
		image.sprite = null;
		item = null;
		changeAlpha(0);
	}

	void changeAlpha(int alpha){
		Color c = image.color;
        c.a = alpha;
        image.color = c;
	}
}