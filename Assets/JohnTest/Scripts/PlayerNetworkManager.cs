using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerNetworkManager : NetworkBehaviour
{
    [SyncVar]
    public int teamNumber;
    public Camera playerCameraPrefab;
    [Tooltip("A list of gameobject body parts that will be set inactive to the local player.")]
    public List<GameObject> playerBodyParts;

    [Header("Enabled Local Player Objects")]
    public GameObject playerCamera;

    // Start is called before the first frame update
    public override void OnStartLocalPlayer()
    {
        if (GetComponent<PlayerNetworkCommands>() == null)
        {
            Debug.LogError("Add PlayerNetworkCommands not attached with PlayerNetworkManager.");
        }
        

        base.OnStartLocalPlayer();
        if (isLocalPlayer)
        {
            initializeLocalPlayer();
        }
    }

    private void Update()
    {
        if (!isLocalPlayer) return;
        
        //call raycast or local update methods here...
    }

    public void initializeLocalPlayer()
    {
        enableLocalPlayerInput();
        disableLocalBodyParts();
        enableCameraComponents();
    }

    private void enableLocalPlayerInput()
    {
        /*
         * Enable character input controllers here, for example:
         * GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = true;
         */
         
        Camera mainCamera;
        HumanController humanController = GetComponent<HumanController>();
        GhostController ghostController = GetComponent<GhostController>();
        if (humanController == null && ghostController == null)
        {
            Debug.LogError("Controller not connected to player network manager.");
        }
        else
        {
            if (humanController != null)
            {
                mainCamera = GameObject.Instantiate(playerCameraPrefab, transform.TransformPoint(Vector3.forward) , transform.rotation);
                GetComponent<PlayerManager>().playerCamera = mainCamera;
                playerCamera = GetComponent<PlayerManager>().playerCamera.gameObject;
                humanController.cam = mainCamera;
                GetComponent<PlayerNetworkCommands>().setPlayerCameraTransform(mainCamera.transform);
                humanController.enabled = true;
            }

            if (ghostController != null)
            {
                mainCamera = ghostController.GhostEyes;
                GetComponent<PlayerManager>().playerCamera = mainCamera;
                playerCamera = GetComponent<PlayerManager>().playerCamera.gameObject;
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
