using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingCharacterInitializer : MonoBehaviour
{
    public List<GameObject> objectsToSetActive;
    // Start is called before the first frame update
    void Start()
    {
        objectsToSetActive.ForEach(o => {
            o.SetActive(true);
            if (o.GetComponent<PlayerNetworkManager>() != null)
            {
                o.GetComponent<PlayerNetworkManager>().initializeLocalPlayer();
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
