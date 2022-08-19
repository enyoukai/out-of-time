using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nuke : MonoBehaviour
{
	[SerializeField] GameObject nukeObject;
	[SerializeField] GameObject targetObject;
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

		StartCoroutine(NukeFalling(nukeStartPos, transform.position));
	}


	IEnumerator NukeFalling(Vector3 startPos, Vector3 targetPos)
	{
		yield return new WaitForSeconds(warningTime);

		float elapsedTime = 0.0f;

		while (elapsedTime < fallingTime)
		{
			nukeObject.transform.position = Vector3.Lerp(startPos, targetPos, elapsedTime / fallingTime);
			elapsedTime += Time.deltaTime;
			yield return null;
		}

		targetObject.GetComponent<TargetDamage>().DamageCheck();

		CameraManager.Singleton.CameraShakeWrapper(shakeDuration, shakeMagnitude);

		Destroy(gameObject);
	}

}
