using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSliderController : MonoBehaviour
{
    private void Start() {
        float volume = PlayerPrefs.GetFloat("Master Volume", 0.5f);
        GetComponent<Slider>().value = volume;
    }
    
    public void setVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("Master Volume", volume);
    }
}
