using UnityEngine;
using System.Collections;

public class Healing : MonoBehaviour {

	public int healthValue = 1;

	void OnTriggerEnter (Collider collision)
	{
		if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Ladja") {

			/*if (collision.gameObject.GetComponent<Health> () != null) {	// if the hit object has the Health script on it, deal damage
				collision.gameObject.GetComponent<Health> ().ApplyHeal(healthValue);
				Debug.Log("DA");
			} else {
				Debug.Log("NE");
			}*/
			GameManager.gm.player.GetComponent<Health>().ApplyHeal(1);
			// destroy after collection
			Destroy (gameObject);
		}
	}
}
