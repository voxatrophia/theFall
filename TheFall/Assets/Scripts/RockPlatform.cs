using UnityEngine;
using System.Collections;

public class RockPlatform : MonoBehaviour {

	[Range(0,1)]
	public float secondGapRate = 0.1f;
	private int childCount;
	public GameObject[] children;

	void Awake(){
		childCount = transform.childCount;
		children = new GameObject[childCount];
	}

	void OnEnable(){
		//Re-enable all child transforms (blocks in platform)
		int i = 0;
		foreach(Transform child in transform){
			children[i] = child.gameObject;
			child.gameObject.SetActive(true);
			i++;
		}

		//Disable one of the blocks randomly
		int disabledAlready = Random.Range(0, childCount);
		children[disabledAlready].SetActive(false);

		//Possibly disable a second block
		//Fix this!
		if(Random.value < secondGapRate){
			int secondGap = Random.Range(0, childCount);
			if(secondGap == disabledAlready){
				children[Random.Range(0,childCount)].SetActive(false);
			}
		}
	}
}