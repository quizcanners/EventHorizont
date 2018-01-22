using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GameWin : MonoBehaviour {
	Text instruction;
	// Use this for initialization
	void Start () {
		instruction = GetComponent<Text>();
		instruction.enabled=false;
	}
	
	// Update is called once per frame
	void Update () {

		if (glob.TillVictory/1000 < -35) {
		instruction.enabled=true;
		
		
		}
	}
}
