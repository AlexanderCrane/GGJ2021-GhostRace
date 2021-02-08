using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class TeamSelectionManager : NetworkBehaviour
{
    enum PlayerType {human, ghost}
    struct PlayerSpawnData
    {
        public NetworkIdentity networkIdentity;
        public PlayerType playerType;
        public int team;
    }

    
    public GameObject humanPrefab;
    public GameObject ghostPrefab;

    public Transform team1SpawnTransform;
    public Transform team2SpawnTransform;

    public readonly SyncList<bool> playerSelectionStates = new SyncList<bool>();

    public List<TeamSelectionController> teamSelectionControllers = new List<TeamSelectionController>();
    private List<PlayerSpawnData> playerSpawnDatas = new List<PlayerSpawnData>();

    public override void OnStartServer()
    {
        base.OnStartServer();
        for (int i = 0; i < 4; i++)
        {
            playerSelectionStates.Add(true);
        }
        playerSelectionStates.Callback += OnPlayerSelectionStatesChanged;
    }

    void OnPlayerSelectionStatesChanged(SyncList<bool>.Operation op, int index, bool oldValue, bool newValue)
    {
        switch (op)
        {
            case SyncList<bool>.Operation.OP_ADD:
                Rpc_SetButtonInteractable(convertListToArray());
                break;
            case SyncList<bool>.Operation.OP_SET:
                Debug.Log("value set");
                Rpc_SetButtonInteractable(convertListToArray());
                break;
            default:
                Debug.LogWarning("Invalid player selection state change operation.");
                break;
        }
    }

    public void changeStateRequest(NetworkIdentity netId, int index, bool value)
    {
        //request approved
        if (playerSelectionStates[index] != value)
        {
            playerSelectionStates[index] = value;
            playerSpawnDatas.Add(setPlayerSpawn(netId, index));
            netId.GetComponent<TeamSelectionController>().Rpc_SetButtonsNotInteractable();

            bool startPlayerGame = true;
            for (int i = 0; i < playerSelectionStates.Count; i++)
            {
                if (playerSelectionStates[i])
                {
                    startPlayerGame = false;
                }
            }

            if (startPlayerGame)
            {
                StartCoroutine(checkGameStartState());
            }
        }
    }

    [ClientRpc]
    public void Rpc_SetButtonInteractable(bool[] states)
    {
        setControllerButtons(states);
    }

    private IEnumerator checkGameStartState()
    {
        for (int i = 0; i < teamSelectionControllers.Count; i++)
        {
            teamSelectionControllers[i].Rpc_SetStartingGameText();
        }
        yield return new WaitForSeconds(3.0f);
        spawnPlayers();
    }

    private void spawnPlayers()
    {
        for (int i = 0; i < playerSpawnDatas.Count; i++)
        {
            spawnPlayer(playerSpawnDatas[i]);
        }
    }

    private void spawnPlayer(PlayerSpawnData playerSpawnData)
    {
        GameObject playerPrefab;
        switch(playerSpawnData.playerType)
        {
            case PlayerType.human:
                playerPrefab = humanPrefab;
                break;
            case PlayerType.ghost:
                playerPrefab = ghostPrefab;
                break;
            default:
                playerPrefab = null;
                Debug.LogError("Invalid PlayerType to be spawned.");
                break;
        }
        
        Vector3 spawnPosition;
        Quaternion spawnRotation;
        if (playerSpawnData.team == 1)
        {
            spawnPosition = team1SpawnTransform.position;
            spawnRotation = team1SpawnTransform.rotation;
        }
        else
        {
            spawnPosition = team2SpawnTransform.position;
            spawnRotation = team2SpawnTransform.rotation;
        }

        GameObject playerObject = Instantiate(playerPrefab, spawnPosition, spawnRotation);
        NetworkServer.Spawn(playerObject);
        if (NetworkServer.ReplacePlayerForConnection(playerSpawnData.networkIdentity.connectionToClient, playerObject))
        {
            NetworkServer.Destroy(playerSpawnData.networkIdentity.gameObject);
        }
    }

    private bool[] convertListToArray()
    {
        bool[] tempArray = new bool[playerSelectionStates.Count];
        for (int i = 0; i < playerSelectionStates.Count; i++)
        {
            tempArray[i] = playerSelectionStates[i];
        }
        return tempArray;
    }

    public void addTeamSelectionControllerListener(TeamSelectionController teamSelectionController)
    {
        teamSelectionControllers.Add(teamSelectionController);
    }

    public void removeTeamSelectionControllerListener(TeamSelectionController teamSelectionController)
    {
        teamSelectionControllers.Remove(teamSelectionController);
    }

    private void setControllerButtons(bool[] states)
    {
        for (int i = 0; i < teamSelectionControllers.Count; i++)
        {
            teamSelectionControllers[i].setButtons(states);
        }
    }

    private PlayerSpawnData setPlayerSpawn(NetworkIdentity netId, int index)
    {
        PlayerSpawnData playerSpawnData = new PlayerSpawnData();
        playerSpawnData.networkIdentity = netId;
        if (index == 0 || index == 1)
        {
            playerSpawnData.team = 1;
        }
        else if (index == 2 || index == 3)
        {
            playerSpawnData.team = 2;
        }
        else
        {
            Debug.LogError("invalid index for team assigning");
        }

        if (index % 2 == 0)
        {
            playerSpawnData.playerType = PlayerType.human;
        }
        else if (index % 2 == 1)
        {
            playerSpawnData.playerType = PlayerType.ghost;
        }
        else
        {
            Debug.LogError("invalid index for player spawn");
        }

        return playerSpawnData;
    }
}
