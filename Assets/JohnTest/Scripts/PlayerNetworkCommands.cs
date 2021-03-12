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
    private HumanController humanController;

    private void Start() {
        humanController = GetComponent<HumanController>();
    }
    
    [Command]
    public void Cmd_ActivateTrap()
    {
        GameObject closestTrap = null;
        float closestDistance = 999.0f;
        Vector3 point1 = transform.position + Vector3.up;
        Vector3 point2 = transform.position + Vector3.down;

        RaycastHit[] hits = Physics.CapsuleCastAll(point1, point2, 0.5f, transform.forward, 0);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].transform.tag == "trap" && hits[i].transform.GetComponent<TrapActivate>() != null && hits[i].distance < closestDistance)
            {
                closestDistance = hits[i].distance;
                closestTrap = hits[i].transform.gameObject;
            }
        }

        if (closestTrap != null)
        {
            closestTrap.GetComponent<TrapActivate>().Rpc_Activate();
        }
    }

    [Command]
    public void Cmd_InteractWithMcGuffin()
    {
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
        humanController.pickUpMcGuffin(); //call on clients
    }

    [ClientRpc]
    private void Rpc_HandleGameEnd(int winningTeam)
    {
        networkManagerScriptableObject.loadWinLoseScene(winningTeam);
    }

    public void setPlayerCameraTransform(Transform cameraTransform)
    {
        playerCamera = cameraTransform;
    }
    
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "trap" && this.tag == "Player")
        {
            Rpc_TakeDamage();
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (this.gameObject.tag == "Player")
        {
            switch(other.gameObject.tag)
            {
                case "mcguffin":
                    humanController.nearMcGuffin = true;
                    break;
                case "bullet":
                    Rpc_TakeDamage();
                    break;
                case "Finish":
                    humanController.nearGoal = true;
                    break;
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (this.gameObject.tag == "Player")
        {
            switch(other.gameObject.tag)
            {
                case "mcguffin":
                    humanController.nearMcGuffin = false;
                    break;
                case "Finish":
                    humanController.nearGoal = false;
                    break;
            }
        }
    }

    [Command]
    public void Cmd_TakeDamage()
    {
        Rpc_TakeDamage();
    }

    [ClientRpc]
    public void Rpc_TakeDamage()
    {
        GetComponent<HumanController>().DropMcGuffin();
        GetComponent<HumanController>().TakeDamage();
    }

    [Command]
    public void Cmd_Attack(Vector3 position, Quaternion rotation)
    {
        HumanController humanController = GetComponent<HumanController>();
        if(!humanController.McGuffinEquipped && humanController.canAttack)
        {
            humanController.commandAttackReceiver();
            Rpc_Attack();
            // Create the bullet and give it a velocity according to the target point computed before
            GameObject bullet = GameObject.Instantiate(humanController.Projectile, position, rotation);
            NetworkServer.Spawn(bullet);
        }
    }

    [ClientRpc]
    public void Rpc_Attack()
    {
        GetComponent<HumanController>().animationController.SetTrigger("Spell1");
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
}
