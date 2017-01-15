using UnityEngine;
using System.Collections;
using UnityEngine.UI; // Required when Using UI elements.

public class Story : MonoBehaviour {
	
	public GameObject StoryText;
	public GameObject InstructionText;

	public void StoryButtonClick () {
		//exitB.onClick.AddListener(delegate {ValueChangeCheck ();});
		Debug.Log ("Story");
		InstructionText.SetActive(false);;
		StoryText.SetActive(true);;
	}
}
