using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputHandler : Singleton<InputHandler> {

    FadeText fade;

    //Window where key binding happens
    public GameObject RebindWindow;
    public GameObject RebindMenu;
    //component to cache
    Rebind rebind;

    public Text JumpText;
    public Text PauseText;
    public Text ItemText;
    public Text BackText;

    //Keep track of last button selected
    Button lastButton;

    //Keep track of BindAll steps
    int step = 0;
    bool bindAll = false;

    void Start() {
        fade = GetComponent<FadeText>();
        rebind = RebindWindow.GetComponent<Rebind>();
        UpdateKeys();
    }

    void UpdateKeys() {
        JumpText.text = Controls.Instance.jump.ToString();
        PauseText.text = Controls.Instance.pause.ToString();
        ItemText.text = Controls.Instance.useItem.ToString();
        BackText.text = Controls.Instance.back.ToString();
    }

    IEnumerator BindAllKeysRoutine() {
        rebind.control = actions.Jump;
        RebindWindow.SetActive(true);
        yield return new WaitUntil(() => step == 1);
        rebind.control = actions.Pause;
        RebindWindow.SetActive(true);
        yield return new WaitUntil(() => step == 2);
        rebind.control = actions.UseItem;
        RebindWindow.SetActive(true);
        yield return new WaitUntil(() => step == 3);
        rebind.control = actions.Back;
        RebindWindow.SetActive(true);
        bindAll = false;
        step = 0;
        yield return null;
    }

    public void CaptureFocus(Button btn) {
        btn.interactable = false;
        lastButton = btn;
    }

    public void BindKey(string action) {
        System.Array values = System.Enum.GetValues(typeof(actions));
        foreach (actions code in values) {
            if (action == code.ToString()) {
                rebind.control = code;
                RebindWindow.SetActive(true);
                break;
            }
        }
    }

    public void BindAll() {
        bindAll = true;
        StartCoroutine(BindAllKeysRoutine());
    }

    public void KeyInUse() {
        fade.Fade("That key is already in use...");
        bindAll = false;
        step = 0;
    }

    public void ResetDefault(Button btn) {
        Controls.Instance.SetDefaults();
        UpdateKeys();
    }

    public void ReturnControlHelper() {
        UpdateKeys();
        if (bindAll || step > 3) {
            step++;
        }
        else {
            lastButton.interactable = true;
            EventSystem.current.SetSelectedGameObject(lastButton.gameObject);
        }
    }

    //public void ReturnFocus() {
    //    Debug.Log("?");
    //    lastButton.interactable = true;
    //    EventSystem.current.SetSelectedGameObject(lastButton.gameObject);
    //    //EventSystem.current.sendNavigationEvents = true;
    //}

    public void Save() {
        Controls.Instance.Save();
        RebindMenu.SetActive(false);
    }
}