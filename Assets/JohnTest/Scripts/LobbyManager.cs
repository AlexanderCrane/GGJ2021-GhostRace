using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class LobbyManager : NetworkBehaviour
{
    public GameObject ghost1;
    public GameObject ghost2;
    public GameObject human1;
    public GameObject human2;

    [SyncVar(hook = nameof(initializeText))]
    public string myText = "";
    public Text text;

    public void setConnectionText()
    {
        if (isServer)
        {
            Debug.Log("on rpc connect being called");
            myText = "Number of Players: \n" + NetworkServer.connections.Count;
        }
    }
    
    public void setStartingText()
    {
        myText = "Starting game...\n" + "Number of Players: \n" + NetworkServer.connections.Count;;
    }

    private void initializeText(string oldValue, string newValue)
    {
        Debug.Log("initialize text");
        text.text = newValue;
    }

    public void activateghost1()
    {
        Debug.Log(connectionToClient);
        Debug.Log(connectionToClient.identity);
        GameNetworkManager.ReplacePlayer(connectionToClient,GameObject.FindWithTag("playerholder").GetComponent<PlayerHolder>().ghost[0]);
    }

    public void activateghost2()
    {
        Debug.Log(connectionToClient);
        Debug.Log(connectionToClient.identity);
        GameNetworkManager.ReplacePlayer(connectionToClient,GameObject.FindWithTag("playerholder").GetComponent<PlayerHolder>().ghost[1]);
    }

    public void activatehuman1()
    {
        Debug.Log("button pressed");
        Debug.Log(connectionToServer);
        Debug.Log(connectionToServer.identity);
        GameNetworkManager.ReplacePlayer(connectionToServer,GameObject.FindWithTag("playerholder").GetComponent<PlayerHolder>().human[0]);
    }

    public void activatehuman2()
    {
        Debug.Log(connectionToClient);
        Debug.Log(connectionToClient.identity);
        GameNetworkManager.ReplacePlayer(connectionToClient,GameObject.FindWithTag("playerholder").GetComponent<PlayerHolder>().human[1]);
    }
}
