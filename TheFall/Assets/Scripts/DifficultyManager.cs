using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Difficulty {
    public Dictionary<int, Dictionary<string, float>> levels;

    public void Initialize() {
        levels = new Dictionary<int, Dictionary<string, float>>();
        levels[1] = new Dictionary<string, float>();
        levels[1]["NA"] = 0.5f;
        levels[1]["GreenSpell"] = 0.5f;

        levels[2] = new Dictionary<string, float>();
        levels[2]["NA"] = 0.25f;
        levels[2]["GreenSpell"] = 0.5f;
        levels[2]["OrangeSpell"] = 0.25f;

        levels[3] = new Dictionary<string, float>();
        levels[3]["NA"] = 0.25f;
        levels[3]["GreenSpell"] = 0.25f;
        levels[3]["OrangeSpell"] = 0.25f;
        levels[3]["YellowSpell"] = 0.25f;
    }
}

public class DifficultyManager : Singleton<DifficultyManager> {
    Difficulty diff;
    public int level = 1;
    public float levelIncrease = 10f;

    void Start () {
        diff = new Difficulty();
        diff.Initialize();
        StartCoroutine(IncreaseDifficulty());
    }

    public int GetDifficultyLevel() {
        return level;
    }

    public Dictionary<string, float> GetAttackList() {
        return diff.levels[level];
    }

    IEnumerator IncreaseDifficulty() {
        while (level < 3) {
            yield return Yielders.Get(levelIncrease);
            level += 1;
        }
    }
}
