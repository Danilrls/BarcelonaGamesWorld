using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ScoreManager : NetworkBehaviour
{
    [SyncVar] public int score = 0; // Player's score

    private void OnTriggerEnter(Collider col)
    {
        if (isLocalPlayer && GetComponent<BallHandler>().ballIsGrabbed && col.tag.Contains("Keep") &&
             !col.tag.Contains(tag))
        {
            score++;
            GetComponent<BallHandler>().CmdRespawnBall();
            CmdUpdateScoreboard(tag, score);
        }
    }

    [Command]
    private void CmdUpdateScoreboard(string tag, int score)
    {
        RpcUpdateScoreboard(tag, score);
    }

    [ClientRpc]
    private void RpcUpdateScoreboard(string tag, int score)
    {
        GameObject.Find(tag + "Score").GetComponent<Text>().text = score.ToString();
    }
}
