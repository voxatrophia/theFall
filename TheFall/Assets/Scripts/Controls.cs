using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Controls : Singleton<Controls> {

    public KeyCode jump { get; private set; }
    public KeyCode pause { get; private set; }
    public KeyCode useItem { get; private set; }
    public KeyCode back { get; private set; }

    private KeyCode[] _defaultBindings = new KeyCode[] { KeyCode.Space, KeyCode.Escape, KeyCode.LeftShift, KeyCode.Backspace};

    void Awake() {
        SetDefaults();
    }

    public void SetDefaults() {
        jump = _defaultBindings[0];
        pause = _defaultBindings[1];
        useItem = _defaultBindings[2];
        back = _defaultBindings[3];
    }

    public bool SetKey(actions action, KeyCode key) {

        if (!KeyFree(key)) {
            return false;
        }

        switch (action) {
            case actions.Jump:
                jump = key;
                break;
            case actions.Pause:
                pause = key;
                break;
            case actions.UseItem:
                useItem = key;
                break;
            case actions.Back:
                back = key;
                break;
            default:
                Debug.Log("Unknown Action");
                return false;
        }
        return true;
    }

    bool KeyFree(KeyCode key) {
        if (key == jump) {
            return false;
        }
        if (key == pause) {
            return false;
        }
        if (key == useItem) {
            return false;
        }
        if (key == back) {
            return false;
        }
        return true;
    }
}