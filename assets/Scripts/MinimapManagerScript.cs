using UnityEngine;
using System.Collections;

public class MinimapManagerScript : MonoBehaviour {


	public RenderTexture MinimapTexture;
	public Material MiniMapMaterial;
	public float offset;

	private bool show = true;

	// Use this for initialization
	void Awake () {
		offset = 150;
		show = true;
	}

	void Update (){
		offset = Screen.height / 2;
		if (offset < 150) {
			offset = 150;
		}
		if (offset > 256) {
			offset = 256;
		}

		//set to boat mode//
		if (Input.GetKeyDown("m")) {
			if (show)
				show = false;
			else
				show = true;

		}

	}

	// Update is called once per frame
	void OnGUI () {
		if (show) {
			Graphics.DrawTexture (
			new Rect (10, 10, 
		         offset, 
		         offset),
			MinimapTexture,
			MiniMapMaterial
			);
		}
	}	
}
