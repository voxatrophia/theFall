using UnityEngine;
using System.Collections;

public class ZigZag : MonoBehaviour {

    Rigidbody2D rb;
    bool canMove;
    Vector2 origVel;
    int startDir;

    void Start() {
        origVel = rb.velocity;
    }

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable() {
        canMove = true;
        EventManager.StartListening(Events.StopMoving, StopMoving);
        rb.velocity = origVel;

        startDir = (Random.value < 0.5f) ? -1 : 1;
        Vector2 v = new Vector2(rb.velocity.x * startDir, rb.velocity.y);
        rb.velocity = v;
        Debug.Log(rb.velocity);

    }

    void OnDisable() {
        EventManager.StopListening(Events.StopMoving, StopMoving);
    }

    void StopMoving() {
        StartCoroutine(StopMovingCoroutine());
    }

    IEnumerator StopMovingCoroutine() {
        canMove = false;
        rb.velocity = new Vector2(0, 0);
        yield return Yielders.Get(2f);
        rb.velocity = origVel;
        canMove = true;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag(Tags.Player)) {
            this.gameObject.SetActive(false);
        }

        if (other.CompareTag("Wall")){
            Vector2 vel = new Vector2(rb.velocity.x * -1, rb.velocity.y);
            rb.velocity = vel;
        }
    }
}
