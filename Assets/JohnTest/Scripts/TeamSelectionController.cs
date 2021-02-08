using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class TeamSelectionController : NetworkBehaviour
{
    public List<GameObject> playerSelectionButtons;
    public GameObject startingGamePanel;

    private TeamSelectionManager teamSelectionManager;

    public override void OnStartClient()
    {
        base.OnStartClient();
        
        teamSelectionManager = GameObject.FindWithTag("TeamSelection").GetComponent<TeamSelectionManager>();
        teamSelectionManager.addTeamSelectionControllerListener(this);
    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        setButtons();
    }

    public override void OnStopClient()
    {
        base.OnStopClient();
        teamSelectionManager.removeTeamSelectionControllerListener(this);
    }

    [Command]
    public void Cmd_SetPlayerSelectionOff(int index)
    {
        teamSelectionManager.changeStateRequest(GetComponent<NetworkIdentity>(), index, false);
    }

    [Command]
    public void Cmd_SetPlayerSelectionOn(int index)
    {
        teamSelectionManager.changeStateRequest(GetComponent<NetworkIdentity>(), index, true);
    }

    public void setButtons()
    {
        for (int i = 0; i < teamSelectionManager.playerSelectionStates.Count; i++)
        {
            if (playerSelectionButtons[i].GetComponent<Button>().interactable)
            {
                playerSelectionButtons[i].GetComponent<Button>().interactable = teamSelectionManager.playerSelectionStates[i];
            }
        }
    }

    public void setButtons(bool[] states)
    {
        for (int i = 0; i < teamSelectionManager.playerSelectionStates.Count; i++)
        {
            if (playerSelectionButtons[i].GetComponent<Button>().interactable && !states[i])
            {
                playerSelectionButtons[i].GetComponent<Button>().interactable = states[i];
            }
        }
    }

    [ClientRpc]
    public void Rpc_SetButtonsNotInteractable()
    {
        for (int i = 0; i < teamSelectionManager.playerSelectionStates.Count; i++)
        {
            playerSelectionButtons[i].GetComponent<Button>().interactable = false;
        }
    }

    [ClientRpc]
    public void Rpc_SetStartingGameText()
    {
        startingGamePanel.SetActive(true);
    }
}
