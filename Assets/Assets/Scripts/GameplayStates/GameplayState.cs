using System;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GameplayState : AState
{
    public GameObject goal;
    public GameObject player;

    public override void Enter(AState from)
    {
        GameStatusText.instance.SetText("Currently playing (0) player(s)");
        player = GameManager.instance.gameObject.GetComponent<GameManagerNetwork>().localPlayerGO;
        //player = GetLocalPlayerGameObject();
    }

    public GameObject GetLocalPlayerGameObject()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < players.Length; i++)
        {
            GameObject player = players[i];
            if (player.GetComponent<NetworkIdentity>().isLocalPlayer)
            {
                return player;
            }
        }
        return null;
    }

    public override void Exit(AState to)
    {
        //throw new System.NotImplementedException();
    }

    public override string GetName()
    {
        return "GameplayState";
    }

    public override void Tick()
    {
        if (player.transform.position.x >= goal.transform.position.x)
        {
            manager.SwitchState("PregameState");
            //you won, transition to end game state
        }
    }
}
