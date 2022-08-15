// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// using Photon.Pun;

// public class Health : MonoBehaviour
// {
// 	[SerializeField] private int maxHealth = 100;
// 	[SerializeField] private PlayerCanvas playerCanvas;

// 	private NetworkVariable<int> networkHealth = new(writePerm: NetworkVariableWritePermission.Owner);
// 	private int localHealth;

// 	public override void OnNetworkSpawn()
// 	{
// 		networkHealth.OnValueChanged += OnHealthChange;
// 		if (IsOwner) networkHealth.Value = maxHealth;
// 		// if (!IsOwner) OnHealthChange(networkHealth.Value, networkHealth.Value); // for new clients
// 	}

// 	void OnTriggerEnter2D(Collider2D col)
// 	{
// 		// move this code into the bullet later
// 		if (!IsOwner) return;

// 		if (col.tag == "Bullet" && col.gameObject.GetComponent<BulletMovement>().getSenderID() != gameObject.GetInstanceID())
// 		{
// 			// don't harcode lol
// 			networkHealth.Value -= 5;
// 		}
// 	}

// 	void OnHealthChange(int prevHealth, int newHealth)
// 	{
// 		localHealth = newHealth;
// 		playerCanvas.SetHealth(localHealth, maxHealth);

// 		if (localHealth <= 0)
// 		{
// 			HandleDeath();
// 		}
// 	}

// 	void HandleDeath()
// 	{
// 		if (IsOwner) UIManager.Instance.ToggleDeathPanel();
// 		Destroy(gameObject);
// 	}
// }
