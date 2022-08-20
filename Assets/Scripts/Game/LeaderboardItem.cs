using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using TMPro;
using Photon.Realtime;

public class LeaderboardItem : MonoBehaviour
{
	[SerializeField] private TMP_Text username;
	[SerializeField] private TMP_Text kills;
	[SerializeField] private TMP_Text deaths;

	private Player targetPlayer;

	public void RecordPlayer(Player player)
	{
		targetPlayer = player;
		Render();
	}

	void Render()
	{
		username.text = targetPlayer.NickName;
		if (targetPlayer.CustomProperties.ContainsKey("Deaths"))
		{
			// yeah
			deaths.text = ((int)CustomProperties.GetProperty("Deaths", targetPlayer)).ToString();
		}

		if (targetPlayer.CustomProperties.ContainsKey("Kills"))
		{
			kills.text = ((int)CustomProperties.GetProperty("Kills", targetPlayer)).ToString();
		}
	}
}
