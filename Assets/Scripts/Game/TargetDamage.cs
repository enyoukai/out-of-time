using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class TargetDamage : MonoBehaviour
{
	private bool isIn = false;
	private GameObject client;
	private int damage = 999;

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

	public void DamageCheck()
	{
		if (isIn)
		{
			client.GetComponent<Health>().ChangeHealth(-damage);
		}
	}
}
