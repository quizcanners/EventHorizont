using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CloseMeUp : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	public void OpenMe(){
		gameObject.SetActive (true); 
	}

    public void CloseMe(){
		gameObject.SetActive (false); 
	}

	// Update is called once per frame
	void Update () {
	
	}
}
