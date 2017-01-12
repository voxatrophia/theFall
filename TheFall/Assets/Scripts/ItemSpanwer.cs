using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(MultiObjectPooler))]
public class ItemSpanwer : MonoBehaviour {
    public float spawnMinTime = 3f;
    public float spawnMaxTime = 5f;

    //weighted list of items
    Dictionary<string, float> items;
    //list of items with default weights
    Dictionary<string, float> originalItems;
    //Pool of items
    MultiObjectPooler itemList;
    //Which item type to spawn
    string itemToSpawn;


    //int tutorialItemIndex = 0;
    //List<string> tutorialItems;
    //bool inTutorial;

    //void OnEnable() {
    //    //Triggered in UseItem
    //    EventManager.StartListening(TutorialEvents.ItemUsed, ChangeItem);
    //}
    //void OnDisable() {
    //    EventManager.StopListening(TutorialEvents.ItemUsed, ChangeItem);
    //}

    void Start() {
        itemList = GetComponent<MultiObjectPooler>();
        //inTutorial = TutorialManager.Instance.inTutorial;

        SetUpItemList();
        //tutorialItems = itemList.GetObjectTypes();
        StartCoroutine(SpawnItems());
    }

    //Sets all items to equal weights
    void SetUpItemList() {
        items = new Dictionary<string, float>();
        foreach (string itemType in itemList.GetObjectTypes()) {
            items[itemType] = 0.25f;
        }
        items["NA"] = 0.25f;
        originalItems = items;
    }

    //Loop to spawn items
    IEnumerator SpawnItems() {
        while (true) {
            yield return Yielders.Get(Random.Range(spawnMinTime, spawnMaxTime));
            SelectItem();
            //if (inTutorial) {
            //    TutorialSpawn();
            //}
            //else {
            //    SelectItem();
            //}
        }
    }

    //void TutorialSpawn() {
    //    itemToSpawn = tutorialItems[tutorialItemIndex];
    //    SpawnItem(itemToSpawn);
    //}

    ////Called by event trigger
    ////Increments item to spawn
    //void ChangeItem() {
    //    if (inTutorial) {
    //        if (tutorialItemIndex == tutorialItems.Count - 1) {
    //            inTutorial = false;
    //            EventManager.TriggerEvent(TutorialEvents.ItemStageDone);
    //        }
    //        else {
    //            tutorialItemIndex += 1;
    //        }
    //    }
    //}

    void SpawnItem(string spawningItem) {
        if (spawningItem == "NA") {
            return;
        }
        GameObject item = itemList.GetPooledObject(spawningItem);
        if (item != null) {
            item.transform.position = new Vector3(Random.Range(-10, 10), transform.position.y, transform.position.z);
            item.transform.rotation = Quaternion.identity;
            item.SetActive(true);
        }
    }

    void SelectItem() {
        AdjustItemProb();
        itemToSpawn = ChooseItem();
        SpawnItem(itemToSpawn);
        //reset probabilities
        items = originalItems;
    }

    void AdjustItemProb() {
        //Set up list (already done in start method, only needed once)
        //get tension measurements (tension y, health, tension x)
        //Assign new probabilities based on tension

        //increase probability of stopwatch as tension increases
        items["Stopwatch"] += TensionManager.Instance.tensionY / 100;

        //if health low, double probability of apple
        if (Health.CheckHealth() < 2) {
            items["Apple"] = items["Apple"] * 2;
        }
    }

    //Given probability list, choose one at random but taking their weights into account
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


}