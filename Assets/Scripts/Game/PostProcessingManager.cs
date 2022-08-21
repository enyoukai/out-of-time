using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


public class PostProcessingManager : MonoBehaviour
{
	private Volume volume;
	private Bloom bloom;
	private ChromaticAberration chromAb;
	public static PostProcessingManager Singleton;

	void Awake()
	{
		if (Singleton)
		{
			Destroy(gameObject);
			return;
		}
		Singleton = this;

		volume = GetComponent<Volume>();
		volume.profile.TryGet(out bloom);
		volume.profile.TryGet(out chromAb);

	}

	public void ModifyChromaticAberration(float intensity)
	{
		chromAb.intensity.value = intensity;
	}
}
