using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
	[SerializeField] private GameObject deathPanel;
	[SerializeField] private Slider secondaryCooldown;
	[SerializeField] private Slider ultimateCooldown;

	private static CanvasManager _instance;

	public static CanvasManager Singleton { get { return _instance; } }

	private void Awake()
	{
		if (_instance != null && _instance != this)
		{
			Destroy(this.gameObject);
		}
		else
		{
			_instance = this;
		}
	}

	public void ToggleDeathPanel()
	{
		deathPanel.SetActive(!deathPanel.activeSelf);

	}

	public void SetSecondaryCooldown(float value, float maxValue)
	{
		value = Mathf.Clamp(value, 0, maxValue);
		secondaryCooldown.value = value;
		secondaryCooldown.maxValue = maxValue;
	}

	public void SetUltimateCooldown(float value, float maxValue)
	{
		value = Mathf.Clamp(value, 0, maxValue);
		ultimateCooldown.value = value;
		ultimateCooldown.maxValue = maxValue;
	}


}