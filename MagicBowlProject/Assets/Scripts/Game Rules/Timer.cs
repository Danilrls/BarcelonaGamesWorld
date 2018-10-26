using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Timer : NetworkBehaviour
{
    //nota: els valors es tornen a reiniciar cada vegada que es carrega l'escena
    private CountdownFinished countEnd; //script
    private bool animationStart;
    private bool timeCounter;
    private bool attackDisabled;
    public GameObject countdown;
	public AudioClip whistle;
	private AudioSource audio;
    private GameObject score;
    //public GameObject directional_light;
    private GameObject timerCanvas;
	private bool inmatch;

    private float minutes;
    private float seconds;
    [SyncVar]
    public float timeRemaining;


    [SyncVar]
    public bool allowPlayerMovementTimer; //accessed by PlayerController.cs

    private PlayerController playerController;

    void Start()
    {
		inmatch = true;
		audio=(AudioSource)FindObjectOfType(typeof(AudioSource));
		//audio.clip = whistle;
        attackDisabled = false;
        timeCounter = false;
        timerCanvas = transform.Find("Timer Canvas").gameObject;
        score = GameObject.Find("score");
        countEnd = countdown.GetComponent<CountdownFinished>();
        timeRemaining = 10;
        playerController = GetComponent<PlayerController>();
    }
    void Update()
	{
		if (inmatch) {
			//startGame es troba a CustomNetworkManager.cs. Inicia animacio countdown
			if (playerController.gameStarted == 1 && !animationStart) {
				//animacio 3,2,1,start https://www.youtube.com/watch?v=ZEP3lxsA-FY
				countdown.SetActive (true); //starts animation
				animationStart = true;
			}

			if (animationStart) {
				//has to be accessed now because before was inactive
				if (countEnd.countdownEnd) {
					timeCounter = true;
					countdown.SetActive (false);

				}
			}

			if (isServer && timeCounter) {
				timeRemaining -= Time.deltaTime;
				allowPlayerMovementTimer = true;
			}
			
			if (timeCounter) {
				//canvia el temps
				float minutes = Mathf.Floor (timeRemaining / 60);
				float seconds = Mathf.Floor (timeRemaining % 60);
				string minutes2 = minutes.ToString ("00");
				string seconds2 = seconds.ToString ("00");
				timerCanvas.transform.GetChild (1).GetComponent<Text> ().text = minutes2 + ":" + seconds2; //accesses to Time

				//acaba partida si passen 3 minuts
				if (timeRemaining <= 0) {
					audio.PlayOneShot (whistle, 1.0F);//whistle end game sound


					if (isServer) {
						allowPlayerMovementTimer = false; //impedeix que pugui moure's
					}

					//impedeix que pugui atacar
					if (!attackDisabled) {
						foreach (Transform child in gameObject.transform) {
							if (child.gameObject.transform.tag == "spellCanvas") {
								attackDisabled = true;
								CanvasGroup spellCanvasPlayer = child.gameObject.GetComponent<CanvasGroup> ();
								spellCanvasPlayer.alpha = 0;
								spellCanvasPlayer.interactable = false;
							}
						}
					}

					timeCounter = false;
					inmatch = false;

					//directional_light.GetComponent<Light> ().intensity = 0.1f;
					score.GetComponent<Animator> ().Play ("ShowResults");
					
					GameObject.Find("NetworkManager").GetComponent<CustomNetworkManager>().stopIterating = false;

					timerCanvas.SetActive (false);
					//game is ended by ScoreAnimationFinished.cs
					//reinicia els valors "private" degut a que canvia d'escena
				}
			}
		}
	}
}