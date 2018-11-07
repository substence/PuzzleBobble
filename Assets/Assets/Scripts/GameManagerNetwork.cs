using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManagerNetwork : NetworkBehaviour
{
    public List<NetworkPlayer> players = new List<NetworkPlayer>();
    public NetworkPlayer localPlayer;
    public GameObject localPlayerGO;

    private void OnPlayerConnected(NetworkPlayer player)
    {
        players.Add(player);
    }

    int GetNumberOfPlayers()
    {
        return players.Count;
    }
}
