using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "NetworkManagerScriptableObject", menuName = "ScriptableObjects/NetworkManagerScriptableObject", order = 1)]
public class NetworkManagerScriptableObject : ScriptableObject
{
    public GameNetworkManager gameNetworkManager;
    public PlayerInfoScriptableObject playerInfoScriptableObject;
    public GameObject teamSelectionManager;

    private bool _isHost = false;
    public bool isHost
    {
        get => _isHost;
        set { _isHost = value; }
    }

    public void leaveGame()
    {
        stopGame();
        SceneManager.LoadScene("TitleScreen");
    }

    public void stopGame()
    {
        gameNetworkManager.StopClient();
        isHost = false;
    }

    public void startHost()
    {
        gameNetworkManager.StartHost();
        isHost = true;
    }

    public void stopHost()
    {
        if (isHost)
        {
            gameNetworkManager.StopHost();
        }
    }

    public void startClient()
    {
        gameNetworkManager.StartClient();
    }

    public void changeNetworkAddress(string networkAddress)
    {
        gameNetworkManager.changeNetworkAddress(networkAddress);
    }

    public void loadMainLevelScene()
    {
        SceneManager.LoadScene("MainLevel");
    }

    public void loadWinLoseScene()
    {
        setPlayerInfo();

        gameNetworkManager.ServerChangeScene("WinLoseScene");
    }

    public void setPlayerInfo()
    {
        //this sets all players' info status to win or lose based on their current team.
        Debug.Log("Unimplemented set player info (win/lose) condition");
    }

    public void resetMainLevel()
    {
        gameNetworkManager.StartClient();
        gameNetworkManager.ServerChangeScene("MainLevel");
        teamSelectionManager.SetActive(true);
    }
}
