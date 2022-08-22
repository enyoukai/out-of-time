using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
	private Vector3 originalPos;

	public static CameraManager Singleton;

	[SerializeField] private AnimationCurve shakeDropoff;
	void Awake()
	{
		if (Singleton)
		{
			Destroy(gameObject);
			return;
		}
		Singleton = this;
	}

	void Start()
	{
		originalPos = transform.localPosition;
	}

	public IEnumerator CameraShakeCoroutine(float duration, float magnitude, bool ease = true)
	{
		float elapsed = 0.0f;

		while (elapsed < duration)
		{
			elapsed += Time.deltaTime;

			float curveMultiplier = 1.0f;

			if (ease)
			{
				curveMultiplier = shakeDropoff.Evaluate(elapsed / duration);
			}

			float xShake = Random.Range(-curveMultiplier, curveMultiplier) * magnitude;
			float yShake = Random.Range(-curveMultiplier, curveMultiplier) * magnitude;


			transform.localPosition = new Vector3(xShake, yShake, originalPos.z);


			yield return null;

		}

		transform.localPosition = originalPos;
	}

	public void CameraShake(float duration, float magnitude, bool ease = true)
	{
		StartCoroutine(CameraShakeCoroutine(duration, magnitude, ease));
	}
}
