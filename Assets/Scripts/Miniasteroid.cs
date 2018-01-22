using UnityEngine;
using System.Collections;

public class Miniasteroid : MonoBehaviour {

	// Use this for initialization
	
	void Start (){
		Vector3 pos = transform.position;
		pos.z = 2;
		float ang = Random.Range (0.0f, 360.0f);
		ang = ang / 57;
		pos.y = 2 * Mathf.Sin (ang);
		pos.x = 2 * Mathf.Cos (ang) ;
		transform.position = pos;
		float size = 0.1f;
		transform.localScale = new  Vector3 (size, size, size);
        if (!rigiB)
		rigiB = GetComponent<Rigidbody> ();
		//rigidbodGetComponent<Rigidbody>()y.angularVelocity = Random.insideUnitSphere * 2;
	}
	Rigidbody rigiB;
	// Update is called once per frame
	void Update(){
		rigiB.velocity = new Vector3 (
			glob.speedx,
			glob.speedy,
			-1 * glob.Worldspeed
			)/100;
		
		//TheVar.speedx
	}
}
