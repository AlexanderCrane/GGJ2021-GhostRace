using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnManagerScriptableObject", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]
public class SpawnManagerScriptableObject : ScriptableObject, ISerializationCallbackReceiver
{
    public List<Transform> spawnPoints = new List<Transform>();

    public void addSpawnPoint(Transform transform)
    {
        spawnPoints.Add(transform);
    }

    public Transform getSpawnPoint(int index)
    {
        if(index >= spawnPoints.Count)
        {
            Debug.LogError("Given index too large for spawnPoints");
            return null;
        }
        else
        {
            return spawnPoints[index];
        }
    }

    public void OnAfterDeserialize()
    {
        spawnPoints.Clear();
    }

    void ISerializationCallbackReceiver.OnBeforeSerialize()
    {
    }
}
