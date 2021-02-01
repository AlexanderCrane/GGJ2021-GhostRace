using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class GameNetworkManager : NetworkManager
{
    public bool testingMode;

    public GameObject ghostPrefab;
    public GameObject humanPrefab;
    public Transform team1SpawnPoint;
    public Transform team2SpawnPoint;

    List<NetworkConnection> connections = new List<NetworkConnection>();
    List<NetworkConnection> temporaryConnections;

    private LobbyManager lobbyManager;

    private NetworkConnection thisConnection;

    public static void ReplacePlayer(NetworkConnection conn, GameObject newPrefab)
    {
        // Cache a reference to the current player object
        // GameObject oldPlayer = conn.identity.gameObject;

        // Instantiate the new player object and broadcast to clients
        NetworkServer.ReplacePlayerForConnection(conn, newPrefab);

        // Remove the previous player object that's now been replaced
        // NetworkServer.Destroy(oldPlayer);
    }

    /*
    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
        Debug.Log("before connection add");
        connections.Add(conn);
        
        if (lobbyManager != null)
        {
            lobbyManager.setConnectionText();
        }
        thisConnection = conn;
    }

    public override void Start() {
        base.Start();
        NetworkConnection conn = thisConnection;
        
        Debug.Log("before text set");
        Debug.Log(GameObject.FindWithTag("lobby").GetComponent<LobbyManager>());
        
        
        lobbyManager = GameObject.FindWithTag("lobby").GetComponent<LobbyManager>();
        lobbyManager.setConnectionText();
        Debug.Log("attempting to run game");
        attemptRunGame(conn);
    }

    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);
        connections.Remove(conn);
    }

    private IEnumerator attemptRunGame(NetworkConnection conn)
    {
        if (NetworkServer.connections.Count == 4)
        {
            lobbyManager.setStartingText();
            yield return new WaitForSeconds(3.0f);
            startGame(conn);
        }
        else if (testingMode && NetworkServer.connections.Count == 2)
        {
            lobbyManager.setStartingText();
            yield return new WaitForSeconds(3.0f);
            temporaryConnections = new List<NetworkConnection>(connections);

            createTeam1();
        }
    }

    public void startGame(NetworkConnection conn)
    {
        temporaryConnections = new List<NetworkConnection>(connections);
        team1SpawnPoint = startPositions[0];
        team2SpawnPoint = startPositions[1];
        createTeam1();
        createTeam2();
    }

    private void createTeam1()
    {
        createHuman(team1SpawnPoint);
        createGhost(team1SpawnPoint);
    }

    private void createTeam2()
    {
        createHuman(team2SpawnPoint);
        createGhost(team2SpawnPoint);
    }

    private void createHuman(Transform spawnPoint)
    {
        int index1 = Random.Range(0, temporaryConnections.Count - 1);
        NetworkConnection tempConnection1 = temporaryConnections[index1];
        temporaryConnections.RemoveAt(index1);
        GameObject human = Instantiate(humanPrefab, spawnPoint.position, Quaternion.identity);
        NetworkServer.Spawn(human, tempConnection1);
    }

    private void createGhost(Transform spawnPoint)
    {
        int index1 = Random.Range(0, temporaryConnections.Count - 1);
        NetworkConnection tempConnection1 = temporaryConnections[index1];
        temporaryConnections.RemoveAt(index1);
        GameObject ghost = Instantiate(ghostPrefab, spawnPoint.position, Quaternion.identity);
        NetworkServer.Spawn(ghost, tempConnection1);
    }
    */
}
