using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
	[SerializeField] private GameObject deathPanel;
	private static CanvasManager _instance;

	public static CanvasManager Instance { get { return _instance; } }

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


}