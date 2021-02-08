using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GameNetworkManager : NetworkManager
{
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

    
}
