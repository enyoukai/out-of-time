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
		Hashtable hash = new Hashtable();
		hash.Add("Deaths", 0);
		hash.Add("Kills", 0);

		PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
	}

	public void IncrementDeaths()
	{
		int deaths = (int)PhotonNetwork.LocalPlayer.CustomProperties["Deaths"];
		deaths++;
		Hashtable deathTable = new Hashtable();
		deathTable.Add("Deaths", deaths);

		PhotonNetwork.LocalPlayer.SetCustomProperties(deathTable);
	}

}
