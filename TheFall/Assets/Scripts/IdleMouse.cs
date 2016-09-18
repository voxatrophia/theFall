using UnityEngine;
using System.Collections;

public class IdleMouse : MonoBehaviour {

    Vector3 lastMousePosition = Vector3.zero;
    float timeTilHide = 3f;
    float lastTime;
    bool mouseHidden;

    void Update() {
        if (lastMousePosition != Input.mousePosition) {
            lastMousePosition = Input.mousePosition;
            lastTime = Time.time;
            if (mouseHidden) {
                Cursor.visible = true;
                mouseHidden = false;
            }
        }
        else {
            if (Time.time > lastTime + timeTilHide) {
                mouseHidden = true;
                Cursor.visible = false;
            }
        }
    }
}
