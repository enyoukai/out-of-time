using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
public class HostMenu : MonoBehaviour
{
	public void SaveUsername(string username)
	{
		MultiplayerData.USERNAME = username;
	}
	public void SavePort(string port)
	{
		MultiplayerData.PORT = ushort.Parse(port);
	}

	public void HostGame()
	{
		MultiplayerData.HOSTING = true;
		MultiplayerData.IP_ADDRESS = "127.0.0.1";

		SceneManager.LoadScene("Game");
	}
}
