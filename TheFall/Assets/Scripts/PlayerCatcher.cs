using UnityEngine;
using System.Collections;

public class PlayerCatcher : MonoBehaviour {

    void Start() {
        //Debug.Log(AspectRatioController.Instance.Scale);
        transform.localScale = new Vector3(AspectRatioController.Instance.Ratio * 20, transform.localScale.y, transform.localScale.z);
    }

    void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag(Tags.Player)){
	        EventManager.TriggerEvent(Events.MoveBackwards);
		}
	}

}
