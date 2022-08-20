using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class Leaderboard : MonoBehaviourPunCallbacks
{
	[SerializeField] private GameObject leaderboardItemPrefab;
	[SerializeField] private Transform leaderboardContainer;
	private CanvasGroup canvasGroup;
	private string renderKey = "tab";


	void Awake()
	{
		canvasGroup = GetComponent<CanvasGroup>();
	}

	void Start()
	{
		RenderLeaderboard(PhotonNetwork.PlayerListOthers);
	}

	void Update()
	{
		if (Input.GetKeyDown(renderKey))
		{
			canvasGroup.alpha = 1f;
		}
		else if (Input.GetKeyUp(renderKey))
		{
			canvasGroup.alpha = 0f;
		}

	}

	public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
	{
		if (changedProps.ContainsKey("Deaths") || changedProps.ContainsKey("Kills"))
		{
			RenderLeaderboard(PhotonNetwork.PlayerList);
		}
	}

	public override void OnPlayerLeftRoom(Player player)
	{
		RenderLeaderboard(PhotonNetwork.PlayerList);
	}

	void RenderLeaderboard(Player[] playerList)
	{
		foreach (Transform playerItem in leaderboardContainer)
		{
			Destroy(playerItem.gameObject);
		}

		Array.Sort(playerList, LeaderboardSort);

		foreach (Player player in playerList)
		{
			LeaderboardItem item = Instantiate(leaderboardItemPrefab, leaderboardContainer).GetComponent<LeaderboardItem>();
			item.RecordPlayer(player);
		}
	}

	int LeaderboardSort(Player p1, Player p2)
	{
		int p1Kills = (int)CustomProperties.GetProperty("Kills", p1);
		int p2Kills = (int)CustomProperties.GetProperty("Kills", p2);

		int p1Deaths = (int)CustomProperties.GetProperty("Deaths", p1);
		int p2Deaths = (int)CustomProperties.GetProperty("Deaths", p2);

		if (p1Kills > p2Kills) return -1;
		else if (p1Kills < p2Kills) return 1;
		else
		{
			if (p1Deaths > p2Deaths) return 1;
			else if (p1Deaths < p2Deaths) return -1;
			else return 0;
		}

	}
}
