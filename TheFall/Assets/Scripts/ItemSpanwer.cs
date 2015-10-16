using UnityEngine;
using System.Collections;

[RequireComponent (typeof(MultiObjectPooler))]
public class ItemSpanwer : MonoBehaviour {
	public float spawnMinTime = 3f;
	public float spawnMaxTime = 5f;
	public Transform itemParent;

	Animator anim;
	MultiObjectPooler itemList;

	void Start() {
		itemList = GetComponent<MultiObjectPooler>();

		StartCoroutine("SpawnItem");
	}

	IEnumerator SpawnItem(){
		while(true){
			yield return new WaitForSeconds(Random.Range(spawnMinTime, spawnMaxTime));
			GameObject item = itemList.GetPooledObjectOfRandomType();

			if(item != null){
				item.transform.position = new Vector3(Random.Range(-10,10), transform.position.y, transform.position.z);
				item.transform.rotation = Quaternion.identity;
				item.transform.parent = itemParent;
				item.SetActive(true);
			}
		}
	}
}
