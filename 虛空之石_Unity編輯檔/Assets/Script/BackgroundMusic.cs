using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BackgroundMusic : MonoBehaviour
{
    public AudioSource backgroundMusic1;
    public AudioSource backgroundMusic2;

    private void Update()
    {
        if(backgroundMusic1 == null)
        {
            backgroundMusic1 = GameObject.Find("Music").GetComponent<AudioSource>();
        }
    }

    public void Awake()
    {   
        backgroundMusic2.Pause();
    }
    public void PauseMusic1()
    {
        if(backgroundMusic1 != null)
            backgroundMusic1.Pause();
    }

    public void PauseMusic2()
    {
        backgroundMusic2.Pause();
    }

    public void UnPauseMusic1()
    {
        if (backgroundMusic1 != null)
            backgroundMusic1.UnPause();
    }

    public void UnPauseMusic2()
    {
        backgroundMusic2.UnPause();
    }
}
