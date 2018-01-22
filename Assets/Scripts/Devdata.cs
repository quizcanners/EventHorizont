using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Devdata : MonoBehaviour {

	Text instruction;
	// Use this for initialization
	void Start () {
		instruction = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		instruction.text = "HolePower: "+ glob.blackHolePower*1000 +
			" dx:" + glob.deX + " dy:" + glob.deY + " Mode:" + glob.spawningMode;
	}
}
