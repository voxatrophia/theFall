using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MultiObjectPooler : MonoBehaviour {

	public Transform parent;
	//The list of type of objects to pool
	public GameObject[] objectsList;
	//Array to know how many to pool initially
	public int[] pooledAmount;
	//Array to know whether to allow the poll to grow
	public bool[] willGrow;

    public Dictionary<string, List<GameObject>> pooledObjectsList;

	void Awake () {

		pooledObjectsList = new Dictionary<string, List<GameObject>>();

		//Have to make sure all three arrays (Objects, Amount, Grow) are equal
		if((objectsList.Length != pooledAmount.Length) || (objectsList.Length != willGrow.Length)){
			//Throw error
			Debug.LogError("Object List and Pooled Amount arrays need to be equal");
			return;
		}

		//Looping through array of object types
		for(int i = 0; i < objectsList.Length; i++){
			//initialize the list of pooled objects
			List<GameObject> pooledObjects = new List<GameObject>();
			//Add List to dictionary with object name as key
			pooledObjectsList.Add(objectsList[i].name, pooledObjects);

			//now begin creating objects to store in list
			for(int j=0; j < pooledAmount[i]; j++){
				GameObject obj = (GameObject)Instantiate(objectsList[i]);
				if(parent != null){
					obj.transform.parent = parent;
				}
				obj.SetActive(false);
				pooledObjectsList[objectsList[i].name].Add(obj);
			}
		}
	}

	public List<string> GetObjectTypes(){
		if(pooledObjectsList == null) {
			return null;
		}

		List<string> keyList = new List<string>(pooledObjectsList.Keys);
		return keyList;
	}

	public GameObject GetPooledObject(string type) {
		if(pooledObjectsList == null) {
			return null;
		}

		//Pull from existing disabled objects
		for(int i = 0; i < pooledObjectsList[type].Count; i++){
			if(!pooledObjectsList[type][i].activeInHierarchy) {
				return pooledObjectsList[type][i];
			}
		}

		//If out of existing pool, check if allowed to grow
		int poIndex = -1;
		for(int i = 0; i < objectsList.Length; i++) {
			if(objectsList[i].name == type) {
				poIndex = i;
				break;
			}
		}

		if((poIndex >= 0) && (willGrow[poIndex])) {
			GameObject obj = (GameObject)Instantiate(objectsList[poIndex]);
			obj.SetActive(false);
			pooledObjectsList[objectsList[poIndex].name].Add(obj);
			return obj;
		}
		else {
			Debug.Log("Don't grow");
		}

		return null;
	}


	public GameObject GetPooledObjectOfRandomType(){
		if(pooledObjectsList == null) {
			return null;
		}

		List<string> ObjectTypeList = GetObjectTypes();

		return GetPooledObject(ObjectTypeList[Random.Range(0, ObjectTypeList.Count)]);
	}
}