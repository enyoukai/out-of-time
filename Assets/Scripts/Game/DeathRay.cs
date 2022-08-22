using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathRay : MonoBehaviour
{
	private LineRenderer LR;
	private LayerMask ignoreMask;

	void Awake()
	{
		LR = GetComponent<LineRenderer>();
		ignoreMask = LayerMask.GetMask("Projectile", "Particle");
	}

	void Update()
	{
		LR.SetPosition(0, transform.position);
	}
	void FixedUpdate()
	{

		// RaycastHit2D hit = Physics2D.BoxCast(transform.position, new Vector2(5, 1), Vector2.Angle(Vector2.zero, transform.right), transform.right, Mathf.Infinity, ~ignoreMask);
		RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, Mathf.Infinity, ~ignoreMask);
		if (hit.collider != null)
		{
			Vector3 hitCoords = hit.point;

			LR.SetPosition(1, hitCoords);

		}

	}
}
