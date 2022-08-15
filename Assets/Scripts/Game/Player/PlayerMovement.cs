using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class PlayerMovement : MonoBehaviour
{

	[Header("Horizontal Movement")]
	[SerializeField] private float acceleration = 7;
	[SerializeField] private float deceleration = 7;
	[SerializeField] private float velPower = 0.9f;
	[SerializeField] private float maxHorizontalSpeed = 12f;

	[Header("Vertical Movement")]
	[SerializeField] private float maxFlyingSpeed = 12f;
	[SerializeField] private float flyingAcceleration = 10f;
	[SerializeField] private float maxFallingSpeed = 8f;
	[SerializeField] private float immediateGravity = 4f;
	[SerializeField] private float gravity = 10f;

	private Rigidbody2D _rb;
	private PhotonView _pv;
	private float horizontalInput;
	private bool verticalInput;


	private void Awake()
	{
		_rb = GetComponent<Rigidbody2D>();
		_pv = GetComponent<PhotonView>();
	}

	// Start is called before the first frame update
	void Start()
	{
		if (!_pv.IsMine)
		{
			Destroy(_rb);
			Destroy(this);
		}
	}

	// Update is called once per frame
	void Update()
	{
		horizontalInput = Input.GetAxisRaw("Horizontal");
		verticalInput = Input.GetButton("Jump");
	}

	private void FixedUpdate()
	{
		HorizontalMovement();
		FlyingMovement();

	}

	private void HorizontalMovement()
	{
		// lmao thanks dawnosaur
		float targetSpeed = horizontalInput * maxHorizontalSpeed;
		float speedDif = targetSpeed - _rb.velocity.x;
		float accelRate = (Mathf.Abs(targetSpeed) != 0) ? acceleration : deceleration;
		float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);

		_rb.AddForce(movement * Vector2.right);
	}

	private void FlyingMovement()
	{
		if (verticalInput) _rb.AddForce(Vector2.up * flyingAcceleration);
		else if (_rb.velocity.y > 0)
		{
			_rb.AddForce(Vector2.down * immediateGravity);
		}
		else
		{
			_rb.AddForce(Vector2.down * gravity);
		}
		if (_rb.velocity.y > maxFlyingSpeed) _rb.velocity = new Vector2(_rb.velocity.x, maxFlyingSpeed);
		else if (_rb.velocity.y < -maxFallingSpeed) _rb.velocity = new Vector2(_rb.velocity.x, -maxFallingSpeed);
	}
}
