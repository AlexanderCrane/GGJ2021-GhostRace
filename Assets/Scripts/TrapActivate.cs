using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class TrapActivate : NetworkBehaviour
{
    Animator animationController;

    Animation anim;

    bool canActivate = true;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animation>();
        // animationController = GetComponent<Animator>();   
        // animationController.SetBool("Activated", true);
        anim.Play();
        StartCoroutine(CountdownToStop());
    }

    public void Activate()
    {
        canActivate = false;
        anim.Play();
        StartCoroutine(CountdownToStop());
    }

    private IEnumerator CountdownToStop()
    {
        yield return new WaitForSeconds(2f);
        anim.Stop();
        yield return new WaitForSeconds(2f);
        canActivate = true;
    }

    [ClientRpc]
    public void Rpc_Activate()
    {
        if (canActivate)
        {
            Activate();
        }
    }
}
