using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
	[SerializeField] private GameObject MainMenu;
	[SerializeField] private GameObject LoadingMenu;
	public static MenuManager Singleton;
	void Awake()
	{
		if (Singleton)
		{
			Destroy(gameObject);
			return;
		}
		Singleton = this;
	}

	// TODO: IMPROVE LATER
	public void ToggleMainMenu(bool active)
	{
		MainMenu.SetActive(active);
	}

	public void ToggleLoadingMenu(bool active)
	{
		LoadingMenu.SetActive(active);
	}
}
