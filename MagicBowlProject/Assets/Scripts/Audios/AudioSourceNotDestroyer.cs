using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceNotDestroyer : MonoBehaviour {
	//es fica dins de l'audiosource
	private static AudioSourceNotDestroyer instance = null;
	public static AudioSourceNotDestroyer Instance {
		get { return instance; }
	}
	void Awake() {  //es crida el primer cop que es crida l'script
		//conserva l'objecte despres de canviar d'scene, mira que si ja existeix no es dupliqui
		if (instance != null) {
			Destroy(this.gameObject);
			return;
		} else {
			instance = this;
		}
		DontDestroyOnLoad(this.gameObject);
	}
}