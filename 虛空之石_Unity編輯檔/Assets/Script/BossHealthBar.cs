using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public Slider slider;

	public FireWorm fireWorm;
	//public bool isInvulnerable = false;

	void Start()
	{
		slider.maxValue = fireWorm.Hp;
	}

	// Update is called once per frame
	void Update()
	{
		slider.value = fireWorm.Hp;
	}

	
}


