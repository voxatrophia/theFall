using UnityEngine;
using System.Collections;

public class UseItem : MonoBehaviour {

	GameObject item;
	AudioSource audioSrc;

	public AudioClip bombSE;
	public AudioClip appleSE;
	public AudioClip watchSE;
    public AudioClip orbSE;

    void Start(){
		audioSrc = GetComponent<AudioSource>();
	}

	void Update () {
		if(Input.GetKeyDown(Controls.Instance.useItem)){
			item = ItemManager.i.GetItem();
			if(item != null){
				switch(item.tag){
					case Tags.Bomb:
						Bomb useBomb = item.GetComponent<Bomb>();
						useBomb.Explode();
						audioSrc.PlayOneShot(bombSE);
						break;
					case Tags.Apple:
						Apple useApple = item.GetComponent<Apple>();
						useApple.HealthUp();
						audioSrc.PlayOneShot(appleSE);
						break;
					case Tags.Stopwatch:
						audioSrc.PlayOneShot(watchSE);
						Stopwatch useStopwatch = item.GetComponent<Stopwatch>();
						useStopwatch.StopTime();
						break;
                    case Tags.Orb:
                        audioSrc.PlayOneShot(orbSE);
                        Orb useOrb = item.GetComponent<Orb>();
                        useOrb.SpeedUp();
                        break;
					default:
						break;

				}
                //Track item
                Tracker.Instance.TrackItemUsed(item.tag);

				//Remove item from image
				ItemManager.i.UsedItem();

                ////Tutorial
                //if (TutorialManager.Instance.inTutorial) {
                //    EventManager.TriggerEvent(TutorialEvents.ItemUsed);
                //}

            }
		}
	}
}
