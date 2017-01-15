using UnityEngine;
using System.Collections;

public class switchMode : MonoBehaviour {

	public GameObject boat;
	public GameObject boatCamera;
	public GameObject player;
	public GameObject playerStartPos;

	public static bool firstPersonView;

	// Use this for initialization
	void Start () {
		firstPersonView = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//set to boat mode//
		if (Input.GetKeyDown("c")) {
			if (firstPersonView)
				firstPersonView = false;
			else
				firstPersonView = true;

			if (!firstPersonView){
				boat.GetComponent<Rigidbody>().isKinematic = false;
				boat.GetComponent<Boat>().enabled = true;
				boatCamera.SetActive(true);
				
				player.SetActive(false);
			}
			
			//set to FPS mode//
			else
			{
				boat.GetComponent<Rigidbody>().isKinematic = true;
				boat.GetComponent<Boat>().enabled = false;
				boatCamera.SetActive(false);
				
				player.SetActive(true);
				player.transform.position = playerStartPos.transform.position;
			}
		}
	}
}
