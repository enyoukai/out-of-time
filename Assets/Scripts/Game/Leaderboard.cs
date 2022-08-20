using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;
public class Leaderboard : MonoBehaviourPunCallbacks
{
	[SerializeField] private GameObject leaderboardItemPrefab;
	[SerializeField] private Transform leaderboardContainer;
	private Player[] playerList;
	private CanvasGroup canvasGroup;
	private string renderKey = "tab";

	void Awake()
	{
		canvasGroup = GetComponent<CanvasGroup>();
	}

	void Start()
	{
		RenderLeaderboard();
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

	public override void OnPlayerEnteredRoom(Player player)
	{
		RenderLeaderboard();
	}

	public override void OnPlayerLeftRoom(Player player)
	{
		RenderLeaderboard();
	}

	void RenderLeaderboard()
	{
		foreach (Transform playerItem in leaderboardContainer)
		{
			Destroy(playerItem.gameObject);
		}

		playerList = PhotonNetwork.PlayerList;

		foreach (Player player in playerList)
		{
			LeaderboardItem item = Instantiate(leaderboardItemPrefab, leaderboardContainer).GetComponent<LeaderboardItem>();
			item.RecordPlayer(player);
		}
	}
}
