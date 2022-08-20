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
	// Start is called before the first frame update
	void Start()
	{
		RenderLeaderboard();
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
