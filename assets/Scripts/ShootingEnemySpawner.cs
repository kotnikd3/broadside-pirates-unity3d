using UnityEngine;
using System.Collections;

public class ShootingEnemySpawner : MonoBehaviour {

	public int maxEnemies = 5;
	public float secondsBetweenSpawning = 10.0f;
	public GameObject shootingEnemie;

	private float nextSpawnTime;
	//private Transform enemieStaticPaths;
	private Transform enemieStaticPaths;

	// Use this for initialization
	void Start () {
		nextSpawnTime = Time.time+secondsBetweenSpawning;
		//emieStaticPaths = this.gameObject.transform.GetChild(0);

		enemieStaticPaths = GameObject.FindWithTag("EnemyStaticPaths").GetComponent<Transform>(); //.GetComponentsInChildren< Transform >();
	}
	
	// Update is called once per frame
	void Update () {
		// exit if there is a game manager and the game is over
		if (GameManager.gm) {
			if (GameManager.gm.gameIsOver)
				return;
		}

		// if time to spawn a new game object
		if (Time.time  >= nextSpawnTime && 
		    	this.transform.childCount < maxEnemies && 
			   	enemieStaticPaths != null) {
			MakeThingToSpawn ();
			
			// determine the next time to spawn the object
			nextSpawnTime = Time.time + secondsBetweenSpawning;
		}
	}

	void MakeThingToSpawn ()
	{
		int pot = (int)Random.Range(0, enemieStaticPaths.childCount);
		int tocka = (int)Random.Range(0, enemieStaticPaths.GetChild(pot).childCount);
		
		Transform potTockaKomponenta = enemieStaticPaths.GetChild(pot).GetChild(tocka);
		Vector3 spawnPosition;
		spawnPosition.x = potTockaKomponenta.transform.position.x;
		spawnPosition.y = 1.3f;
		spawnPosition.z = potTockaKomponenta.transform.position.z;

		GameObject spawnedObject = Instantiate (shootingEnemie, spawnPosition, transform.rotation) as GameObject;
		//spawnedObject.GetComponent<ShootingChaser> ().player = GameObject.FindWithTag ("Ladja");
		spawnedObject.GetComponent<ShootingChaser>().spawnObjects = new GameObject[enemieStaticPaths.GetChild(pot).childCount];

		for (int i = 0; i < enemieStaticPaths.GetChild(pot).childCount; i++) {
			GameObject staticPoint = enemieStaticPaths.GetChild(pot).GetChild(i).gameObject;
			spawnedObject.GetComponent<ShootingChaser>().spawnObjects[i] = staticPoint;
		}

		spawnedObject.transform.parent = this.gameObject.transform;
	}
}
