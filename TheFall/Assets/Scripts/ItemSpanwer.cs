using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(MultiObjectPooler))]
public class ItemSpanwer : MonoBehaviour
{
    public float spawnMinTime = 3f;
    public float spawnMaxTime = 5f;

    Dictionary<string, float> items;

    Animator anim;
    MultiObjectPooler itemList;
    string itemToSpawn;

    void Start()
    {
        itemList = GetComponent<MultiObjectPooler>();
        SetUpItemList();
        StartCoroutine(SpawnRandomItem());
    }

    void OnDisable()
    {
        //        Debug.Log(itemToSpawn);
    }

    void SetUpItemList() {
        items = new Dictionary<string, float>();
        foreach (string itemType in itemList.GetObjectTypes())
        {
            items[itemType] = 0.25f;
        }
        items["NA"] = 0.25f;
    }

    void SpawnItem() {
        //Set up list (already done in start method, only needed once)
        //get tension measurements (tension y, health, tension x)
        //Assign new probabilities based on tension
        //spawn item of correct type (or none at all)
        if (Health.CheckHealth() < 2) {
            items["Apple"] += 0.5f;
        }
        itemToSpawn = ChooseItem();
//        Debug.Log(itemToSpawn);
        if (itemToSpawn == "NA") {
            return;
        }
        GameObject item = itemList.GetPooledObject(itemToSpawn);
        if (item != null)
        {
            item.transform.position = new Vector3(Random.Range(-10, 10), transform.position.y, transform.position.z);
            item.transform.rotation = Quaternion.identity;
            item.SetActive(true);

        }
    }

    string ChooseItem()
    {
        float total = 0;

        foreach (KeyValuePair<string, float> elem in items)
        {
            // do something with entry.Value or entry.Key
            total += elem.Value;
        }

        float randomPoint = Random.value * total;

        foreach (KeyValuePair<string, float> elem in items)
        {
            if (randomPoint < elem.Value)
            {
                return elem.Key;
            }
            else
            {
                randomPoint -= elem.Value;
            }
        }
        return "NA";
    }

    IEnumerator SpawnRandomItem()
    {
        while (true)
        {
            yield return Yielders.Get(Random.Range(spawnMinTime, spawnMaxTime));
            SpawnItem();
            /*
            GameObject item = itemList.GetPooledObjectOfRandomType();
			if(item != null){
				item.transform.position = new Vector3(Random.Range(-10,10), transform.position.y, transform.position.z);
				item.transform.rotation = Quaternion.identity;
				item.SetActive(true);
			}
           */
        }
    }
}