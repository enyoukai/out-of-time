using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using TMPro;
using Photon.Realtime;

using Hashtable = ExitGames.Client.Photon.Hashtable;
public class LeaderboardItem : MonoBehaviourPunCallbacks
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

	public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
	{
		if (this.targetPlayer != targetPlayer) return;
		Render();
	}

	void Render()
	{
		username.text = targetPlayer.NickName;
		if (targetPlayer.CustomProperties.ContainsKey("Deaths"))
		{
			// yeah
			deaths.text = ((int)targetPlayer.CustomProperties["Deaths"]).ToString();
		}
	}
}
