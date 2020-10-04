using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System;
public class OptionsMenu : MonoBehaviour
{
    public Toggle tglVsync, tglFullSc;

    public Text lblRes;

    public ResItem[] resolutions;

    public int selectedResolution;

    public AudioMixer mainMixer;

    public Slider masterSldr, musicSldr, sfxSldr;

    public Text lblMaster, lblMusic, lblSfx;

    public AudioSource sfxLoop;

    // Start is called before the first frame update
    void Start()
    {
        
        tglFullSc.isOn=Screen.fullScreen;

        if (QualitySettings.vSyncCount==0){
            tglVsync.isOn=false;
        } else {
            tglVsync.isOn=true;
        }

        bool foundRes=false;
        foreach(ResItem resItem in resolutions){
            if(Screen.width==resItem.horizontal && Screen.height==resItem.vertical)
            {
                foundRes=true;
                selectedResolution=Array.IndexOf(resolutions,resItem);
                UpdateLblRes();
                break;
            }
        }

        if(!foundRes){
            lblRes.text=Screen.width.ToString()+" x "+Screen.height.ToString();
        }

        //met le master vol comme l'utilisateur l'as sauvegarder
        if(PlayerPrefs.HasKey("MasterVol"))
        { 
            mainMixer.SetFloat("MasterVol", PlayerPrefs.GetFloat("MasterVol"));
            masterSldr.value=PlayerPrefs.GetFloat("MasterVol");
        }

        //affiche master volume actuel
        lblMaster.text=(masterSldr.value+80).ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void ResLeft()
    {
        selectedResolution--;
        if (selectedResolution < 0) selectedResolution = 0;
        UpdateLblRes();
     
    }

    public void ResRight()
    {
        selectedResolution++;
        if (selectedResolution > resolutions.Length -1 )
        {
            selectedResolution = resolutions.Length - 1;
        }

    
        UpdateLblRes();
    }

    public void UpdateLblRes(){
        lblRes.text=resolutions[selectedResolution].horizontal.ToString();
        lblRes.text+=" x "+resolutions[selectedResolution].vertical.ToString();

    }
    public void ApplyGraphics()
    {
    
        //vsync
        if (tglVsync.isOn)
        {
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
        }
        //res and fullscreen
        Screen.SetResolution(resolutions[selectedResolution].horizontal,resolutions[selectedResolution].horizontal,tglFullSc.isOn);
    }

    public void SetMasterVol()
    {
        lblMaster.text=(masterSldr.value+80).ToString();
        mainMixer.SetFloat("MasterVol", masterSldr.value);
        PlayerPrefs.SetFloat("MasterVol",masterSldr.value);
    }

    public void SetMusicVol()
    {
        lblMusic.text=(musicSldr.value+80).ToString();
        mainMixer.SetFloat("MusicVol", musicSldr.value);
        PlayerPrefs.SetFloat("MusicVol",musicSldr.value);
    }

    public void SetSfxVol()
    {
        lblSfx.text=(sfxSldr.value+80).ToString();
        mainMixer.SetFloat("SfxVol", sfxSldr.value);
        PlayerPrefs.SetFloat("SfxVol",sfxSldr.value);
    }

    public void PlaySfxLoop()
    {
        sfxLoop.Play();
    }

    public void StopSfxLoop()
    {
        sfxLoop.Stop();  
    }

}

[System.Serializable]
public class ResItem
{
    public int horizontal, vertical;
}

