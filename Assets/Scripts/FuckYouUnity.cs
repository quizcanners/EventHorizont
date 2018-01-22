using UnityEngine;
using System.Collections;

public class FuckYouUnity : MonoBehaviour {

	public float distance;
	public GameObject Explosion;
	Rigidbody rigi;
	AudioSource Radio;

	void OnTriggerEnter(Collider other) 
	{
		
		if (other.tag == "Boundary") {
			return;
		}
		
		//Instantiate (explosion, transform.position, transform.rotation);
		if (other.tag == "Player") {
		//	Debug.Log ("Should be a sound right now");
			Radio.Play();
			glob.Worldspeed = glob.Worldspeed*6/7;
			
			Destroy(gameObject);
			Instantiate (Explosion, transform.position, transform.rotation);
			//	Instantiate (playerExplosion, other.transform.position, other.transform.rotation);
			//gameController.GameOver ();
		}
	}

	void Update(){
		rigi.velocity = new Vector3 (
			glob.speedx,
			glob.speedy,
			-1 * glob.Worldspeed
			);
		
		//TheVar.speedx
	}
	

	
	void Start (){
		Radio = GetComponent<AudioSource> ();
		rigi = GetComponent<Rigidbody> ();
		//position.x -=Vector3(0,0, 100.0f);
		float size = Random.Range (1, 5);
		Quaternion asd = transform.rotation;
		asd.x = 0;
		asd.y = 0;
		asd.z = 0;
		transform.rotation = asd;
		rigi.velocity = transform.forward * glob.Worldspeed;
		Vector3 pos = transform.position;
		pos.z = 1000;
		float ang = Random.Range (0.0f, 360.0f);
		ang = ang / 57;
		pos.y = 10 * Mathf.Sin (ang);
		pos.x =  10 * Mathf.Cos (ang);
		transform.position = pos;
		
		transform.localScale = new  Vector3 (size, size, size);
		
		rigi.angularVelocity = Random.insideUnitSphere * 2;
	}
}
