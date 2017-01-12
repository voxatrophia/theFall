using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour {
	//Singleton class
	public static ItemManager i;
	private Image image;		//UI Image
	private GameObject item;	//current item
    bool firstItem;

    public AudioClip itemSE;

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

        //Tutorial Trigger for first text
        //If inTutorial, then check for first item
        //firstItem = (TutorialManager.Instance.inTutorial) ? true : false;

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

        AudioManager.Instance.PlaySoundEffect(itemSE);

        if (firstItem) {
            EventManager.TriggerEvent(TutorialEvents.FirstItem);
            firstItem = false;
        }
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