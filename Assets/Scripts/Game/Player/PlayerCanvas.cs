using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class PlayerCanvas : MonoBehaviour
{
	[SerializeField] private Slider healthBar;
	[SerializeField] private TMP_Text usernameTMP;
	// Start is called before the first frame update
	public void SetHealth(float health, float maxHealth)
	{
		healthBar.maxValue = maxHealth;
		healthBar.value = health;
	}

	public void SetUsername(string username)
	{
		usernameTMP.text = username;
	}
}
