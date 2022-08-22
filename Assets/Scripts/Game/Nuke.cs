using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: projectile inherit?
public class Nuke : MonoBehaviour
{
	[SerializeField] GameObject nukeObject;
	[SerializeField] GameObject targetObject;
	[SerializeField] ParticleSystem burstEffect;

	private int sender;

	private int damage = 999;

	private float yAboveScene = 60.0f;
	private float warningTime = 0.5f;
	private float fallingTime = 2f;
	private float shakeMagnitude = 10.0f;
	private float shakeDuration = 5.0f;
	private Vector3 nukeStartPos;
	// Start is called before the first frame update
	void Start()
	{
		float xPos = transform.position.x;
		nukeObject.SetActive(true);
		nukeStartPos = new Vector3(xPos, yAboveScene, nukeObject.transform.position.z);
		nukeObject.transform.position = nukeStartPos;

		StartCoroutine(LaunchNuke(nukeStartPos, transform.position));
	}

	public void Initialize(int sender)
	{
		this.sender = sender;
	}

	IEnumerator LaunchNuke(Vector3 startPos, Vector3 targetPos)
	{
		yield return new WaitForSeconds(warningTime);

		float elapsedTime = 0.0f;

		while (elapsedTime < fallingTime)
		{
			nukeObject.transform.position = Vector3.Lerp(startPos, targetPos, elapsedTime / fallingTime);
			elapsedTime += Time.deltaTime;
			yield return null;
		}

		targetObject.GetComponent<TargetHitbox>().ApplyDamage(damage, sender);

		Instantiate(burstEffect, targetObject.transform.position, Quaternion.identity);
		CameraManager.Singleton.CameraShake(shakeDuration, shakeMagnitude);

		Destroy(gameObject);
	}

}
