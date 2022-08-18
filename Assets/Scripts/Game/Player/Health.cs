using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class Health : MonoBehaviourPunCallbacks, IPunObservable
{
	[SerializeField] private PlayerCanvas playerCanvas;
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

	void OnTriggerEnter2D(Collider2D col)
	{
		// move this code into the bullet later
		if (!_pv.IsMine) return;

		if (col.tag == "Bullet" && col.gameObject.GetComponent<BulletMovement>().getSenderID() != _pv.Owner.ActorNumber)
		{
			ChangeHealth(-5);
		}
	}

	public void ChangeHealth(int addedHealth)
	{
		if (dead) return;

		currentHealth += addedHealth;
		if (currentHealth <= 0)
		{
			dead = true;
			HandleDeath();
		}
	}

	void Update()
	{
		playerCanvas.SetHealth(currentHealth, maxHealth);
	}

	void HandleDeath()
	{

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
