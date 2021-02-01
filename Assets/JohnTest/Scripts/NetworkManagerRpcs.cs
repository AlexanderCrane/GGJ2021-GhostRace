using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class NetworkManagerRpcs : NetworkBehaviour
{
    public GameObject lobbyCanvas;
    public Text text1;

    [ClientRpc]
    public void RpcSetConnectionText()
    {
        
        Debug.Log("on rpc connect being called");
        lobbyCanvas.SetActive(true);
        text1.text = "Number of Players: \n" + NetworkServer.connections.Count;
    }
    

    [ClientRpc]
    public void RpcSetStartingText()
    {
        lobbyCanvas.SetActive(true);
        text1.text = "Starting game...\n" + "Number of Players: \n" + NetworkServer.connections.Count;;
    }
}
