using UnityEngine;
using System.Collections;

public class NewTest : MonoBehaviour {
    void Update() {
        System.Array values = System.Enum.GetValues(typeof(KeyCode));
        foreach (KeyCode code in values) {
            if (Input.GetKeyDown(code)) { print(System.Enum.GetName(typeof(KeyCode), code)); }
        }
    }
}
