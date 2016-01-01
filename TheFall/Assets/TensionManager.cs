using UnityEngine;
using System.Collections;

public class TensionManager : Singleton<TensionManager> {
    //Track tension in the game
    //Tension builds slowly over time (when above 0 on Y axis)
    //Using items reduces tension (drastically)
    //Track: Y, X, Health, attack avoidance streak

    public Transform player;
    public float tensionY = 0;

    public bool tutorial;

    void Awake () {
        tutorial = true;
	}

    void OnDisable() {
        Debug.Log("Tension Y:" + tensionY);
    }

    void Tutorial() {
        //Remove boss
        //Remove item spawning
        //Remove energy charging
        //Remove score tracking
    }

        void Update() {
            if (player.position.y > 0) {
                tensionY += Time.deltaTime;
            }
        }
    }