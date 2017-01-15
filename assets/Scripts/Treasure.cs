using UnityEngine;
using System.Collections;

public class Treasure : MonoBehaviour {

	public int value = 10;
	public GameObject explosionPrefab;

	// Reference to AudioClip to play
	public AudioClip collectSFX;

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == "Player" || other.gameObject.tag == "Ladja") {
			if (GameManager.gm!=null)
			{
				// tell the game manager to Collect
				GameManager.gm.Collect (value);
			}
			
			// explode if specified
			if (explosionPrefab != null) {
				Instantiate (explosionPrefab, transform.position, Quaternion.identity);
			}

			// play effect if specified
			if (collectSFX != null) {
				AudioSource.PlayClipAtPoint (collectSFX, this.transform.position);
			}
			
			// destroy after collection
			Destroy (gameObject);
		}
	}
}
