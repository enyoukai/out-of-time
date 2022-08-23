using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DeathRay : MonoBehaviour
{
	public UnityEvent rayDestroyEvent;
	public int sender;
	private LineRenderer LR;
	private LayerMask ignoreMask;
	private int damage = 5;
	private int width = 1;
	private float warningTime = 1.0f;
	private RaycastHit2D hit;

	private float rayLifetime = 5.0f;
	private float shakeMagnitude = 1.0f;

	void Awake()
	{
		LR = GetComponent<LineRenderer>();
		LR.startWidth = width;
		LR.endWidth = width;
		ignoreMask = LayerMask.GetMask("Projectile", "Particle");
	}

	void Start()
	{
		StartCoroutine(EnableRay());
	}

	void FixedUpdate()
	{
		float boxCastAngle = Mathf.Rad2Deg * Mathf.Atan2(transform.right.y, transform.right.x);
		// hit = CastUtilities.BoxCast(transform.position, new Vector2(0.01f, width), boxCastAngle, transform.right, 5000.0f, ~ignoreMask);
		hit = Physics2D.Raycast(transform.position, transform.right, Mathf.Infinity, ~ignoreMask);
	}

	private IEnumerator EnableRay()
	{
		yield return new WaitForSeconds(warningTime);
		StartCoroutine(RayLifetimeClock());

		CameraManager.Singleton.CameraShake(rayLifetime, shakeMagnitude, false);
		while (true)
		{
			LR.SetPosition(0, transform.position);

			if (hit.collider != null)
			{
				Vector3 hitCoords = hit.point;

				LR.SetPosition(1, hitCoords);

				if (hit.collider.tag == "Player") hit.collider.gameObject.GetComponent<Health>().TakeDamage(damage, sender);
			}

			yield return null;
		}
	}

	private IEnumerator RayLifetimeClock()
	{
		yield return new WaitForSeconds(rayLifetime);
		Destroy(gameObject);
	}

	void OnDestroy()
	{
		rayDestroyEvent.Invoke();
	}

}