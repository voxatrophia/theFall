using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System.Collections.Generic;

public class Health : MonoBehaviour {
	public static Health i;

	public int health = 3;
	public float invulTime = 1f;
    public Color damageColor;
    public Color healthColor;

    private bool invulnerable = false;

    Animator anim;
    FlashSpriteColor flash;

    void OnEnable(){
        EventManager.StartListening("HealthPickup", AddHealth);
    }

    void OnDisable(){
        EventManager.StopListening("HealthPickup", AddHealth);
    }

    void Awake(){
    	if(i == null){
			i = this;
    	}
        anim = GetComponent<Animator>();
        flash = GetComponent<FlashSpriteColor>();
    }

	public static int CheckHealth(){
		return i.health;
	}

     void OnTriggerEnter2D(Collider2D other){
		if(!invulnerable){
	    	if(other.gameObject.tag == "Enemy") {
    			TakeDamage();
                //If hit near top of screen, too chaotic, move screen down
                if(transform.position.y > 6){
                    EventManager.TriggerEvent("MoveBackwards");                
                }
	    	}
 
    	}
    }

	void AddHealth(){
    	i.health += 1;
        flash.FlashSprite(healthColor);
        EventManager.TriggerEvent("AddHealth");
	}

    public void TakeDamage() {
        if(!invulnerable){
            health -= 1;
			EventManager.TriggerEvent("Damage");
            StartCoroutine(JustHurt());
            if(health <= 0){
//            	Death();
                StartCoroutine("Die");
            }
        }
    }
     
    IEnumerator JustHurt() {
        invulnerable = true;
        flash.FlashSprite(damageColor);
        yield return new WaitForSeconds(invulTime);
        invulnerable = false;
     }

    IEnumerator Die(){
        Time.timeScale = 0;
        EventManager.TriggerEvent("Death");
        yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(1f));
        anim.SetBool("Dead",true);
    }

    // IEnumerator Transition(Color flashColor) {
    //     float endInvul = invulTime + Time.time;
    //     while(Time.time < endInvul){
    //         float lerpTransitionTime = 0.1f;
    //         float lerpStartTime = Time.time;
    //         float lerpEndTime = lerpStartTime + lerpTransitionTime;

    //         while (lerpEndTime >= Time.time)
    //         {
    //             spr.color = Color.Lerp(origColor, flashColor, (Time.time - lerpStartTime)/lerpTransitionTime);
    //             yield return null;
    //         }

    //         yield return new WaitForSeconds(0.1f);

    //         lerpStartTime = Time.time;
    //         lerpEndTime = lerpStartTime + lerpTransitionTime;

    //         while (lerpEndTime >= Time.time)
    //         {
    //             spr.color = Color.Lerp(flashColor, origColor, (Time.time - lerpStartTime)/lerpTransitionTime);
    //             yield return null;
    //         }
    //     }
    //     yield return null;
    // }

    // void MovePlayerToCenter(){
    //     Vector3 dest = new Vector3(0, 0, transform.position.z);
    //     Vector3 vel = Vector3.zero;
    //     float damp = 0.2f;

    //     transform.position = Vector3.SmoothDamp(transform.position, dest, ref vel, damp);
    // }

}