using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum KeyMap
{
    startTurn,
    loadMainScene,
    loadSmashHammer,
    player1Action1,
    player2Action1,
    player3Action1,
    player4Action1,
    pauseGame,
    back
};

public class InputManager : MonoBehaviour
{
    Dictionary<KeyMap, KeyCode> inputDictionary; 
    // Start is called before the first frame update
    void Start()
    {
        inputDictionary = new Dictionary<KeyMap, KeyCode>();
        inputDictionary.Add(KeyMap.startTurn, KeyCode.Space);
        inputDictionary.Add(KeyMap.loadMainScene, KeyCode.Z);
        inputDictionary.Add(KeyMap.loadSmashHammer, KeyCode.Slash);
        inputDictionary.Add(KeyMap.player1Action1, KeyCode.Alpha1);
        inputDictionary.Add(KeyMap.player2Action1, KeyCode.Alpha2);
        inputDictionary.Add(KeyMap.player3Action1, KeyCode.Alpha3);
        inputDictionary.Add(KeyMap.player4Action1, KeyCode.Alpha4);
        inputDictionary.Add(KeyMap.pauseGame, KeyCode.Escape);
        inputDictionary.Add(KeyMap.back, KeyCode.Backspace);
    }

    public KeyCode getKey(KeyMap key)
    {
        return inputDictionary[key];
    }
}
