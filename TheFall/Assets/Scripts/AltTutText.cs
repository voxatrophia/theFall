using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TutorialText {
    public string text;
    public float duration;
    public Vector2 position;
}

public class AltTutText : MonoBehaviour {

    public Text tutUI;
    TutorialText tutorial;
    bool visible;

    void OnEnable()
    {
        EventManager.StartListening("TutorialFirstItem", ShowItemText);
        EventManager.StartListening("TutorialStage2Start", ShowEnergyChargeText);
        EventManager.StartListening("TutorialEnergyFull", ShowEnergeAttackText);
    }

    void OnDisable()
    {
        EventManager.StopListening("TutorialFirstItem", ShowItemText);
        EventManager.StopListening("TutorialStage2Start", ShowEnergyChargeText);
        EventManager.StopListening("TutorialEnergyFull", ShowEnergeAttackText);
    }

    void Start()
    {
        tutorial = new TutorialText { text = "String", duration = 15f, position = new Vector3(-100f, -75f, 0) };
        tutUI.canvasRenderer.SetAlpha(0.0f);
        visible = false;


    }

    void Fade()
    {
        if (visible)
        {
            tutUI.CrossFadeAlpha(0, 3f, true);
            visible = false;
        }
        else
        {
            tutUI.CrossFadeAlpha(1f, 3f, true);
            visible = true;
        }
    }

    void ShowItemText()
    {
        tutUI.text = tutorial.text;
        tutUI.rectTransform.anchoredPosition = tutorial.position;
        StartCoroutine(ItemText());
    }

    IEnumerator ItemText()
    {
        visible = false;
        Fade();
        yield return Yielders.Get(tutorial.duration);
        Fade();
    }

    void ShowEnergyChargeText()
    {
        tutUI.text = "Charge your energy meter by staying in the blue light";
        tutUI.rectTransform.anchoredPosition = new Vector3(-500, -650, 0);
        StartCoroutine(ItemText());

    }

    void ShowEnergeAttackText()
    {

    }
}
