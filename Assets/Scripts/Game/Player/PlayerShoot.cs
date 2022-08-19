using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
public class PlayerShoot : MonoBehaviour
{
	private float cooldown = 0.3f;
	[SerializeField] private GameObject bulletObject;

	private PhotonView _pv;

	private float cooldownTimer = 0f;
	// Start is called before the first frame update
	void Awake()
	{
		_pv = GetComponent<PhotonView>();
	}

	// Update is called once per frame
	void Update()
	{
		if (!_pv.IsMine) return;

		cooldownTimer -= Time.deltaTime;

		Vector2 playerToMouse = getPlayerToMouse();
		float mouseAngle = getMouseAngleDegrees(playerToMouse);

		if (Input.GetMouseButton(0) && cooldownTimer <= 0)
		{
			_pv.RPC(nameof(ShootBullet), RpcTarget.All, mouseAngle, transform.position.x, transform.position.y);
			cooldownTimer = cooldown;

		}
	}

	[PunRPC]
	private void ShootBullet(float angle, float x, float y)
	{
		float angleRadians = Mathf.Deg2Rad * angle;
		Vector3 directionVector = new Vector3(Mathf.Cos(angleRadians), Mathf.Sin(angleRadians), 0).normalized;

		GameObject bullet = Instantiate(bulletObject, new Vector3(x, y, 0) + directionVector, Quaternion.Euler(0, 0, angle));
		bullet.GetComponent<BulletMovement>().setSenderID(_pv.Owner.ActorNumber);
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
