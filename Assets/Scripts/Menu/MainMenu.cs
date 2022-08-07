using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public void HostGame()
	{
		MultiplayerData.IP_ADDRESS = "127.0.0.1";
		MultiplayerData.PORT = 12345;
		MultiplayerData.HOSTING = true;
		SceneManager.LoadScene("Game");
	}
}
