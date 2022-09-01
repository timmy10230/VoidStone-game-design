using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroller : MonoBehaviour {

    public float scrollSpeed;
    private float tileSizeZ;

    private Vector3 startPosition;

    void Start()
    {
        Time.timeScale = 1;
        startPosition = transform.position;
        tileSizeZ = 1495;
    }

    void Update()
    {
        float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizeZ);
        transform.position = startPosition + Vector3.left * newPosition;
    }
}
