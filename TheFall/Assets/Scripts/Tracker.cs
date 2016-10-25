using UnityEngine;
using UnityEngine.Analytics;
using System.Collections;
using System.Collections.Generic;

public class Tracker : Singleton<Tracker> {

    Dictionary<string, object> items;
    Dictionary<string, object> gameTime;
    Dictionary<string, object> health;
    Dictionary<string, object> difficulty;
    Dictionary<string, object> sessions;
    Dictionary<string, object> enemies;

    string played = "Played_";

    void Start() {
        items = new Dictionary<string, object>();
        items.Add(Tags.Apple, 0);
        items.Add(Tags.Bomb, 0);
        items.Add(Tags.Stopwatch, 0);
        items.Add(Tags.Orb, 0);

        gameTime = new Dictionary<string, object>();
        gameTime.Add("GameTime", 0);

        health = new Dictionary<string, object>();
        health.Add("MaxHealth", 3);

        difficulty = new Dictionary<string, object>();
        difficulty.Add("DifficultyLevel", 0);

        sessions = new Dictionary<string, object>();
        sessions.Add("Played", "");

        enemies = new Dictionary<string, object>();
        enemies.Add("Enemy", "");
    }

    /* Called Where Needed */
    //Count items used
    //called in UseItem
    public void TrackItemUsed(string item) {
        items[item] = (int)items[item] + 1;
    }

    //max health gained
    //called in PlayerHealth
    public void TrackHealth(int h) {
        if (h > (int)health["MaxHealth"]) {
            health["MaxHealth"] = (int)health["MaxHealth"] + 1;
        }
    }

    //Track what killed player
    public void TrackEnemy(string enemy) {
        enemies["Enemy"] = enemy;
    }


    /* Called When Player Dies */

    //Count survival time
    public void TrackTime() {
        gameTime["GameTime"] = Time.timeSinceLevelLoad;
    }

    //count number of tries
    //reset at gamestart
    public void CountTry() {
        if (PlayerPrefs.HasKey("Played")) {
            played = played + PlayerPrefs.GetInt("Played");
            PlayerPrefs.SetInt("Played", PlayerPrefs.GetInt("Played") + 1);
        }
        else {
            played = played + 1;
            PlayerPrefs.SetInt("Played", 2);
        }

        sessions["Played"] = played;
    }

    //Track difficulty
    public void TrackDifficulty() {
        difficulty["DifficultyLevel"] = DifficultyManager.Instance.attackLevel;
    }


    //Called in PlayerHealth after player death
    public void TrackEvents() {
        TrackTime();
        CountTry();
        TrackDifficulty();

        SendEvents();
    }

    //Send all custom events
    public void SendEvents() {
        //Item Custom Event
        Debug.Log(Analytics.CustomEvent("ItemsUsed", items));
        //Debug.Log(items[Tags.Apple]);
        //Debug.Log(items[Tags.Bomb]);
        //Debug.Log(items[Tags.Orb]);
        //Debug.Log(items[Tags.Stopwatch]);

        //Time Event
        Debug.Log(Analytics.CustomEvent("GameTime", gameTime));
        //Debug.Log(gameTime["GameTime"]);

        //Track difficulty
        Debug.Log(Analytics.CustomEvent("Difficulty", difficulty));
        //Debug.Log(difficulty["DifficultyLevel"]);

        //Health Event
        Debug.Log(Analytics.CustomEvent("Health", health));
        //Debug.Log(health["MaxHealth"]);

        //Count Tries
        Debug.Log(Analytics.CustomEvent("Sessions", sessions));
        //Debug.Log(played);

        //Enemy
        Debug.Log(Analytics.CustomEvent("KilledBy", enemies));
        //Debug.Log(enemies["Enemy"]);
    }
}
