using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class GameManagerNetwork : NetworkBehaviour
{
    //public List<NetworkPlayer> players = new List<NetworkPlayer>();
    //public NetworkPlayer localPlayer;
    public GameObject localPlayerGO;
    public static UnityEvent playerChanged = new UnityEvent();

    private void Start()
    {
        
    }

    /*void OnDisconnectedFromServer(NetworkDisconnection info)
    {
        //Debug.Log("Disconnected from server: " + info);
    }

    void OnPlayerDisconnected(NetworkPlayer player)
    {
       // Debug.Log("Clean up after player " + player);
    }

    void OnMasterServerEvent(MasterServerEvent MSE)
    {
        //Debug.Log("Clean up dsddsyer ");
    }

    public void OnPlayerConnected(NetworkPlayer player)
    {
        //Debug.Log("Clean up OnPlayerConnected ");
    }*/

    override public void OnStartServer()
    {
        base.OnStartServer();
        //Debug.Log("OnStartServer ");
        playerChanged.Invoke();
    }

    override public void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        //Debug.Log("Clean up OnStartLocalPlayer ");
    }

    override public void OnStartClient()
    {
        base.OnStartClient();
        //playerChanged.Invoke();
       // Debug.Log("on start client");
    }

    public override void OnNetworkDestroy()
    {
        base.OnNetworkDestroy();
       // Debug.Log("OnNetworkDestroy ");
        playerChanged.Invoke();
    }

    public static int GetNumberOfPlayers()
    {
        return GetAllPlayers().Count;
    }

    public static bool AreAllPlayersReady()
    {
        foreach (var player in GetAllPlayers())
        {
            if (!player.isReady)
            {
                return false;
            }
        }
        return true;
    }

    public static List<PlayerController> GetAllPlayers()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        List<PlayerController> playerControllers = new List<PlayerController>();

        for (int i = 0; i < players.Length; i++)
        {
            playerControllers.Add(players[i].GetComponent<PlayerController>());
        }
        return playerControllers;
    }

    public static PlayerController GetLocalPlayer()
    {
        foreach (var player in GetAllPlayers())
        {
            if (player.gameObject.GetComponent<NetworkIdentity>().isLocalPlayer)
            {
                return player;
            }
        }        
        return null;
    }
}
