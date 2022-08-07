using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
	[SerializeField] private Slider _slider;
	// Start is called before the first frame update
	public void SetHealth(float health, float maxHealth)
	{
		_slider.maxValue = maxHealth;
		_slider.value = health;
	}

	// Update is called once per frame
	void Update()
	{

	}
}
