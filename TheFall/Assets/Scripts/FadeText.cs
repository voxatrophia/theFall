using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeText : MonoBehaviour {

    public Text TextToFade;

    public float fadeInTime = 0f;
    public float fadeOutTime = 3f;
    public float displayTime = 10f;

    void Start() {
        Initialize(TextToFade);
    }

    void Initialize(Text text) {
        //Must set CanvasRenderer to 0 get get CrossFadeAlpha to work
        //But it also makes the text invisible to start which is necessary
        text.canvasRenderer.SetAlpha(0.0f);
        text.gameObject.SetActive(false);
    }

    public void Fade(string t) {
        TextToFade.text = t;
        StartCoroutine(DisplayText(TextToFade));
    }

    IEnumerator DisplayText(Text display) {
        display.gameObject.SetActive(true);
        display.CrossFadeAlpha(1f, fadeInTime, true);
        yield return Yielders.Get(displayTime);
        display.CrossFadeAlpha(0, fadeOutTime, true);
    }
}
