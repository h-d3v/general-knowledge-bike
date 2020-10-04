using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer mainMixer;
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey("MasterVol"))
        { 
            mainMixer.SetFloat("MasterVol", PlayerPrefs.GetFloat("MasterVol"));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
