using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using TMPro;

public class GameLoader : MonoBehaviourPunCallbacks
{
	[SerializeField] private TMP_InputField hostRoomName;
	[SerializeField] private TMP_InputField joinRoomName;
	[SerializeField] private TMP_InputField username;
	// Start is called before the first frame update
	void Start()
	{
		PhotonNetwork.ConnectUsingSettings();
	}

	public override void OnConnectedToMaster()
	{
		PhotonNetwork.JoinLobby();
	}

	public override void OnJoinedLobby()
	{
		MenuManager.Singleton.ToggleLoadingMenu(false);
		MenuManager.Singleton.ToggleMainMenu(true);
	}

	public void CreateRoom()
	{
		PhotonNetwork.CreateRoom(hostRoomName.text);

		MenuManager.Singleton.ToggleMainMenu(false);
		MenuManager.Singleton.ToggleLoadingMenu(true);
	}
	public void JoinRoom()
	{
		PhotonNetwork.JoinRoom(joinRoomName.text);

		MenuManager.Singleton.ToggleMainMenu(false);
		MenuManager.Singleton.ToggleLoadingMenu(true);
	}
	public override void OnJoinedRoom()
	{
		PhotonNetwork.NickName = username.text;
		PhotonNetwork.LoadLevel("Game");
		Debug.Log(PhotonNetwork.CurrentRoom.Name);
	}

	public override void OnJoinRoomFailed(short returnCode, string message)
	{
		Debug.Log(message);

		MenuManager.Singleton.ToggleMainMenu(true);
		MenuManager.Singleton.ToggleLoadingMenu(false);
	}

	public override void OnCreateRoomFailed(short returnCode, string message)
	{
		Debug.Log(message);

		MenuManager.Singleton.ToggleMainMenu(true);
		MenuManager.Singleton.ToggleLoadingMenu(false);
	}

	void Update()
	{
		Debug.Log(PhotonNetwork.GetPing());
	}

}
