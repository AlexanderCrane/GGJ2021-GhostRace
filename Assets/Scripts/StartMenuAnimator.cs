using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuAnimator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Animator>().SetBool("SitandLook", true);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
