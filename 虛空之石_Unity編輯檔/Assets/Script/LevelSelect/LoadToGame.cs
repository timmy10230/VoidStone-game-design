using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadToGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Load", 1);
    }

    public void Load()
    {
        SceneManager.LoadSceneAsync(2);
    }
}
