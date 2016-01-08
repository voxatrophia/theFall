using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(MultiObjectPooler))]
public class BossAttack : MonoBehaviour {
    public float attackMinTime = 3f;
    public float attackMaxTime = 5f;

    bool canMove = true;
    GameObject attack;
    Animator anim;
    MultiObjectPooler attackPool;

    Dictionary<string, float> attackProbs;
    string pendingAttack;

    void Start() {
        anim = GetComponent<Animator>();
        attackPool = GetComponent<MultiObjectPooler>();

        if (!TutorialManager.Instance.inTutorial) {
            StartCoroutine(RandomAttack());
        }
    }

    void OnEnable() {
        //Called from Stopwatch item
        EventManager.StartListening(Events.StopMoving, StopMoving);
        //Called from TutorialManager
        EventManager.StartListening("TutorialStage3Start", StartAttack);
    }

    void OnDisable() {
        EventManager.StopListening(Events.StopMoving, StopMoving);
        EventManager.StopListening("TutorialStage3Start", StartAttack);
    }

    //Sets all attacks to be equal chance
    void SetUpAttackList() {
        attackProbs = new Dictionary<string, float>();
        foreach (string attackType in attackPool.GetObjectTypes()) {
            attackProbs[attackType] = 0.25f;
        }
        attackProbs["NA"] = 0.25f;
    }

    void Attack() {
        attackProbs = DifficultyManager.Instance.GetAttackList();
        pendingAttack = ChooseAttack();
        if (pendingAttack == "NA") {
            return;
        }
        else {
            attack = attackPool.GetPooledObject(pendingAttack);
            if (attack != null)
            {
                attack.transform.position = transform.position;
                attack.transform.rotation = Quaternion.identity;
                attack.SetActive(true);
                anim.SetTrigger(BossAnim.Attack);
            }

        }
    }

    //Given probability list, choose one at random but taking their weights into account
    string ChooseAttack() {
        float total = 0;

        foreach (KeyValuePair<string, float> elem in attackProbs)
        {
            // do something with entry.Value or entry.Key
            total += elem.Value;
        }

        float randomPoint = Random.value * total;

        foreach (KeyValuePair<string, float> elem in attackProbs)
        {
            if (randomPoint < elem.Value)
            {
                return elem.Key;
            }
            else
            {
                randomPoint -= elem.Value;
            }
        }
        return "NA";
    }

    //Wrapper for RandomAttack() coroutine
    void StartAttack() {
        StartCoroutine(RandomAttack());
    }

    IEnumerator RandomAttack(){
		while(true){
			yield return Yielders.Get((Random.Range(attackMinTime,attackMaxTime)));
            if (canMove) {
                Attack();
                /*
                    //old method
                    attack = attackPool.GetPooledObjectOfRandomType();
                    if(attack != null){
                        attack.transform.position = transform.position;
                        attack.transform.rotation = Quaternion.identity;
                        attack.SetActive(true);
                        anim.SetTrigger(BossAnim.Attack);
                    }
                */
            }
		}
	}

	void StopMoving(){
		StartCoroutine(StopMovingCoroutine());
	}

	IEnumerator StopMovingCoroutine(){
		canMove = false;
		yield return new WaitForSeconds(2f);
		canMove = true;
	}

}
