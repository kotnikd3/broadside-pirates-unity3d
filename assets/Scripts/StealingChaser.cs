using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(CharacterController))]

public class StealingChaser : MonoBehaviour {
	
	public float speed = 20.0f;
	public float rotationSpeed = 2.0f;
	public float minDist = 1f;
	public float maxDist = 100f;
	public Transform player;
	public int stealingTreasureValue = 1;
	public float stealingTime = 1.0f;
	public GameObject[] spawnObjects; // what prefabs to spawn

	private Transform target;
	private int targetIndex;
	private bool followingPlayer;
	private float lastUpdate;
	// Use this for initialization
	void Start () 
	{
		// if no target specified, assume the player
		if (player == null) {
			if (GameObject.FindWithTag ("Ladja")!= null)
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

		float playerDistance = Vector3.Distance(transform.position, player.position);

		if (spawnObjects.Length > 0) {
			// Ce je razdalja do igralca manjsa od najvecje dovoljene, potem za tarco postavi igralca.
			if (playerDistance <= maxDist) {
				target = player;
				followingPlayer = true;
			} else {
				// Ce ladja prej ni sledila igralcu, naj gre proti naslednji tarci.
				if (!followingPlayer) {
					target = spawnObjects[targetIndex % spawnObjects.Length].GetComponent<Transform>();
					
					if (Vector3.Distance(transform.position, target.position) < 5)
						targetIndex += 1;
				} else {
					// Ce je ladja prej sledila igralcu in je ta sel izven njenega dometa, potem sledi najblizji tarci.
					float minimumDistance = float.MaxValue;
					
					// Najdi najblizjo tarco.
					for (int i = 0; i < spawnObjects.Length - 1; i++) {
						Transform target_tmp = spawnObjects[i].GetComponent<Transform>();
						float distance_tmp = Vector3.Distance(transform.position, target_tmp.position);
						
						if (distance_tmp < minimumDistance){
							minimumDistance = distance_tmp;
							targetIndex = i;
						}
					}
					target = spawnObjects[targetIndex].GetComponent<Transform>();
					followingPlayer = false;
				}
			}
			
			// Obrni se proti tarci.
			Vector3 targetPoint = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z) - transform.position;
			Quaternion targetRotation = Quaternion.LookRotation (targetPoint, Vector3.up);
			transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
			
			// Premikaj se proti tarci.
			if (followingPlayer == true) {
				// Ce ladja se ni prisla do igralca, naj gre proti njemu.
				if (Vector3.Distance(transform.position,target.position) > minDist){
					transform.position += transform.forward * speed * Time.deltaTime;
				} else { // Ce je ladja prisla blizu igralca, naj mu jemlje kovancke.
					// Kovancke mu jemlji vsakih n sekund - stealingTime.
					if(Time.time - lastUpdate >= stealingTime){
						lastUpdate = Time.time;
						GameManager.gm.Steal(stealingTreasureValue);
						// execute block of code here
					}
				}
			} else {
				transform.position += transform.forward * speed * Time.deltaTime;
			}
		}
	}
}
