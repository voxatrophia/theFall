using UnityEngine;
using System.Collections;

public class LevelManager : Singleton<LevelManager> {

	public string SceneName;
	public GameObject Player;
	public GameObject MainCamera;
	public GameObject Boss;
	public AudioClip StoryTheme;
	public AudioClip ArcadeTheme;

	int mode;

	void Start(){
		mode = GetMode();
		if(mode == Modes.Arcade){
			AudioManager.Instance.SwitchMusic(ArcadeTheme);
		}
		else if(mode == Modes.Story) {
			AudioManager.Instance.SwitchMusic(StoryTheme);
		}
	}

	public int GetMode(){
		//Modes
		//0 = Story
		//1 = Arcade
		//Defaults to Arcade
		mode = (PlayerPrefs.HasKey("GameMode")) ? PlayerPrefs.GetInt("GameMode") : Modes.Arcade;
		return mode;
	}

}
