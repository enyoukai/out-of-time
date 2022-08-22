using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class TargetHitbox : MonoBehaviour
{
	private bool isIn = false;
	private GameObject client;

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.GetComponent<PhotonView>().IsMine)
		{
			isIn = true;
			client = col.gameObject;
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject.GetComponent<PhotonView>().IsMine)
		{
			isIn = false;
		}
	}

	public void ApplyDamage(int damage, int sender)
	{
		if (isIn)
		{
			client.GetComponent<Health>().TakeDamage(damage, sender);
		}
	}
}
