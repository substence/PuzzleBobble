using UnityEngine;
using UnityEngine.Networking;

public class PuzzleBobbleNetworkManager : NetworkManager
{

    private void Start()
    {
        //Debug.Log("OnServerAddPlayer");

    }

    /*public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
    {
        Debug.Log("OnServerAddPlayer");
        OnServerAddPlayer(conn, playerControllerId, extraMessageReader);
    }

    override public void OnStartServer()
    {
        base.OnStartServer();
        Debug.Log("OnStartServer ");
    }*/

    public override void OnServerConnect(NetworkConnection connection)
    {
        //Change the text to show the connection
        Debug.Log("OnServerConnect ");
        OnServerConnect(connection);
    }

    //Detect when a client disconnects from the Server
    public override void OnServerDisconnect(NetworkConnection connection)
    {
        //Change the text to show the loss of connection
        Debug.Log("OnServerDisconnect ");
        OnServerDisconnect(connection);
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
    }*/

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

    void OnDisconnectedFromServer(NetworkDisconnection info)
    {
        Debug.Log("Disconnected from server: " + info);
    }*/
}
