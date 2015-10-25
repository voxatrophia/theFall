using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public string SceneName;
	public GameObject Player;
	public GameObject MainCamera;
	public GameObject Boss;
	public AudioClip SceneTheme;

	void Start(){
		AudioManager.Instance.SwitchMusic(SceneTheme);
	}

}
