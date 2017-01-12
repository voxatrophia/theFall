using UnityEngine;
using System.Collections;

public class ScaleObject : MonoBehaviour {

    void Start() {
        Vector3 newSize = transform.localScale;
        newSize.x = AspectRatioController.Instance.Scale;
        transform.localScale = newSize;
    }
}
