using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required when Using UI elements.
using UnityEngine.SceneManagement; 
	
public class SettingsMenu : MonoBehaviour {

	public Slider sliderMusic;
	AudioSource initialsong;

	void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		if (scene.name == "SubmenuSettings") {
			Debug.Log ("aa");
			sliderMusic.value = initialsong.volume;
		}

	}

}
