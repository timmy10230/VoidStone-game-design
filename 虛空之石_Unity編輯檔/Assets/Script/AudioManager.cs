using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    public AudioMixer audioMixer;
    public AudioMixer audioBackgound;

    public void SetMasterVolume(float volume)  //控制音量參數
    {
        audioMixer.SetFloat("MasterVolume", volume);
    }

    public void SetBackgoundVolume(float volume)  //控制音量參數
    {
        audioBackgound.SetFloat("BackgroundVolume", volume);
    }

}
