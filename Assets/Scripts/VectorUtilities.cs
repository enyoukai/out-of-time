using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorUtilities : MonoBehaviour
{
	public static void RotateWithRadius(Transform pivot, Transform orbit, float angleDeg, float radius)
	{
		float angleRad = Mathf.Deg2Rad * angleDeg;
		orbit.rotation = Quaternion.Euler(0, 0, angleDeg);
		orbit.position = new Vector3(pivot.position.x + Mathf.Cos(angleRad) * radius, pivot.position.y + Mathf.Sin(angleRad) * radius, pivot.position.z);
	}

	public static float AngleBetweenVectors(Vector3 aVec, Vector3 bVec)
	{
		Vector2 vectorBetween = (Vector2)(aVec - bVec);
		return Mathf.Rad2Deg * Mathf.Atan2(vectorBetween.y, vectorBetween.x);
	}
}
