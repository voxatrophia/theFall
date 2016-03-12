using UnityEngine;
using System.Collections;

public class TensionManager : Singleton<TensionManager> {
    //Track tension in the game
    //Tension builds slowly over time (when above 0 on Y axis)
    //Using items reduces tension (drastically)
    //Track: Y, X, Health, attack avoidance streak

    public Transform player;
    public float tensionY = 0;

    void OnEnable() {
        EventManager.StartListening(Events.StopMoving, ZeroTension);
    }

    void OnDisable() {
        EventManager.StopListening(Events.StopMoving, ZeroTension);
    }

    public void ZeroTension() {
        tensionY = 0;
    }

    public void ReleaseTension(float amt) {
        tensionY -= amt;
    }

    public float CheckTension() {
        return tensionY;
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