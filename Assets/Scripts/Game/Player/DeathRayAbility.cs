using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class DeathRayAbility : MonoBehaviour, IPunObservable
{
	[SerializeField] private GameObject deathRayObject;
	// MAKE AN ABILITY CLASS LATER
	private float cooldown = 20.0f;
	private float cooldownTimeElapsed = 0f;

	private bool rayActive = false;

	private PhotonView PV;

	private float radius = 8;
	private float mouseAngle = 0;
	void Awake()
	{
		PV = GetComponent<PhotonView>();
		cooldownTimeElapsed = cooldown;
	}

	void Start()
	{
		StartCoroutine(UpdateMouseAngle());
	}
	void Update()
	{
		if (!PV.IsMine) return;

		cooldownTimeElapsed += Time.deltaTime;

		if (Input.GetKeyDown("q") && !rayActive && cooldownTimeElapsed >= cooldown)
		{
			PV.RPC(nameof(ActivateRay), RpcTarget.All);
			cooldownTimeElapsed = 0.0f;
		}
	}

	[PunRPC]
	void ActivateRay()
	{
		rayActive = true;
		GameObject ray = Instantiate(deathRayObject, transform.position + Vector3.right, Quaternion.identity, transform);

		StartCoroutine(RotateRay(ray));
	}

	IEnumerator RotateRay(GameObject ray)
	{
		while (rayActive)
		{
			VectorUtilities.RotateWithRadius(transform, ray.transform, mouseAngle, radius);
			yield return null;
		}
	}

	IEnumerator UpdateMouseAngle()
	{
		while (true)
		{
			if (rayActive)
			{
				Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				mouseAngle = VectorUtilities.AngleBetweenVectors(mousePos, transform.position);
			}

			yield return null;
		}
	}

	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (!rayActive) return;

		if (stream.IsWriting)
		{
			stream.SendNext(mouseAngle);
		}
		else
		{
			mouseAngle = (float)stream.ReceiveNext();
		}
	}
}
