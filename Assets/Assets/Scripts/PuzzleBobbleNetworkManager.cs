using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using System.Collections.Generic;

public class PuzzleBobbleNetworkManager : NetworkManager
{
    public List<PlayerController> players = new List<PlayerController>();
    public static UnityEvent playerChanged = new UnityEvent();

    public override void OnServerConnect(NetworkConnection connection)
    {
        //Change the text to show the connection
        Debug.Log("OnServerConnect ");
        base.OnServerConnect(connection);
        playerChanged.Invoke();
    }

    //Detect when a client disconnects from the Server
    public override void OnServerDisconnect(NetworkConnection connection)
    {
        //Change the text to show the loss of connection
        Debug.Log("OnServerDisconnect ");
        base.OnServerDisconnect(connection);
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

    /*public override void OnClientConnect(NetworkConnection connection)
    {
        //Change the text to show the connection on the client side
        Debug.Log("OnClientConnect ");
    }

    //Detect when a client connects to the Server
    public override void OnClientDisconnect(NetworkConnection connection)
    {
        //Change the text to show the connection loss on the client side
        Debug.Log("OnClientDisconnect ");
    }

    /*void OnPlayerConnected(NetworkPlayer player)
    {
        Debug.Log("Player connected from " + player.ipAddress + ":" + player.port);
    }

    void OnPlayerDisconnected(NetworkPlayer player)
    {
        Debug.Log("Clean up after player " + player);

        Network.RemoveRPCs(player);
        Network.DestroyPlayerObjects(player);
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
    {
        Debug.Log("OnServerAddPlayer");
        OnServerAddPlayer(conn, playerControllerId, extraMessageReader);
    }

    override public void OnStartServer()
    {
        base.OnStartServer();
        Debug.Log("OnStartServer ");
    }

    void OnDisconnectedFromServer(NetworkDisconnection info)
    {
        Debug.Log("Disconnected from server: " + info);
    }*/
}
