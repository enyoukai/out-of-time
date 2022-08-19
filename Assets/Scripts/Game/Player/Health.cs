using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class Health : MonoBehaviourPunCallbacks, IPunObservable
{
	[SerializeField] private PlayerCanvas playerCanvas;
	[SerializeField] private ParticleSystem deadEffect;
	private int maxHealth = 100;
	private int currentHealth;
	private PhotonView _pv;

	private bool dead = false;

	void Awake()
	{
		_pv = GetComponent<PhotonView>();

		if (_pv.IsMine) currentHealth = maxHealth;
	}

	void Start()
	{

	}

	public void ChangeHealth(int addedHealth)
	{
		if (dead || !_pv.IsMine) return;

		currentHealth += addedHealth;
		if (currentHealth <= 0)
		{
			dead = true;
			_pv.RPC(nameof(HandleDeath), RpcTarget.All);
		}
	}

	[PunRPC]
	void Damaged()
	{

	}

	void Update()
	{
		playerCanvas.SetHealth(currentHealth, maxHealth);
	}

	[PunRPC]
	void HandleDeath()
	{
		Instantiate(deadEffect, transform.position, Quaternion.identity);

		if (_pv.IsMine)
		{
			PlayerManager.Singleton.Respawn();

			PhotonNetwork.Destroy(_pv);
		}
		else
		{
			gameObject.SetActive(false);
		}
	}

	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.IsWriting)
		{
			stream.SendNext(currentHealth);
		}
		else
		{
			currentHealth = (int)stream.ReceiveNext();
		}

	}
}
