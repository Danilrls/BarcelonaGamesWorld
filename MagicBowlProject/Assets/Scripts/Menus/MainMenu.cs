using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void settingsPressed(){
		
		SceneManager.LoadScene("SubmenuSettings");
	}

	public void BackToMain(){
		SceneManager.LoadScene("MainMenu");
	}

	public void findMatch(){
		SceneManager.LoadScene("CharacterSelection");
	}
}
