using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    public enum GameMode
    {
        playerJoinMode,
        gameBoardMode,
        minigameMode
    };

    public int numPlayers = 8;
    GameMode currentGameMode = GameMode.playerJoinMode; //current game mode
    List<InputDevice> playerDevices; //index of player device is index of player
    public int currentPlayer = 0;

    PlayerInput playerInput;
    public InputMaster controls;

    private void Awake()
    {
        playerDevices = new List<InputDevice>();
        for (int i = 0; i < numPlayers; i++) {playerDevices.Add(null);} //temporary for testing

        controls = new InputMaster();
        // controls.Enable();
        controls.Player.Confirm.Enable();
        controls.Player.Confirm.performed += context => Action(context);
        controls.Player.Start.performed += context => startGame(context);
    }

    // Start is called before the first frame update
    void Start()
    {
        deviceDetectionManager();
    }
    
    public void Action(InputAction.CallbackContext context)
    {
        if (context.performed) //controls for single performed status (not started and canceled)
        {
            switch(currentGameMode)
            {
                case GameMode.playerJoinMode:
                    togglePlayerJoin(context.control.device);
                    break;
                case GameMode.gameBoardMode:
                    //do player game board input actions here
                    gameBoardMode(context.control.device);
                    break;
                case GameMode.minigameMode:
                    //do minigame controls
                    break;
                default:
                    // invalid game mode
                    break;
            }
        }
    }

    public void startGame(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            currentGameMode = GameMode.gameBoardMode;

            // start load game board scene here
            
        }
    }

    /// <summary>
    /// Move to new class that manages title screen player joining...
    /// </summary>
    /// <param name="device"></param>
    void togglePlayerJoin(InputDevice device)
    {
        int playerIndex = playerDevices.IndexOf(device);
        if (playerIndex != -1) //if the player is already joined
        {
            Debug.Log("Player joined already. Removing player " + playerIndex + " from list.");
            playerDevices[playerIndex] = null;
        }
        else
        {
            for (int i = 0; i < playerDevices.Count; i++)
            {
                if (playerDevices[i] == null)
                {
                    playerDevices[i] = device;
                    Debug.Log("Player " + i + " has joined.");
                    break;
                }
            }
        }
    }

    void gameBoardMode(InputDevice device)
    {
        int playerIndex = playerDevices.IndexOf(device);
        if (playerIndex == currentPlayer)
        {
            Debug.Log("Player " + playerIndex + " just hit confirm on game board mode");
        }
    }

    void deviceDetectionManager()
    {
        InputSystem.onDeviceChange +=
        (device, change) =>
        {
            switch (change)
            {
                case InputDeviceChange.Added:
                    // New Device.
                    Debug.Log("new device");
                    break;
                case InputDeviceChange.Disconnected:
                    Debug.Log("device disconnected");
                    // Device got unplugged.
                    break;
                case InputDeviceChange.Reconnected:
                    Debug.Log("device reconnected");
                    // Plugged back in.
                    break;
                case InputDeviceChange.Removed:
                    Debug.Log("device removed");
                    // Remove from Input System entirely; by default, Devices stay in the system once discovered.
                    break;
                default:
                    // See InputDeviceChange reference for other event types.
                    break;
            }
        };
    }
}
