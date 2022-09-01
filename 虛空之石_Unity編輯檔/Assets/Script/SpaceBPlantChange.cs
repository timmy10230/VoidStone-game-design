using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceBPlantChange : MonoBehaviour
{
    public List<GameObject> PlantsObjects;
    Animator ani;
    public static SpaceBPlantChange _instance;
    private void Awake()
    {
        _instance = this;
    }

    public void SpaceChange()
    {
        foreach (var PlantsObject in PlantsObjects)
        {
            ani = PlantsObject.GetComponent<Animator>();
            ani.SetTrigger("ObjectDisapper");
        }
    }
}
