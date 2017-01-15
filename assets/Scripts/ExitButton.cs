using UnityEngine;
using System.Collections;
using UnityEngine.UI; // Required when Using UI elements.

public class ExitButton : MonoBehaviour {

	public void ExitClick () {
		//exitB.onClick.AddListener(delegate {ValueChangeCheck ();});
		Debug.Log ("QUIT");
		Application.Quit ();
	}

}
