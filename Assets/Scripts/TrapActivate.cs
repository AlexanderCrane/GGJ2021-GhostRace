using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapActivate : MonoBehaviour
{
    Animator animationController;

    Animation anim;

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
        anim.Play();
        StartCoroutine(CountdownToStop());
    }

    private IEnumerator CountdownToStop()
    {
        yield return new WaitForSeconds(2f);
        anim.Stop();
    }
}
