using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum actions { Jump, UseItem, Pause, Back }

public class Controls : Singleton<Controls> {
    public KeyCode jump { get; private set; }
    public KeyCode pause { get; private set; }
    public KeyCode useItem { get; private set; }
    public KeyCode back { get; private set; }

    private Dictionary<string, KeyCode> _default = new Dictionary<string, KeyCode>()
    {
        { Control.Jump, KeyCode.Space },
        { Control.UseItem, KeyCode.LeftShift },
        { Control.Pause, KeyCode.Escape },
        { Control.Back, KeyCode.Backspace }
    };

    void Awake() {
        Load();
    }

    public void SetDefaults() {
        jump = _default[Control.Jump];
        useItem = _default[Control.UseItem];
        pause = _default[Control.Pause];
        back = _default[Control.Back];
    }

    public void Load() {
        jump = (PlayerPrefs.HasKey(Control.Jump)) ? (KeyCode)PlayerPrefs.GetInt(Control.Jump) : _default[Control.Jump];
        useItem = (PlayerPrefs.HasKey(Control.UseItem)) ? (KeyCode)PlayerPrefs.GetInt(Control.UseItem) : _default[Control.UseItem];
        pause = (PlayerPrefs.HasKey(Control.Pause)) ? (KeyCode)PlayerPrefs.GetInt(Control.Pause) : _default[Control.Pause];
        back = (PlayerPrefs.HasKey(Control.Back)) ? (KeyCode)PlayerPrefs.GetInt(Control.Back) : _default[Control.Back];
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
                Debug.LogError("Unknown Action");
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

    public void Save() {
        PlayerPrefs.SetInt(Control.Jump, (int)jump);
        PlayerPrefs.SetInt(Control.UseItem, (int)useItem);
        PlayerPrefs.SetInt(Control.Pause, (int)pause);
        PlayerPrefs.SetInt(Control.Back, (int)back);
    }
}