using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnOnHitOrTime : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CountDownToDestroy();
    }

    public void CountDownToDestroy()
    {
        StartCoroutine(Countdown());
    }

    private void OnCollisionEnter(Collision other) {
        if (other.transform.tag == "Player")
        {
            GetComponent<PlayerNetworkCommands>().Cmd_TakeDamage();
        }
        Destroy(this.gameObject);
    }

    private IEnumerator Countdown()
    {
        yield return new WaitForSeconds(10);
        Destroy(this.gameObject);
    }
}
