using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;


// TODO: MAKE THESE ALL PROPERTIES LATER
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

	public void ChangeHealth(int addedHealth, int damagedBy)
	{
		if (dead || !_pv.IsMine) return;

		_pv.RPC(nameof(onDamage), RpcTarget.All);

		currentHealth += addedHealth;
		if (currentHealth <= 0)
		{
			dead = true;
			_pv.RPC(nameof(onDead), RpcTarget.All, damagedBy);
		}
	}

	[PunRPC]
	void onDamage()
	{

	}

	void Update()
	{
		playerCanvas.SetHealth(currentHealth, maxHealth);
	}

	[PunRPC]
	void onDead(int killedBy)
	{
		Instantiate(deadEffect, transform.position, Quaternion.identity);

		if (_pv.IsMine)
		{
			GetComponent<PlayerManagerWrapper>().playerManager.IncrementDeaths(killedBy);
			GetComponent<PlayerManagerWrapper>().playerManager.Respawn();

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
