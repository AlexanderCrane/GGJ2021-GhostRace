using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

/// <summary>
/// A client can call a command (client -> server). The server executes the command,
/// and likely you will want an rpc called from the command (server -> client). An RPC is called on ALL clients.
/// </summary>
public class PlayerNetworkCommands : NetworkBehaviour
{
    public NetworkManagerScriptableObject networkManagerScriptableObject;
    private Transform playerCamera;
    
    [Command]
    public void Cmd_ActivateTrap(GameObject trap)
    {
        trap.GetComponent<TrapActivate>().Rpc_Activate();
    }

    [Command]
    public void Cmd_InteractWithMcGuffin()
    {
        HumanController humanController = GetComponent<HumanController>();
        if (humanController.nearMcGuffin && humanController.McGuffinEquipped != true) //server side check
        {
            Rpc_PickUpMcGuffin();
        }
        else if (humanController.McGuffinEquipped && humanController.nearGoal)
        {
            Rpc_HandleGameEnd(GetComponent<PlayerNetworkManager>().teamNumber);
        }
    }

    [ClientRpc]
    private void Rpc_PickUpMcGuffin()
    {
        GetComponent<HumanController>().pickUpMcGuffin(); //call on clients
    }

    [ClientRpc]
    private void Rpc_HandleGameEnd(int winningTeam)
    {
        networkManagerScriptableObject.loadWinLoseScene(winningTeam);
    }

    /// <summary>
    /// Call this function from a player script, and it will execute on the server.
    /// This is unsafe.
    /// </summary>
    [Command]
    public void CmdInteractRaycast()
    {
        Vector3 forward = playerCamera.TransformDirection(Vector3.forward).normalized;
        float distance = 3.0f;

        RaycastHit hit;
        if (Physics.Raycast(playerCamera.position, forward, out hit, distance))
        {
            if (hit.transform.tag == "FakeDoorTag")
            {
                // hit.transform.GetComponent<Door>().RpcOpenDoor);
            }
        }
    }

    public void setPlayerCameraTransform(Transform cameraTransform)
    {
        playerCamera = cameraTransform;
    }

    /// <summary>
    /// Example Command call. Function name MUST start with Cmd.
    /// There may be instances where you do not need to do any checks. Checks are purely for security reasons on the server side.
    /// </summary>
    [Command]
    private void CmdExampleFunction()
    {
        // Do any necessary checks or additional required actions.
        // NetworkServer.Spawn(this.gameObject);

        // Do the Rpc function call.
    }

    /// <summary>
    /// Example RPC (Remote Procedure Call) call. Function name MUST start with Rpc.
    /// </summary>
    /// <param name="someObject">List of Supported Data Types: https://mirror-networking.com/docs/Articles/Guides/DataTypes.html</param>
    [ClientRpc]
    private void RpcExampleFunction(GameObject someObject)
    {
        // Do the actions on the client side
        // NetworkServer.Destroy(someObject);
    }
}
