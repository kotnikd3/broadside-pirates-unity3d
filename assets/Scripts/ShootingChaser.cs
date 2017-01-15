using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(CharacterController))]

public class ShootingChaser : MonoBehaviour {
	
	public float speed = 20.0f;
	public float rotationSpeed = 2.0f;
	public float minDist = 1f;
	public float maxDist = 100f;
	public Transform player;
	public float shootingTime = 1.0f;
	public float shootingPower = 10.0f;
	public GameObject projectile;
	// Reference to AudioClip to play
	public AudioClip shootSFX;
	public GameObject[] spawnObjects; // what prefabs to spawn
	public Transform shootingSource;


	private Transform target;
	private int targetIndex;
	private bool followingPlayer;
	private float lastUpdate;


	public bool pathsSet;
	// Use this for initialization
	void Start () 
	{
		// if no target specified, assume the player
		if (player == null) {
			if (GameObject.FindWithTag ("Ladja") != null)
			{
				player = GameObject.FindWithTag ("Ladja").GetComponent<Transform>();
			}
		}

		targetIndex = 0;
		followingPlayer = true;
		lastUpdate = Time.time;
	}
	
	// Update is called once per frame
	void Update () 
		{
			if (player == null)
				return;

			if (spawnObjects.Length > 0) {
			/*if (target == null && spawnObjects.Length > 0) {
				target = spawnObjects[0].GetComponent<Transform>();
			} else {
				return;
			}*/
			float playerDistance = Vector3.Distance (transform.position, player.position);
			
			// Ce je razdalja do igralca manjsa od najvecje dovoljene, potem za tarco postavi igralca.
			if (playerDistance <= maxDist) {
				target = player;
				followingPlayer = true;
			} else {
				// Ce ladja prej ni sledila igralcu, naj gre proti naslednji tarci.
				if (!followingPlayer) {
					target = spawnObjects [targetIndex % spawnObjects.Length].GetComponent<Transform> ();
					
					if (Vector3.Distance (transform.position, target.position) < 5)
						targetIndex += 1;
				} else {
					// Ce je ladja prej sledila igralcu in je ta sel izven njenega dometa, potem sledi najblizji tarci.
					float minimumDistance = float.MaxValue;
					
					// Najdi najblizjo tarco.
					for (int i = 0; i < spawnObjects.Length - 1; i++) {
						Transform target_tmp = spawnObjects [i].GetComponent<Transform> ();
						float distance_tmp = Vector3.Distance (transform.position, target_tmp.position);
						
						if (distance_tmp < minimumDistance) {
							minimumDistance = distance_tmp;
							targetIndex = i;
						}
					}
					target = spawnObjects [targetIndex].GetComponent<Transform> ();
					followingPlayer = false;
				}
			}
			
			// Obrni se proti tarci.
			Vector3 targetPoint = new Vector3 (target.transform.position.x, transform.position.y, target.transform.position.z) - transform.position;
			Quaternion targetRotation = Quaternion.LookRotation (targetPoint, Vector3.up);
			transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
			
			// Premikaj se proti tarci.
			if (followingPlayer == true) {
				// Ce ladja se ni prisla do igralca, naj gre proti njemu.
				if (Vector3.Distance (transform.position, target.position) > minDist) {
					transform.position += transform.forward * speed * Time.deltaTime;
				} else { // Ce je ladja prisla blizu igralca, naj mu jemlje kovancke.
					// Kovancke mu jemlji vsakih n sekund - stealingTime.
					if (Time.time - lastUpdate >= shootingTime) {
						lastUpdate = Time.time;
						shoot ();
						//Debug.Log(uiSprites);
						//GameManager.gm.Steal(stealingTreasureValue);
						// execute block of code here
					}
				}
			} else {
				transform.position += transform.forward * speed * Time.deltaTime;
			}
		}
	}
	
	void shoot() {
		// if projectile is specified
		if (projectile)
		{
			// Instantiante projectile at the camera + 1 meter forward with camera rotation
			GameObject newProjectile = Instantiate(projectile, shootingSource.position + shootingSource.forward, shootingSource.rotation) as GameObject;
			
			// if the projectile does not have a rigidbody component, add one
			if (!newProjectile.GetComponent<Rigidbody>()) 
			{
				newProjectile.AddComponent<Rigidbody>();
			}
			// Apply force to the newProjectile's Rigidbody component if it has one
			newProjectile.GetComponent<Rigidbody>().AddForce(shootingSource.forward * shootingPower, ForceMode.VelocityChange);
			
			// play sound effect if set
			if (shootSFX)
			{
				if (newProjectile.GetComponent<AudioSource> ()) { // the projectile has an AudioSource component
					// play the sound clip through the AudioSource component on the gameobject.
					// note: The audio will travel with the gameobject.
					newProjectile.GetComponent<AudioSource> ().PlayOneShot (shootSFX);
				} else {
					// dynamically create a new gameObject with an AudioSource
					// this automatically destroys itself once the audio is done
					AudioSource.PlayClipAtPoint (shootSFX, newProjectile.transform.position);
				}
			}
		}
	}
}