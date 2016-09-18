using UnityEngine;
using System.Collections;

public class CameraLock : MonoBehaviour {
    public Transform player;

    Camera mainCam;
    //float camTop;
    //float camBottom;
    //float camLeft;
    //float camRight;

    //Camera deathCam;
    //float size = 10;
    //float xBuffer;
    //float yBuffer;
    //float xLeft;
    //float xRight;
    //float yTop;
    //float yBottom;
    Vector3 pos;

    //float aspectRatio;

    void OnEnable() {
        //aspectRatio = 1.7777f;
        //mainCam = Camera.main;
        //deathCam = GetComponent<Camera>();
        //camTop = mainCam.orthographicSize;
        //camBottom = -mainCam.orthographicSize;
        //camRight = aspectRatio * mainCam.orthographicSize;
        //camLeft = -aspectRatio * mainCam.orthographicSize;
        //camTop = size;
        //camBottom = -size;
        //camLeft = -size * aspectRatio;
        //camRight = size * aspectRatio;
    }

    void Update() {
        //xBuffer = deathCam.orthographicSize * aspectRatio;
        //yBuffer = deathCam.orthographicSize;
        pos = player.position;
        //if (pos.x > camRight - xBuffer) {
        //    pos.x = camRight - xBuffer;
        //}
        //if (pos.x < camLeft + xBuffer) {
        //    pos.x = camRight + xBuffer;
        //}
        //if (pos.y > camTop - yBuffer) {
        //    pos.y = camTop - yBuffer;
        //}
        //if (pos.y < camBottom + xBuffer) {
        //    pos.y = camBottom + xBuffer;
        //}

        transform.position = new Vector3(pos.x, pos.y, -10);

        //transform.position = new Vector3(Mathf.Clamp(Player.position.x, xLeft, xRight), Mathf.Clamp(Player.position.y, yBottom, yTop), -10);
        //transform.position = new Vector3(Mathf.Clamp(Player.position.x, camLeft + xBuffer, camRight - xBuffer), Mathf.Clamp(Player.position.y, camBottom + yBuffer, camTop - yBuffer), -10);
        //transform.position = new Vector3(Mathf.Clamp(Player.position.x, camLeft + xBuffer, camRight), Mathf.Clamp(Player.position.y, camBottom + yBuffer, camTop - yBuffer), -10);

    }

}