using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
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

	public IEnumerator CameraShake(float duration, float magnitude)
	{
		Debug.Log("here");
		Vector3 originalPos = transform.localPosition;
		float elapsed = 0.0f;

		while (elapsed < duration)
		{
			elapsed += Time.deltaTime;

			float curveMultiplier = shakeDropoff.Evaluate(elapsed / duration);

			float xShake = Random.Range(-curveMultiplier, curveMultiplier) * magnitude;
			float yShake = Random.Range(-curveMultiplier, curveMultiplier) * magnitude;


			transform.localPosition = new Vector3(xShake, yShake, originalPos.z);


			yield return null;

		}

		transform.localPosition = originalPos;
	}

	public void CameraShakeWrapper(float duration, float magnitude)
	{
		StartCoroutine(CameraShake(duration, magnitude));
	}
}
