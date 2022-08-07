using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
public class JoinMenu : MonoBehaviour
{
	public void SaveUsername(string username)
	{
		MultiplayerData.USERNAME = username;
	}

	public void SaveIP(string IP)
	{
		MultiplayerData.IP_ADDRESS = IP;
	}

	public void SavePort(string port)
	{
		MultiplayerData.PORT = ushort.Parse(port);
	}

	public void JoinGame()
	{
		MultiplayerData.HOSTING = false;
		SceneManager.LoadScene("Game");
	}
}
