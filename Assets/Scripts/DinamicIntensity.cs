using UnityEngine;
using System.Collections;

public class DinamicIntensity : MonoBehaviour {

	Light lght;
	void Start (){
		lght = GetComponent<Light> ();
	}
	
	// Update is called once per frame
	void Update () {
		lght.intensity = (glob.Worldspeed / 400);
	}
}
