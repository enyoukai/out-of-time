using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Rendering.PostProcessing;

public class PostProcessingManager : MonoBehaviour
{
	[SerializeField] private PostProcessVolume volume;
	public static PostProcessingManager Singleton;

	void Awake()
	{
		if (Singleton)
		{
			Destroy(gameObject);
			return;
		}
		Singleton = this;
	}

	// Update is called once per frame
	public void ModifyChromaticAberration(float intensity)
	{
		ChromaticAberration chromAb;
		volume.profile.TryGetSettings(out chromAb);
		chromAb.intensity.value = intensity;
	}
}
