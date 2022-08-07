using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
	[SerializeField] private GameObject deathPanel;
	private static UIManager _instance;

	public static UIManager Instance { get { return _instance; } }

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