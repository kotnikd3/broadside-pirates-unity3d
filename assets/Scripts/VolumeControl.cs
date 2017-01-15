using UnityEngine;
using System.Collections;
using UnityEngine.UI; // Required when Using UI elements.

public class VolumeControl : MonoBehaviour {

	public Slider volumeSlider;
	
	public void Start()
	{
		//Adds a listener to the main slider and invokes a method when the value changes.
		volumeSlider.onValueChanged.AddListener (delegate {ValueChangeCheck ();});
	}
	
	// Invoked when the value of the slider changes.
	public void ValueChangeCheck()
	{
		AudioListener.volume = volumeSlider.value;
	}

}
