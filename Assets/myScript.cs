using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class myScript : MonoBehaviour
{
    AudioSource audio;
    public AudioClip clip;
    public Slider slider;
    private void Start()
    {

        audio = GetComponent<AudioSource>();
        slider.value = audio.volume;
    
        slider.onValueChanged.AddListener(delegate { updateSlider(); });
    }
    public void playButtons()
    {
        audio.PlayOneShot(clip);
    }
    public void updateSlider()
    {
        audio.volume = slider.value;
    }
}
