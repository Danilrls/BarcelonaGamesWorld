using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required when Using UI elements.

public class AudioVolume : MonoBehaviour {
	//Nota: es fica dins el slider de musica

	AudioSource initialsong;
	public Slider sliderMusic;

	void Awake (){ //es crida quan s'executa l'script per 1r cop
		initialsong = (AudioSource)FindObjectOfType(typeof(AudioSource)); //troba el audio que es reproduïa en l'escena anterior(MainMenu)
		//sliderMusic.value = initialsong.volume;

	}
		
	public void volumSong() //s'activa quan es canvia el valor de l'slider
	{

		initialsong.volume = sliderMusic.value;
	}
}
