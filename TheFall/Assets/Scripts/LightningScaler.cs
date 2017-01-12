using UnityEngine;
using System.Collections;

public class LightningScaler : MonoBehaviour {

    BoxCollider2D box;

    void Start() {
        box = GetComponent<BoxCollider2D>();
        box.size = new Vector2(AspectRatioController.Instance.Ratio * 20, box.size.y);
    }
}