using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndGameMenu : MonoBehaviour
{
    public NetworkManagerScriptableObject networkManagerScriptableObject;
    public PlayerInfoScriptableObject playerInfoScriptableObject;

    public TMP_Text textMeshPro;

    private void Start() {
        textMeshPro.text = playerInfoScriptableObject.hasPlayerWon ? "You win!" : "You lose!";
        networkManagerScriptableObject.gameNetworkManager.StopClient();
    }

    public void playAgain()
    {
        networkManagerScriptableObject.resetMainLevel();
    }

    public void titleScreen()
    {
        SceneManager.LoadScene("TitleScreen");
        networkManagerScriptableObject.stopHost();
    }
}
