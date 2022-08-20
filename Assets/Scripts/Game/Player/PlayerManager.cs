using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using TMPro;
using Hashtable = ExitGames.Client.Photon.Hashtable;

// yoinked from rugbug
public class PlayerManager : MonoBehaviour
{
	private PhotonView PV;
	private int respawnTime = 5;

	void Awake()
	{
		PV = GetComponent<PhotonView>();
	}

	void Start()
	{
		if (PV.IsMine)
		{
			InitProperties();
			CreateController();
		}
	}

	void CreateController()
	{
		GameObject player = PhotonNetwork.Instantiate("PlayerController", Vector3.zero, Quaternion.identity);
		player.GetComponent<PlayerManagerWrapper>().playerManager = this;
	}

	public IEnumerator RespawnCoroutine()
	{
		CanvasManager.Singleton.ToggleDeathPanel();

		for (int i = respawnTime; i > 0; i--)
		{
			CanvasManager.Singleton.SetRespawnText(i.ToString());
			yield return new WaitForSecondsRealtime(1);
		}

		CreateController();
		CanvasManager.Singleton.ToggleDeathPanel();

	}

	public void Respawn()
	{
		StartCoroutine(RespawnCoroutine());
	}

	void InitProperties()
	{
		Hashtable initTable = new Hashtable();
		initTable.Add("Kills", 0);
		initTable.Add("Deaths", 0);

		CustomProperties.SetProperty(initTable, PhotonNetwork.LocalPlayer);
	}

	public void IncrementDeaths(int killedBy)
	{
		PV.RPC(nameof(IncrementKills), PhotonNetwork.CurrentRoom.GetPlayer(killedBy), PV.Owner.ActorNumber);

		int deaths = (int)CustomProperties.GetProperty("Deaths", PhotonNetwork.LocalPlayer);
		deaths++;

		CustomProperties.SetProperty("Deaths", deaths, PhotonNetwork.LocalPlayer);
	}

	// runs on original playermanager object
	[PunRPC]
	public void IncrementKills(int killed)
	{
		if (killed == PhotonNetwork.LocalPlayer.ActorNumber) return;

		int kills = (int)CustomProperties.GetProperty("Kills", PhotonNetwork.LocalPlayer);
		kills++;

		CustomProperties.SetProperty("Kills", kills, PhotonNetwork.LocalPlayer);
	}

}
