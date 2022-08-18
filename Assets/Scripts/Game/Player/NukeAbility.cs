using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class NukeAbility : MonoBehaviour
{
	[SerializeField] GameObject nukePrefab;
	private PhotonView PV;
	// Start is called before the first frame update
	void Awake()
	{
		PV = GetComponent<PhotonView>();
	}

	// Update is called once per frame
	void Update()
	{
		if (!PV.IsMine) return;

		if (Input.GetKeyDown("e"))
		{
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			PV.RPC(nameof(LaunchNuke), RpcTarget.All, mousePos.x, mousePos.y);
		}
	}

	[PunRPC]
	void LaunchNuke(float x, float y)
	{
		Debug.Log(x + ", " + y);
		Instantiate(nukePrefab, new Vector3(x, y, 0), Quaternion.identity);
	}
}
