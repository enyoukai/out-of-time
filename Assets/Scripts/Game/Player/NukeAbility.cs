using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class NukeAbility : MonoBehaviour
{
	private float cooldown = 15.0f;
	private float cooldownTimeElapsed = 0f;
	[SerializeField] GameObject nukePrefab;
	private PhotonView PV;
	// Start is called before the first frame update
	void Awake()
	{
		PV = GetComponent<PhotonView>();
		cooldownTimeElapsed = cooldown;
	}

	// Update is called once per frame
	void Update()
	{
		if (!PV.IsMine) return;

		// TODO: subclass cooldown ability thing?
		cooldownTimeElapsed += Time.deltaTime;

		if (Input.GetKeyDown("e") && cooldownTimeElapsed >= cooldown)
		{
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			PV.RPC(nameof(LaunchNuke), RpcTarget.All, mousePos.x, mousePos.y);
			cooldownTimeElapsed = 0f;
		}

		CanvasManager.Singleton.SetUltimateCooldown(cooldownTimeElapsed, cooldown);
	}

	[PunRPC]
	void LaunchNuke(float x, float y)
	{
		GameObject nuke = Instantiate(nukePrefab, new Vector3(x, y, 0), Quaternion.identity);
		nuke.GetComponent<Nuke>().Initialize(PV.Owner.ActorNumber);
	}
}
