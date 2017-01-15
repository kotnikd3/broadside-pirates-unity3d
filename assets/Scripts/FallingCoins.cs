using UnityEngine;
using System.Collections;

public class FallingCoins : MonoBehaviour
{
	private SphereCollider sphereCollider;
	private Rigidbody rgbody;

	void OnCollisionEnter (Collision col)
	{
		//Debug.Log ("HIT");
		sphereCollider = GetComponent<SphereCollider>();
		rgbody = GetComponent<Rigidbody> ();

		rgbody.useGravity = false;
		rgbody.isKinematic = true;
		sphereCollider.isTrigger = true;
	}
}