using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInfoScriptableObject", menuName = "ScriptableObjects/PlayerInfoScriptableObject")]
public class PlayerInfoScriptableObject : ScriptableObject
{
    private bool _hasPlayerWon = false;

    //Properties
    public bool hasPlayerWon
    {
        get => _hasPlayerWon;
        set
        {
            _hasPlayerWon = value;
        }
    }
}
