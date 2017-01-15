using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(CharacterController))]

public class Chaser_v2 : MonoBehaviour {
	
	public float speed = 20.0f;
	public float rotationSpeed = 2.0f;
	public float minDist = 1f;
	public float maxDist = 100f;
	public Transform target;

	// Use this for initialization
	void Start () 
	{
		// if no target specified, assume the player
		if (target == null) {

			if (GameObject.FindWithTag ("Player")!=null)
			{
				target = GameObject.FindWithTag ("Player").GetComponent<Transform>();
			}
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (target == null)
			return;

		//get the distance between the chaser and the target
		float targetDistance = Vector3.Distance(transform.position,target.position);

		if (targetDistance < maxDist) {
			// face the target
			//transform.LookAt (target);

			// Obrni se proti tarci - igralcu
			Vector3 targetPoint = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z) - transform.position;
			Quaternion targetRotation = Quaternion.LookRotation (targetPoint, Vector3.up);
			transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

			//so long as the chaser is farther away than the minimum distance, move towards it at rate speed.
			if (targetDistance > minDist)	
				transform.position += transform.forward * speed * Time.deltaTime;
		}
	}

	// Set the target of the chaser
	public void SetTarget(Transform newTarget)
	{
		target = newTarget;
	}

}
