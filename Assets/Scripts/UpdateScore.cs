using UnityEngine;
using System.Collections;

public class UpdateScore : MonoBehaviour {

	void Start(){
		txt = GetComponent<GUIText> ();
	}
	GUIText txt;
	// Update is called once per frame
	void Update () {
		txt.text="To escape gravity of the black hole: " + (glob.TillVictory/1000);
	}
}
