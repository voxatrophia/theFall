using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class MovingPlatform : MonoBehaviour {

	public Vector2 velocity;
	public float speedIncrease = 1.5f;
	public float arcadeIncrease = 1.1f;
    public bool autoIncrease;
    public float autoIncreaseInterval = 30f;
    public float maxSpeed = 8f;

    float increase;
	Rigidbody2D rb;

    bool velocityChanging;

	void Start(){
		rb = GetComponent<Rigidbody2D>();
		rb.velocity = velocity;

        StartCoroutine(IncreaseSpeed());

		switch(LevelManager.Instance.GetMode()){
			case Modes.Arcade:
				increase = arcadeIncrease;
				break;
			case Modes.Story:
				increase = speedIncrease;
				break;
		}
        increase = 0.1f;
    }

	void OnEnable(){
		EventManager.StartListening(Events.StopMoving, StopMoving);
		EventManager.StartListening(Events.MoveBackwards, MoveBackwards);
		EventManager.StartListening(Events.BossNearDeath, MoveFaster);
        EventManager.StartListening(Events.MoveFaster, MoveFaster);
    }

    void OnDisable(){
		EventManager.StopListening(Events.StopMoving, StopMoving);
		EventManager.StopListening(Events.MoveBackwards, MoveBackwards);
		EventManager.StopListening(Events.BossNearDeath, MoveFaster);
        EventManager.StopListening(Events.MoveFaster, MoveFaster);
    }


    void StopMoving(){
		AudioManager.Instance.PauseSound();
		StartCoroutine(StopMovingCoroutine());
	}

	IEnumerator StopMovingCoroutine(){
        velocityChanging = true;
		rb.velocity = new Vector2(0, 0);
		yield return Yielders.Get(2f);
		rb.velocity = velocity;
        velocityChanging = false;
    }

    void MoveBackwards(){
		StartCoroutine(MoveBackwardsCoroutine());
	}

	IEnumerator MoveBackwardsCoroutine(){
        velocityChanging = true;
        rb.velocity = new Vector2(0, -rb.velocity.y);
		yield return Yielders.Get(1f);
		rb.velocity = velocity;
        velocityChanging = false;
    }

    void MoveFaster(){
        if (!velocityChanging) {
            velocity = new Vector2(0, Mathf.Abs(velocity.y) + increase);
            rb.velocity = velocity;
        }
    }

    IEnumerator IncreaseSpeed() {
        while (autoIncrease) {
            yield return Yielders.Get(autoIncreaseInterval);
            MoveFaster();
            if (rb.velocity.y >= maxSpeed) {
                autoIncrease = false;
            }
        }
    }
}
