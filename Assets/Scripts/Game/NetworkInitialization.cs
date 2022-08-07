using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.Netcode;
using Unity.Netcode.Transports.UTP;

public class NetworkInitialization : MonoBehaviour
{
	void Start()
	{
		if (MultiplayerData.HOSTING)
		{
			NetworkManager.Singleton.GetComponent<UnityTransport>().SetConnectionData(
				MultiplayerData.IP_ADDRESS,
				MultiplayerData.PORT,
				"0.0.0.0"
			);
			NetworkManager.Singleton.StartHost();
		}
		else
		{
			NetworkManager.Singleton.GetComponent<UnityTransport>().SetConnectionData(
				MultiplayerData.IP_ADDRESS,
				MultiplayerData.PORT
			);
			NetworkManager.Singleton.StartClient();
		}

	}
}
