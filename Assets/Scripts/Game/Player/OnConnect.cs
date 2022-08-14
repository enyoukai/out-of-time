using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.Netcode;
using Unity.Collections;

public class OnConnect : NetworkBehaviour
{
	private NetworkVariable<FixedString64Bytes> username = new(writePerm: NetworkVariableWritePermission.Owner);

	[SerializeField] private PlayerCanvas playerCanvas;

	public override void OnNetworkSpawn()
	{
		if (IsOwner)
		{
			username.Value = new FixedString64Bytes(MultiplayerData.USERNAME);
		}
	}

	// fix later
	void Update()
	{
		playerCanvas.SetUsername(username.Value.ToString());
	}

}
