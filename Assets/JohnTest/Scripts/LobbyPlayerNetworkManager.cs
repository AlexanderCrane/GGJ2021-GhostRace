using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class LobbyPlayerNetworkManager : NetworkBehaviour
{
    public NetworkManagerScriptableObject networkManagerScriptableObject;
    public PlayerInfoScriptableObject playerInfoScriptableObject;
    GameObject humanPrefab;
    GameObject ghostPrefab;

    Vector3 spawnPosition = new Vector3(0,0,0);
    Quaternion spawnRotation = Quaternion.identity;

    // Start is called before the first frame update
    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        if (GetComponent<NetworkIdentity>().isLocalPlayer)
        {
            enableComponents();
        }
    }

    private void enableComponents()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        GetComponent<Camera>().enabled = true;
        GetComponent<AudioListener>().enabled = true;
    }

    public enum PlayerType : byte
    {
        Human, Ghost
    }

    [Command]
    public void Cmd_ReplacePlayer(PlayerType playerType)
    {
        GameObject playerPrefab;
        switch(playerType)
        {
            case PlayerType.Human:
                playerPrefab = humanPrefab;
                break;
            case PlayerType.Ghost:
                playerPrefab = ghostPrefab;
                break;
            default:
                playerPrefab = null;
                Debug.LogError("Invalid PlayerType to be spawned.");
                break;
        }
        GameObject playerObject = Instantiate(playerPrefab, spawnPosition, spawnRotation);
        NetworkServer.Spawn(playerObject);
        if (NetworkServer.ReplacePlayerForConnection(connectionToClient, playerObject))
        {
            NetworkServer.Destroy(this.gameObject);
        }
    }
}
