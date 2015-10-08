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
    public AudioClip deathSound;
    public AudioClip deathSound2;

    private bool invulnerable = false;

    AudioSource audioSrc;
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
        audioSrc = GetComponent<AudioSource>();
    }

	public static int CheckHealth(){
		return i.health;
	}

     void OnTriggerEnter2D(Collider2D other){
		if(!invulnerable){
	    	if(other.gameObject.tag == "Enemy") {
                audioSrc.Play();
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
        audioSrc.clip = deathSound2;
        audioSrc.Play();
        audioSrc.PlayOneShot(deathSound);

        yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(1.05f));
        anim.SetBool("Dead",true);
        yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(3.5f));
        EventManager.TriggerEvent("DeathFade");
        yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(1.5f));
        Time.timeScale = 1;
        MainController.SwitchScene("GameOver");

    }
}