using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GameNetworkManager : NetworkManager
{
    public NetworkManagerScriptableObject gameNetworkManagerScriptableObject;
    public override void Start() {
        base.Start();
        this.networkAddress = "localhost";
        gameNetworkManagerScriptableObject.gameNetworkManager = this;
    }
    public GameObject human;
    public GameObject ghost;

    public void changePlayerToHuman()
    {
        playerPrefab = human;
    }

    public void changePlayerToGhost()
    {
        playerPrefab = ghost;
    }

    public void changeNetworkAddress(string networkAddress)
    {
        this.networkAddress = networkAddress;
        Debug.Log("address changed to: " + networkAddress);
    }
}
