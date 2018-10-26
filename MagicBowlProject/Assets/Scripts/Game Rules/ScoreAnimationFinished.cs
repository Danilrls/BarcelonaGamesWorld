using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;


public class ScoreAnimationFinished : MonoBehaviour {

	//public RuntimeAnimatorController score2;



	void Start(){
		//GetComponent<Animator>().runtimeAnimatorController = score2 as RuntimeAnimatorController;
	}

	public void FinishGame(){
		CustomNetworkManager m = GameObject.Find("NetworkManager").GetComponent<CustomNetworkManager>();
		m.closeMatch();

		SceneManager.LoadScene ("MainMenu");

	}

	
}
