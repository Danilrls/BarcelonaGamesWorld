using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownFinished : MonoBehaviour {

	public bool countdownEnd = false; //accedit a Temps.cs

	public void CountdownIsFinished(){ //event in Countdown.anim
		countdownEnd = true;

	}
}
