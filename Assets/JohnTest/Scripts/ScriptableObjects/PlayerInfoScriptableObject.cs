using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInfoScriptableObject", menuName = "ScriptableObjects/PlayerInfoScriptableObject")]
public class PlayerInfoScriptableObject : ScriptableObject
{
    public TeamSelectionManager teamSelectionManager;
    private bool _hasPlayerWon = false;

    public int teamNumber {get;set;}

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
