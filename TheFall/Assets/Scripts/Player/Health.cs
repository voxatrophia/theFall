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
        //Called from Apple item
        EventManager.StartListening(Events.HealthPickup, AddHealth);
    }

    void OnDisable(){
        EventManager.StopListening(Events.HealthPickup, AddHealth);
    }

    void Awake(){
    	if(i == null){
			i = this;
    	}
        else{
            Destroy(this.gameObject);
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
	    	if(other.CompareTag("Enemy")) {
                audioSrc.Play();
    			TakeDamage();

                //If hit near top of screen, too chaotic, move screen down
                if(transform.position.y > 6){
                    EventManager.TriggerEvent(Events.MoveBackwards);
                }
	    	}
 
    	}
    }

	void AddHealth(){
    	i.health += 1;
        flash.FlashSprite(healthColor);
        EventManager.TriggerEvent(Events.AddHealth);
	}

    public void TakeDamage() {
        if(!invulnerable){
            health -= 1;
			EventManager.TriggerEvent(Events.Damage);
            StartCoroutine(JustHurt());
            if(health <= 0){
                StartCoroutine(Die());
            }
        }
    }

    IEnumerator JustHurt() {
        invulnerable = true;
        flash.FlashSprite(damageColor);
        yield return Yielders.Get(invulTime);
        invulnerable = false;
     }

    IEnumerator Die(){
        Time.timeScale = 0;
        audioSrc.clip = deathSound2;
        audioSrc.Play();
        EventManager.TriggerEvent(Events.Death);
        yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(0.8f));
        anim.SetBool(PlayerAnim.Dead,true);
        yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(3f));
        audioSrc.Stop();
        EventManager.TriggerEvent(Events.DeathFade);
        yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(1.5f));
        Time.timeScale = 1;
        MainController.SwitchScene(Scenes.GameOver);
    }

    //Called by animation event
    public void PlayDeathSound(){
        audioSrc.PlayOneShot(deathSound);    	
    }
}