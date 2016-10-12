using UnityEngine;
using UnityStandardAssets._2D;

public class Orb : MonoBehaviour {
    PlatformerCharacter2D player;

    void OnEnable() {
        player = FindObjectOfType<PlatformerCharacter2D>();
    }

    public void SpeedUp() {
        player.SpeedUp();
    }
}
