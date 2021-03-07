using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 20f;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(this.transform.forward * speed * 20.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
