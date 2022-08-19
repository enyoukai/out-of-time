using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class PlayerTeleport : MonoBehaviour
{
	private PhotonView _pv;

	private float shakeDuration = 0.5f;
	private float shakeMagnitude = 2;

	private float aberrationDuration = 0.3f;
	[SerializeField] AnimationCurve aberrationCurve;

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
		}
	}

	[PunRPC]
	void Teleport(float x, float y)
	{
		PostProcessingManager.Singleton.ModifyChromaticAberration(1.0f);
		transform.position = new Vector3(x, y, transform.position.z);

		if (_pv.IsMine)
		{
			StartCoroutine(CameraManager.Singleton.CameraShake(shakeDuration, shakeMagnitude));
			StartCoroutine(TeleportEffect());
		}
	}

	IEnumerator TeleportEffect()
	{
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
