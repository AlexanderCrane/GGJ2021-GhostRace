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
        GameObject McGuffinPickup = GameObject.Instantiate(McGuffin, transform.TransformPoint(Vector3.forward * 1.1f) , transform.rotation);
        McGuffinPickup.transform.position = chosenPoint;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
