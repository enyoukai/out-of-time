using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	private Transform playerTransform;
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (playerTransform != null)
		{
			transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, transform.position.z);
		}
	}

	public void setTarget(Transform target)
	{
		playerTransform = target;
	}
}
