using UnityEngine;
using System.Collections;

public class FallUntil : MonoBehaviour {

	public float minHeight = 5.0f;
	public float fallingSpeed = 20.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y > minHeight) {
			transform.Translate (-Vector3.up * Time.deltaTime * fallingSpeed);
			Debug.Log("PADA");
		}
	}
}
