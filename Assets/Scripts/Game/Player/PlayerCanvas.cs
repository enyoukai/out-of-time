using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using TMPro;

public class PlayerCanvas : MonoBehaviour
{
	[SerializeField] private Slider healthBar;
	[SerializeField] private TMP_Text usernameTMP;
	[SerializeField] private PhotonView PV;
	public void SetHealth(float health, float maxHealth)
	{
		healthBar.maxValue = maxHealth;
		healthBar.value = health;
	}

	void Start()
	{
		usernameTMP.text = PV.Owner.NickName;

	}
}
