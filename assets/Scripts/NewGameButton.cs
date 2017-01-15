using UnityEngine;
using System.Collections;
using UnityEngine.UI; // Required when Using UI elements.

public class NewGameButton : MonoBehaviour {

	public void NewGameClick () {
		//exitB.onClick.AddListener(delegate {ValueChangeCheck ();});
		Debug.Log ("NewGame");
		Application.LoadLevel ("Scena2"); 
	}
}
