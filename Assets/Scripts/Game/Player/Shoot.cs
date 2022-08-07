using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.Netcode;
public class Shoot : NetworkBehaviour
{
	[Header("Shooting Tweaks")]
	[SerializeField] private float cooldown;

	[SerializeField] private GameObject bulletPrefab;

	private float cooldownTimer = 0f;
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (!IsOwner) return;
		cooldownTimer -= Time.deltaTime;

		Vector2 playerToMouse = getPlayerToMouse();
		float mouseAngle = getMouseAngleDegrees(playerToMouse);

		if (Input.GetMouseButton(0) && cooldownTimer <= 0)
		{
			ShootBullet(mouseAngle);
			ShootServerRpc(mouseAngle);
			cooldownTimer = cooldown;

		}
	}

	[ServerRpc]
	private void ShootServerRpc(float angle)
	{
		ShootClientRpc(angle);

	}

	[ClientRpc]
	private void ShootClientRpc(float angle)
	{
		if (!IsOwner) ShootBullet(angle);
	}

	private void ShootBullet(float angle)
	{
		float angleRadians = Mathf.Deg2Rad * angle;
		Vector3 directionVector = new Vector3(Mathf.Cos(angleRadians), Mathf.Sin(angleRadians), 0).normalized;

		GameObject Bullet = Instantiate(bulletPrefab, transform.position + directionVector, Quaternion.Euler(0, 0, angle));
		Bullet.GetComponent<BulletMovement>().setSenderID(gameObject.GetInstanceID());
	}

	private float getMouseAngleDegrees(Vector2 playerToMouse)
	{
		return Mathf.Rad2Deg * Mathf.Atan2(playerToMouse.y, playerToMouse.x);
	}

	private Vector2 getPlayerToMouse()
	{
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 playerToMouse = (Vector2)(mousePos - transform.position);

		return playerToMouse;
	}
}
