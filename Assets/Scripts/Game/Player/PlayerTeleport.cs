using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetMouseButtonDown(1))
		{
			Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			transform.position = new Vector3(pos.x, pos.y, transform.position.z);
		}

	}
}
