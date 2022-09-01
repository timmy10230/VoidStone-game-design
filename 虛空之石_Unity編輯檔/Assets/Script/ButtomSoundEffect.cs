using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtomSoundEffect : MonoBehaviour
{
    public GameObject ButtomSoundPrefab;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ButtomSound()
    {
        Instantiate(ButtomSoundPrefab, null);
    }
}
