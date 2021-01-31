using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class McGuffinSpawner : MonoBehaviour
{
    public GameObject[] SpawnPoints;
    public GameObject McGuffin;
    Vector3 chosenPoint;

    // Start is called before the first frame update
    void Start()
    {
        chosenPoint = SpawnPoints[Random.Range(0,SpawnPoints.Length)].transform.position;
        McGuffin.transform.position = chosenPoint;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
