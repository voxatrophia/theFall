using UnityEngine;
using System.Collections;

public class Loading : MonoBehaviour {

    public GameObject icon;

    public void Start() {
        icon.SetActive(false);
    }

    public void ShowIcon() {
        icon.SetActive(true);
    }
}
