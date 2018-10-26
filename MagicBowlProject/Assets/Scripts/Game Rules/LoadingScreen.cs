using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;


public class LoadingScreen : MonoBehaviour
{
    private PlayerController playerController;
    private CustomNetworkManager net;
    private GameObject mainCamera;
    private GameObject HUDCanvas;
    private GameObject load;
    private CanvasGroup staminaCanvasPlayer;
    private CanvasGroup HUDCanvasPlayer;
    private CanvasGroup countdownCanvasPlayer;
    private CanvasGroup timerCanvasPlayer;
    private CanvasGroup spellCanvasPlayer;
    private bool flag;
    private bool flagspell;
    private bool flagspell2;

    void Start()
    {
        flag = false;
        flagspell = false;
        flagspell2 = false;
        playerController = GetComponent<PlayerController>();
        mainCamera = GameObject.Find("Main Camera");
        load = GameObject.Find("Load");
        //loadingCamera = load.transform.GetChild(0).gameObject;
        HUDCanvas = GameObject.Find("HUDCanvas");
        //loadingCanvas = load.transform.GetChild(1).gameObject;
        staminaCanvasPlayer = transform.Find("Stamina Canvas").gameObject.GetComponent<CanvasGroup>();
        HUDCanvasPlayer = transform.Find("HUD Canvas").gameObject.GetComponent<CanvasGroup>();
        timerCanvasPlayer = transform.Find("Timer Canvas").gameObject.GetComponent<CanvasGroup>();
        countdownCanvasPlayer = transform.Find("Countdown Canvas").gameObject.GetComponent<CanvasGroup>();
        //net = network.GetComponent<CustomNetworkManager> ();
        mainCamera.GetComponent<Camera>().enabled = false;

        //makes canvas invisible
        staminaCanvasPlayer.alpha = 0;
        staminaCanvasPlayer.interactable = false;
        HUDCanvasPlayer.alpha = 0;
        HUDCanvasPlayer.interactable = false;
        timerCanvasPlayer.alpha = 0;
        timerCanvasPlayer.interactable = false;
        countdownCanvasPlayer.alpha = 0;
        countdownCanvasPlayer.interactable = false;
    }

    void Update()
    {
        if (!flag && !flagspell && transform.childCount >= 7)
        { //spellCanvas ja existeix
            if (transform.GetChild(transform.childCount - 1).gameObject.transform.tag == "spellCanvas")
            { //spellCanvas han de tenir el tag
                spellCanvasPlayer = transform.GetChild(transform.childCount - 1).
					gameObject.GetComponent<CanvasGroup>();
                spellCanvasPlayer.alpha = 0;
                spellCanvasPlayer.interactable = false;
                flagspell = true;
            }

        }

        if (!flag && playerController.gameStarted == 1)
        {
            flag = true;
            //shows hudcanvas
            CanvasGroup group = HUDCanvas.GetComponent<CanvasGroup>();
            group.alpha = 1;
            group.interactable = true;
            staminaCanvasPlayer.alpha = 1;
            staminaCanvasPlayer.interactable = true;
            HUDCanvasPlayer.alpha = 1;
            HUDCanvasPlayer.interactable = true;
            timerCanvasPlayer.alpha = 1;
            timerCanvasPlayer.interactable = true;
            countdownCanvasPlayer.alpha = 1;
            countdownCanvasPlayer.interactable = true;

            load.SetActive(false); //deactivates Loading camera and LoadingCanvas

            //changes camera
            mainCamera.GetComponent<Camera>().enabled = true;
        }

        if (!flagspell2 && flag && transform.childCount >= 7)
        {
            if (transform.GetChild(transform.childCount - 1).gameObject.transform.tag == "spellCanvas")
            {
                flagspell2 = true;
                spellCanvasPlayer = transform.GetChild(transform.childCount - 1).
					gameObject.GetComponent<CanvasGroup>();
                spellCanvasPlayer.alpha = 1;
                spellCanvasPlayer.interactable = true;
            }
        }
    }
}
