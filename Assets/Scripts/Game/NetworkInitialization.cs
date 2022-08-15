using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class NetworkInitialization : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{
		PhotonNetwork.Instantiate("PlayerManager", Vector3.zero, Quaternion.identity);
	}

	// Update is called once per frame
	void Update()
	{

	}
}
