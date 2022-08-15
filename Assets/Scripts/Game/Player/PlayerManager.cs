using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

// yoinked from rugbug
public class PlayerManager : MonoBehaviour
{
	private PhotonView PV;
	// Start is called before the first frame update
	void Awake()
	{
		PV = GetComponent<PhotonView>();
	}
	void Start()
	{
		if (PV.IsMine)
		{
			CreateController();
		}
	}

	void CreateController()
	{
		PhotonNetwork.Instantiate("PlayerController", Vector3.zero, Quaternion.identity);
	}
}
