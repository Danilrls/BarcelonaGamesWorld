using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Networking;

public class GlobalVariables : MonoBehaviour
{
    public GameObject networkManager;
    private CustomNetworkManager customNetworkManager;

    public List<string> tags;
    public int currentSpawnedPlayer;
    public List<Transform> playerTransforms;


    public bool allowPlayerMovement;
    // Use this for initialization
    void Start()
    {
        customNetworkManager = networkManager.GetComponent<CustomNetworkManager>();


        allowPlayerMovement = false;

        tags = new List<string> { "PlayerPurple", "PlayerBlue", "PlayerGreen", "PlayerRed" };
        currentSpawnedPlayer = 0;
    }

    void Update()
    {
        if (customNetworkManager.startGame)
        {
            allowPlayerMovement = true;
        }
    }
}
