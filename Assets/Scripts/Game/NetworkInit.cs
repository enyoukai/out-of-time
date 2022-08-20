using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class NetworkInit : MonoBehaviour
{
	[SerializeField] PlayerManager playerManager;
	// private PhotonView PV;

	// void Awake()
	// {
	//     PV = GetComponent<PhotonView>();
	// }
	void Start()
	{
		// if (PV.IsMine) PhotonNetwork.Instantiate(playerManager.name, Vector3.zero, Quaternion.identity);
		PhotonNetwork.Instantiate(playerManager.name, Vector3.zero, Quaternion.identity);
	}
}
