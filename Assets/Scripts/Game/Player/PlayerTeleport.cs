using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class PlayerTeleport : MonoBehaviour
{
	private PhotonView _pv;

	private float shakeDuration = 0.5f;
	private float shakeMagnitude = 1;

	// Start is called before the first frame update
	void Awake()
	{
		_pv = GetComponent<PhotonView>();
	}

	// Update is called once per frame
	void Update()
	{
		if (!_pv.IsMine) return;

		if (Input.GetMouseButtonDown(1))
		{
			Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			_pv.RPC("Teleport", RpcTarget.All, pos.x, pos.y);
			StartCoroutine(CameraManager.Singleton.CameraShake(shakeDuration, shakeMagnitude));
		}
	}

	[PunRPC]
	void Teleport(float x, float y)
	{
		transform.position = new Vector3(x, y, transform.position.z);
	}
}
