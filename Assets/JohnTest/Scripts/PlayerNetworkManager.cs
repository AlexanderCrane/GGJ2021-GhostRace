using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerNetworkManager : NetworkBehaviour
{
    [Tooltip("A list of gameobject body parts that will be set inactive to the local player.")]
    public List<GameObject> playerBodyParts;

    [Header("Enabled Local Player Objects")]
    public GameObject playerCamera;
    public HumanController humanController;

    // Start is called before the first frame update
    public override void OnStartLocalPlayer()
    {
        if (GetComponent<PlayerNetworkCommands>() == null)
        {
            Debug.LogError("Add PlayerNetworkCommands not attached with PlayerNetworkManager.");
        }
        

        base.OnStartLocalPlayer();
        DontDestroyOnLoad(this.gameObject);
        if (GetComponent<NetworkIdentity>().isLocalPlayer)
        {
            playerCamera = GetComponent<PlayerManager>().playerCamera.gameObject;
            enableLocalPlayerInput();
            disableLocalBodyParts();

            enableCameraComponents();
        }
    }

    private void Update()
    {
        if (!isLocalPlayer) return;
        
        //call raycast or local update methods here...
    }

    private void enableLocalPlayerInput()
    {
        /*
         * Enable character input controllers here, for example:
         * GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = true;
         */
        GhostController ghostController = GetComponent<GhostController>();
        if (humanController == null && ghostController == null)
        {
            Debug.LogError("Controller not connected to player network manager.");
        }
        else
        {
            if (humanController != null)
            {
                humanController.enabled = true;
            }

            if (ghostController != null)
            {
                ghostController.enabled = true;
            }
        }
    }

    private void disableLocalBodyParts()
    {
        playerBodyParts.ForEach(go => go.SetActive(false));
    }

    private void enableCameraComponents()
    {
        //enable camera components here such as audio listener
        playerCamera.GetComponent<Camera>().enabled = true;
        playerCamera.GetComponent<AudioListener>().enabled = true;
    }
}
