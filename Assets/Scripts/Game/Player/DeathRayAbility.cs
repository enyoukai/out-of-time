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

	private int rayLifetime = 5;
	private bool rayActive = false;

	private PhotonView PV;

	private float radius = 8;
	private float mouseAngle = 0;

	private float shakeMagnitude = 4.0f;
	void Awake()
	{
		PV = GetComponent<PhotonView>();
		cooldownTimeElapsed = cooldown;
	}

	void Start()
	{
		if (PV.IsMine) StartCoroutine(UpdateMouseAngle());
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
		ray.GetComponent<DeathRay>().sender = PV.Owner.ActorNumber;
		ray.GetComponent<DeathRay>().rayDestroyEvent.AddListener(OnRayDestroyed);

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
	// maybe move all rotation code etc into the ray itself later
	void OnRayDestroyed()
	{
		rayActive = false;
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
			// TODO: fix erroring out from latency
			mouseAngle = (float)stream.ReceiveNext();
		}
	}
}
