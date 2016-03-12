using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class FlashSpriteColor : MonoBehaviour {

   	public float flashTime = 1f;

    SpriteRenderer spr;
    Color origColor;

    void Start(){
    	spr = GetComponent<SpriteRenderer>();
    	origColor = spr.color;
    }

    public void FlashSprite(Color clr){
    	StartCoroutine(Transition(clr));
    }

    IEnumerator Transition(Color flash) {
        float endInvul = flashTime + Time.time;
        while(Time.time < endInvul){
            float lerpTransitionTime = 0.1f;
            float lerpStartTime = Time.time;
            float lerpEndTime = lerpStartTime + lerpTransitionTime;

            while (lerpEndTime >= Time.time)
            {
                spr.color = Color.Lerp(origColor, flash, (Time.time - lerpStartTime)/lerpTransitionTime);
                yield return null;
            }

            yield return new WaitForSeconds(0.1f);

            lerpStartTime = Time.time;
            lerpEndTime = lerpStartTime + lerpTransitionTime;

            while (lerpEndTime >= Time.time)
            {
                spr.color = Color.Lerp(flash, origColor, (Time.time - lerpStartTime)/lerpTransitionTime);
                yield return null;
            }
        }
        spr.color = origColor;
        yield return null;
    }

    public void ChangeSpriteColor(Color clr){
        spr.color = clr;
    }
}
