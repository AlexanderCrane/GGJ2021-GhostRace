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
    private Transform playerCamera;

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
