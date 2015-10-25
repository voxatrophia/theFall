using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RockPlatform : MonoBehaviour {

	//Creates array of all child gameobjects
	//randomly disables one of the child gameobjects
	//may also disable a second child gameobject (depending on secondGapRate)

	[Range(0,1)]
	public float secondGapRate = 0.1f; 	//Chance of a second gap in the platform
	private int disabledAlready;
	private List<GameObject> children;

	void Awake(){
		children = new List<GameObject>();
	}

	void OnEnable(){
		children.Clear();
		//Re-enable all child transforms (blocks in platform)
		//add them to children list
		foreach(Transform child in transform){
			children.Add(child.gameObject);
			child.gameObject.SetActive(true);
		}

		//Disable one of the blocks randomly
		//remove from children list
		disabledAlready = Random.Range(0, children.Count);
		children[disabledAlready].SetActive(false);
		children.RemoveAt(disabledAlready);

		//Possibly disable a second block
		//only pull from remaining children
		if(Random.value < secondGapRate){
			int secondGap = Random.Range(0, children.Count);
			children[secondGap].SetActive(false);
		}
	}
}