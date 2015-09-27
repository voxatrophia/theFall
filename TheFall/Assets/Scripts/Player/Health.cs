using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System.Collections.Generic;

public class Health : MonoBehaviour {
	public static Health i;

	public int health = 3;
	public float invulTime = 1f;
    public Color flashColor;

    private bool invulnerable = false;
    private SpriteRenderer spr;
    private Color origColor;

    void Awake(){
    	if(i == null){
			i = this;
    	}
    	spr = GetComponent<SpriteRenderer>();
    	origColor = spr.color;
    }

	public static int CheckHealth(){
		return i.health;
	}

     void OnTriggerEnter2D(Collider2D other){
		if(!invulnerable){
	    	if(other.gameObject.tag == "Enemy") {
    			TakeDamage();
	    	}
	    	// if(other.gameObject.tag == "Health") {
      //           Debug.Log("Health?");
	    	// 	AddHealth();
	    	// }
    	}
    }

	public static void AddHealth(){
    	i.health += 1;
        EventManager.TriggerEvent("AddHealth");
	}

    public void TakeDamage() {
        if(!invulnerable){
            health -= 1;
			EventManager.TriggerEvent("Damage");
            StartCoroutine(JustHurt());
            if(health <= 0){
            	Death();
            }
        }
    }
     
    IEnumerator JustHurt() {
        invulnerable = true;
        StartCoroutine("Transition");

        // spr.color = new Color(1f,1f,1f,Mathf.PingPong(Time.time, 10));
        yield return new WaitForSeconds(invulTime);
        // spr.color = origColor;
        invulnerable = false;
     }

    void Death(){
    	//Debug.Log("Game Over");
    }

    IEnumerator Transition() {
        float endInvul = invulTime + Time.time;
        while(Time.time < endInvul){
            float lerpTransitionTime = 0.1f;
            float lerpStartTime = Time.time;
            float lerpEndTime = lerpStartTime + lerpTransitionTime;

            while (lerpEndTime >= Time.time)
            {
                spr.color = Color.Lerp(origColor, flashColor, (Time.time - lerpStartTime)/lerpTransitionTime);
                yield return null;
            }

            yield return new WaitForSeconds(0.1f);

            lerpStartTime = Time.time;
            lerpEndTime = lerpStartTime + lerpTransitionTime;

            while (lerpEndTime >= Time.time)
            {
                spr.color = Color.Lerp(flashColor, origColor, (Time.time - lerpStartTime)/lerpTransitionTime);
                yield return null;
            }
//            Debug.Log(counter);
        }
        yield return null;
    }
}