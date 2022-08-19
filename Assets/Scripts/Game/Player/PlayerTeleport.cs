using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class PlayerTeleport : MonoBehaviour
{
	[SerializeField] private ParticleSystem teleportEffect;
	[SerializeField] private Color teleportStartColor;
	[SerializeField] private Color teleportEndColor;

	private PhotonView _pv;

	private float shakeDuration = 0.5f;
	private float shakeMagnitude = 2;

	private float aberrationDuration = 0.3f;
	[SerializeField] AnimationCurve aberrationCurve;

	private float cooldown = 5.0f;
	private float cooldownTimeElapsed = 0f;

	// Start is called before the first frame update
	void Awake()
	{
		_pv = GetComponent<PhotonView>();
		cooldownTimeElapsed = cooldown;
	}

	// Update is called once per frame
	void Update()
	{
		if (!_pv.IsMine) return;

		cooldownTimeElapsed += Time.deltaTime;

		if (Input.GetMouseButtonDown(1) && cooldownTimeElapsed >= cooldown)
		{
			Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			_pv.RPC("Teleport", RpcTarget.All, pos.x, pos.y);
			cooldownTimeElapsed = 0.0f;
		}

		CanvasManager.Singleton.SetSecondaryCooldown(cooldownTimeElapsed, cooldown);
	}

	[PunRPC]
	void Teleport(float x, float y)
	{
		// TODO: move effect code somewhere else later
		var teleportEffectMain = teleportEffect.main;

		teleportEffectMain.startColor = teleportStartColor;

		Instantiate(teleportEffect, transform.position, Quaternion.identity);

		transform.position = new Vector3(x, y, transform.position.z);

		teleportEffectMain.startColor = teleportEndColor;

		Instantiate(teleportEffect, transform.position, Quaternion.identity);

		if (_pv.IsMine)
		{
			StartCoroutine(TeleportEffect());
		}
	}

	IEnumerator TeleportEffect()
	{
		StartCoroutine(CameraManager.Singleton.CameraShake(shakeDuration, shakeMagnitude));

		float elapsedTime = 0.0f;

		while (elapsedTime < aberrationDuration)
		{
			float intensity = aberrationCurve.Evaluate(elapsedTime / aberrationDuration);
			PostProcessingManager.Singleton.ModifyChromaticAberration(intensity);

			elapsedTime += Time.deltaTime;
			yield return null;
		}

	}
}
