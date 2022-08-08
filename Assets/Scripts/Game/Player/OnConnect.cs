using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.Netcode;
using Unity.Collections;

public class OnConnect : NetworkBehaviour
{
	private NetworkVariable<FixedString64Bytes> username = new(writePerm: NetworkVariableWritePermission.Owner);

	[SerializeField] private PlayerCanvas playerCanvas;
	// Start is called before the first frame update
	public override void OnNetworkSpawn()
	{
		if (IsOwner)
		{
			username.Value = new FixedString64Bytes(MultiplayerData.USERNAME);
			RenderUsername(username.Value);
		}
		else
		{
			username.OnValueChanged += OnUsernameChanged;
		}

	}

	void OnUsernameChanged(FixedString64Bytes prev, FixedString64Bytes cur)
	{
		RenderUsername(cur);
	}

	void RenderUsername(FixedString64Bytes username)
	{
		playerCanvas.SetUsername(username.Value.ToString());

	}

}
