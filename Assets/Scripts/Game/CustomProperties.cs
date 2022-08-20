using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using Hashtable = ExitGames.Client.Photon.Hashtable;

public class CustomProperties : MonoBehaviour
{
	public static object GetProperty(string property, Player p)
	{
		return p.CustomProperties[property];

	}
	public static void SetProperty(string property, int value, Player p)
	{
		Hashtable hash = new Hashtable();
		hash.Add(property, value);

		p.SetCustomProperties(hash);
	}

	public static void SetProperty(Hashtable hash, Player p)
	{
		p.SetCustomProperties(hash);
	}
}
