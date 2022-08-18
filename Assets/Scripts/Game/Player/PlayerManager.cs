using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using TMPro;

// yoinked from rugbug
public class PlayerManager : MonoBehaviour
{
	public static PlayerManager Singleton;
	[SerializeField] private TMP_Text respawnText;
	private string respawnTextHeader = "RESPAWN IN ";
	private PhotonView PV;
	private int respawnTime = 5;

	// Start is called before the first frame update
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
		CreateController();
	}

	void CreateController()
	{
		PhotonNetwork.Instantiate("PlayerController", Vector3.zero, Quaternion.identity);
	}

	public IEnumerator RespawnCoroutine()
	{
		UIManager.Instance.ToggleDeathPanel();

		for (int i = respawnTime; i > 0; i--)
		{
			respawnText.text = i.ToString();
			Debug.Log(i);
			yield return new WaitForSecondsRealtime(1);
		}

		PhotonNetwork.Instantiate("PlayerController", Vector3.zero, Quaternion.identity);
		UIManager.Instance.ToggleDeathPanel();

	}

	public void Respawn()
	{
		StartCoroutine(RespawnCoroutine());
	}

}
