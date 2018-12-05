using UnityEngine.Networking;
using UnityEngine;

public class DebugLogging
{

	// Use this for initialization
	public static void Log(NetworkBehaviour networkBehaviour, string message)
    {
        if (networkBehaviour.isServer)
        {
            Debug.Log("Server: " + message); 
        }
        else 
        {
            Debug.Log("Client: " + message);
        }
    }
}
