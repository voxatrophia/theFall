using UnityEngine;

public class AspectRatioController : Singleton<AspectRatioController> {

    public Transform LeftWall;
    public Transform RightWall;
    private float AspectRatio;

    private float scale;

    public float Scale {
        get {
            return scale;
        }
        private set {
            scale = value;
        }
    }

    void Awake() {
        AspectRatio = (float)Screen.width / (float)Screen.height;
        scale = (AspectRatio * 9) / 16;

        //Set Walls
        float rightPos = (AspectRatio * Camera.main.orthographicSize) + 0.5f;
        float leftPos = -((AspectRatio * Camera.main.orthographicSize) + 0.5f);
        LeftWall.position = new Vector3(leftPos, LeftWall.position.y, LeftWall.position.z);
        RightWall.position = new Vector3(rightPos, RightWall.position.y, RightWall.position.z);
    }
}
