using UnityEngine;
using System.Collections;

public class BossHealth : MonoBehaviour {

	public Color damageColor;
	public AudioClip damageSound;
	public AudioClip nearDeathTheme;
	public AudioClip victoryTheme;

    bool firstAttack;

	[Range(0, 10)]
	public int bossHealth = 10;

	AudioSource audioSrc;
	FlashSpriteColor flash;

	void Start () {
		flash = GetComponent<FlashSpriteColor>();
		audioSrc = GetComponent<AudioSource>();

        //if in Tutorial, keep track of first attack
        firstAttack = (TutorialManager.Instance.inTutorial) ? true : false;
    }

	void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag(Tags.PlayerAttack)){
			EventManager.TriggerEvent(Events.BossHit);
            if (firstAttack) {
                firstAttack = false;
                EventManager.TriggerEvent(TutorialEvents.AttackStageDone);
            }
			switch(LevelManager.Instance.GetMode()){
				case Modes.Arcade:
					ArcadeDamage();
					break;
				case Modes.Story:
					StoryDamage();
					break;
			}

			//damage effects
			audioSrc.PlayOneShot(damageSound);
			flash.FlashSprite(damageColor);
		}
	}

	void ArcadeDamage(){
		EventManager.TriggerEvent(Events.BossNearDeath);
	}

	void StoryDamage(){
		bossHealth -= 1;

		if(bossHealth == 1){
			AudioManager.Instance.SwitchMusic(nearDeathTheme);
			flash.ChangeSpriteColor(damageColor);
			EventManager.TriggerEvent(Events.BossNearDeath);
		}
		else if(bossHealth == 0){
			StartCoroutine(BossDeath());
		}		
	}

	IEnumerator BossDeath(){
		AudioManager.Instance.SwitchMusic(victoryTheme);
		EventManager.TriggerEvent(Events.Victory);
		Time.timeScale = 0;
        yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(1f));
		EventManager.TriggerEvent(Events.DeathFade);
        yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(1.5f));
        Time.timeScale = 1;
        MainController.SwitchScene(Scenes.Victory);
	}

}
