using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(MultiObjectPooler))]
public class ItemSpanwer : MonoBehaviour {
    public float spawnMinTime = 3f;
    public float spawnMaxTime = 5f;

    Dictionary<string, float> items;
    MultiObjectPooler itemList;
    string itemToSpawn;

    int tutorialItemIndex = 0;
    List<string> tutorialItems;
    bool inTutorial;

    void Start() {
        itemList = GetComponent<MultiObjectPooler>();
        inTutorial = TensionManager.Instance.tutorial;

        SetUpItemList();
        tutorialItems = itemList.GetObjectTypes();
        StartCoroutine(SpawnItems());
    }

    void OnEnable() {
        EventManager.StartListening("TutorialItemUsed", ChangeItem);
    }

    void OnDisable() {
        EventManager.StopListening("TutorialItemUsed", ChangeItem);
    }

    void TutorialSpawn() {
        itemToSpawn = tutorialItems[tutorialItemIndex];
        SpawnItem(itemToSpawn);
    }

    void ChangeItem() {
        if (tutorialItemIndex == tutorialItems.Count - 1) {
            inTutorial = false;
        }
        else {
            tutorialItemIndex += 1;
        }
    }

    void SetUpItemList() {
        items = new Dictionary<string, float>();
        foreach (string itemType in itemList.GetObjectTypes()) {
            items[itemType] = 0.25f;
        }
        items["NA"] = 0.25f;
    }

    void SelectItem() {
        AdjustItemProb();
        itemToSpawn = ChooseItem();
        SpawnItem(itemToSpawn);
    }

    void AdjustItemProb() {
        //Set up list (already done in start method, only needed once)
        //get tension measurements (tension y, health, tension x)
        //Assign new probabilities based on tension
        //spawn item of correct type (or none at all)
        if (Health.CheckHealth() < 2) {
            items["Apple"] += 0.5f;
        }
    }

    string ChooseItem() {
        float total = 0;

        foreach (KeyValuePair<string, float> elem in items) {
            // do something with entry.Value or entry.Key
            total += elem.Value;
        }

        float randomPoint = Random.value * total;

        foreach (KeyValuePair<string, float> elem in items) {
            if (randomPoint < elem.Value) {
                return elem.Key;
            }
            else {
                randomPoint -= elem.Value;
            }
        }
        return "NA";
    }

    void SpawnItem(string spawningItem) {
        if (itemToSpawn == "NA") {
            return;
        }
        GameObject item = itemList.GetPooledObject(itemToSpawn);
        if (item != null) {
            item.transform.position = new Vector3(Random.Range(-10, 10), transform.position.y, transform.position.z);
            item.transform.rotation = Quaternion.identity;
            item.SetActive(true);
        }
    }

    IEnumerator SpawnItems() {
        while (true) {
            yield return Yielders.Get(Random.Range(spawnMinTime, spawnMaxTime));
            if (inTutorial) {
                TutorialSpawn();
            }
            else {
                SelectItem();
            }
        }
    }
}