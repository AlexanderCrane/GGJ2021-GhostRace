using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointController : MonoBehaviour
{
    public SpawnManagerScriptableObject spawnManagerScriptableObject;

    // Start is called before the first frame update
    void Start()
    {
        spawnManagerScriptableObject.addSpawnPoint(this.transform);
    }
}
