using UnityEngine;
using System.Collections;

public class LoadTutorial : MonoBehaviour {
    public string nextScene;

    public void NextScene() {
        MainController.SwitchScene(nextScene);
    }
}
