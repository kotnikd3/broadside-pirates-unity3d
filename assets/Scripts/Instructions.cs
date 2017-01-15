using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Instructions : MonoBehaviour {

	public GameObject StoryText;
	public GameObject InstructionText;
	
	public void InstructionButtonClick () {
		//exitB.onClick.AddListener(delegate {ValueChangeCheck ();});
		Debug.Log ("Instruction");
		InstructionText.SetActive(true);
		StoryText.SetActive(false);;
	}
}
