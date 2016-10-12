using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class ButtonFocus : MonoBehaviour {

    public GameObject[] objects;
    GameObject lastselect;

    bool focused;

    void Start() {
        StartCoroutine(CheckFocusAgain());
        lastselect = new GameObject();
    }

    void CheckFocus() {
        foreach (GameObject go in objects) {
            if (go.activeInHierarchy) {
                EventSystem.current.SetSelectedGameObject(go);
                focused = true;
                lastselect = go;
                break;
            }
        }
    }

    void Update() {
        if (EventSystem.current.currentSelectedGameObject == null) {
            EventSystem.current.SetSelectedGameObject(lastselect);
        }
        else {
            if (lastselect.activeInHierarchy) {
                lastselect = EventSystem.current.currentSelectedGameObject;
            }
            else {
                focused = false;
                StartCoroutine(CheckFocusAgain());
            }
        }
    }

    IEnumerator CheckFocusAgain() {
        while (!focused) {
            yield return new WaitForEndOfFrame();
            CheckFocus();
        }
    }

}