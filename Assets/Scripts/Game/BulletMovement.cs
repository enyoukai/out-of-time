using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class BulletMovement : MonoBehaviour
{
	[SerializeField] private float bulletSpeed;
	[SerializeField] private ParticleSystem bulletEffect;
	private int senderID;
	private int damage = 5;
	private Rigidbody2D _rb;

	void Awake()
	{
		_rb = GetComponent<Rigidbody2D>();
	}
	// Start is called before the first frame update
	void Start()
	{
		float thetaRad = Mathf.Deg2Rad * transform.rotation.eulerAngles.z;
		float velocityX = Mathf.Cos(thetaRad);
		float velocityY = Mathf.Sin(thetaRad);

		_rb.velocity = new Vector2(velocityX, velocityY) * bulletSpeed;
	}

	// Update is called once per frame
	void Update()
	{
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if ((col.tag == "Player" && col.gameObject.GetComponent<PhotonView>().Owner.ActorNumber == senderID) || col.tag == "Bullet")
		{
			return;
		}

		if (col.tag == "Player" && col.gameObject.GetComponent<PhotonView>().IsMine)
		{
			col.gameObject.GetComponent<Health>().ChangeHealth(-damage);
		}

		Instantiate(bulletEffect, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}

	public void setSenderID(int ID)
	{
		senderID = ID;
	}

	public int getSenderID()
	{
		return senderID;
	}
}
