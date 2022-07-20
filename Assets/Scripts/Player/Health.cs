using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.Netcode;

public class Health : NetworkBehaviour
{
	[SerializeField] private int maxHealth = 100;
	[SerializeField] private HealthBar healthBar;
	private int currentHealth;

	// Start is called before the first frame update
	void Start()
	{
		currentHealth = maxHealth;
		healthBar.SetHealth(currentHealth, maxHealth);
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (!IsOwner) return;

		if (col.tag == "Bullet" && col.gameObject.GetComponent<BulletMovement>().getSenderID() != gameObject.GetInstanceID())
		{
			UpdateHealthServerRpc(currentHealth - 5);
		}
	}

	[ServerRpc]
	void UpdateHealthServerRpc(int newHealth)
	{
		UpdateHealthClientRpc(newHealth);
	}

	[ClientRpc]
	void UpdateHealthClientRpc(int newHealth)
	{
		UpdateHealth(newHealth);
	}

	void UpdateHealth(int newHealth)
	{
		currentHealth = newHealth;
		healthBar.SetHealth(currentHealth, maxHealth);

	}
}
