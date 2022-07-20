using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
	[SerializeField] private float bulletSpeed;
	private int senderID;
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
		if (col.tag != "Bullet" && col.gameObject.GetInstanceID() != senderID)
		{
			Destroy(gameObject);
		}
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
