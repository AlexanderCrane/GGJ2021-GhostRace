using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioListenerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("Master Volume", 0.5f);
    }
}
