using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class MovementNetwork : NetworkBehaviour
{
	private NetworkVariable<Vector2> netPos = new(writePerm: NetworkVariableWritePermission.Owner);

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (IsOwner)
		{
			netPos.Value = (Vector2)transform.position;
		}
		else
		{
			transform.position = (Vector3)netPos.Value;
		}
	}
}
