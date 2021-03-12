using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class McGuffinSpawner : NetworkBehaviour
{
    public GameObject[] SpawnPoints;
    public GameObject McGuffin;
    Vector3 chosenPoint;

    // Start is called before the first frame update
    void Start()
    {
        if (isServer)
        {
            // McGuffin = GameObject.FindWithTag("mcguffin");
            chosenPoint = SpawnPoints[Random.Range(0,SpawnPoints.Length)].transform.position;
            McGuffin.transform.position = chosenPoint;
        }
    }
}
