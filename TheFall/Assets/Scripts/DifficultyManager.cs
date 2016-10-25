using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Difficulty {
    public Dictionary<int, Dictionary<string, float>> levels;

    //Store min and max of attack interval
    //used with random.range to get how often to attack
    public Vector2[] attackTimeRange = new Vector2[7];

    public void Initialize() {

        levels = new Dictionary<int, Dictionary<string, float>>();
        levels[1] = new Dictionary<string, float>();
        levels[1]["NA"] = 0.5f;
        levels[1]["GreenSpell"] = 0.5f;
        attackTimeRange[1] = new Vector2(7f, 9f);

        levels[2] = new Dictionary<string, float>();
        levels[2]["NA"] = 0.25f;
        levels[2]["GreenSpell"] = 0.5f;
        levels[2]["OrangeSpell"] = 0.25f;
        attackTimeRange[2] = new Vector2(6f, 8f);

        levels[3] = new Dictionary<string, float>();
        levels[3]["NA"] = 0.25f;
        levels[3]["GreenSpell"] = 0.5f;
        levels[3]["OrangeSpell"] = 0.5f;
        attackTimeRange[3] = new Vector2(5f, 7f);

        levels[4] = new Dictionary<string, float>();
        levels[4]["NA"] = 0.5f;
        levels[4]["GreenSpell"] = 0.5f;
        levels[4]["OrangeSpell"] = 0.25f;
        levels[4]["YellowSpell"] = 0.25f;
        attackTimeRange[4] = new Vector2(4f, 6f);

        levels[5] = new Dictionary<string, float>();
        levels[5]["NA"] = 0.25f;
        levels[5]["GreenSpell"] = 0.5f;
        levels[5]["OrangeSpell"] = 0.5f;
        levels[5]["YellowSpell"] = 0.25f;
        attackTimeRange[5] = new Vector2(3f, 5f);

        levels[6] = new Dictionary<string, float>();
        levels[6]["NA"] = 0.1f;
        levels[6]["GreenSpell"] = 0.25f;
        levels[6]["OrangeSpell"] = 0.5f;
        levels[6]["YellowSpell"] = 0.5f;
        attackTimeRange[6] = new Vector2(2f, 4f);
    }
}

public class DifficultyManager : Singleton<DifficultyManager> {
    Difficulty diff;
    public int attackLevel = 1;
    public float increaseInterval = 30f;
    public int maxAttack = 6;

    void Awake() {
        diff = new Difficulty();
        diff.Initialize();
    }

    void Start () {
        //Difficulty only starts increasing once the tutorial is over
        //if (!TutorialManager.Instance.inTutorial) {
        //    StartCoroutine(IncreaseDifficulty());
        //}

        StartCoroutine(IncreaseDifficulty());

    }

    //void OnEnable() {
    //    //Called from TutorialManager
    //    EventManager.StartListening(TutorialEvents.Done, StartAttack);
    //}

    //void OnDisable() {
    //    EventManager.StopListening(TutorialEvents.Done, StartAttack);
    //}

    //void StartAttack() {
    //    StartCoroutine(IncreaseDifficulty());
    //}

    public Dictionary<string, float> GetAttackList() {
        return diff.levels[attackLevel];
    }

    public float GetAttackInterval() {
        float min = diff.attackTimeRange[attackLevel].x;
        float max = diff.attackTimeRange[attackLevel].y;
        return Random.Range(min, max);
    }

    IEnumerator IncreaseDifficulty() {
        while (attackLevel < maxAttack) {
            yield return Yielders.Get(increaseInterval);
            attackLevel += 1;
        }
    }
}
