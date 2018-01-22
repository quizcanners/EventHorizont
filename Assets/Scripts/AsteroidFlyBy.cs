using UnityEngine;
using System.Collections;

public class AsteroidFlyBy : MonoBehaviour {
	public bool AmActive;
	public int size;
	public float Mode3SpeedX, Mode3SpeedY;
	public GameObject Explosion;
	Rigidbody rigibi;
	Renderer rendi;
	private void SetState(bool state){
		//gameObject.SetActive (state);
		if (!rendi)
			rendi = GetComponent<Renderer> ();
		rendi.enabled = state;
		//GetComponent<Collider>().enabled = state;
		AmActive = state;
		}

	void FixedUpdate(){
		if (AmActive == true) {
			rigibi.velocity = new Vector3 (
			glob.speedx + Mode3SpeedX,
			glob.speedy + Mode3SpeedY,
			-1 * glob.Worldspeed
						);
						if (transform.position.z < -100)
								SetState (false);
				}else if (glob.RocksToSpawn>0)
			Start();
		//TheVar.speedx
	}

    AudioSource audio;

	void OnTriggerEnter(Collider other) 
	{
		
		
		//Instantiate (explosion, transform.position, transform.rotation);
		if ((other.tag == "Player") && (AmActive == true)){
            if (!audio) audio = GetComponent<AudioSource>();
            audio.Play();
			glob.Worldspeed =  glob.Worldspeed-Mathf.Min(size*4,glob.Worldspeed/3*2); //size
			glob.healthLoss+=(int)size;//other.gameObject.size;
			glob.TotalHealth-=(int)size;//size;

			SetState(false);
			//Destroy(gameObject);
			Instantiate (Explosion, other.transform.position, other.transform.rotation);
			
		}
		
		
	}

	public float MovingRocksDist=8000.0f;



	void Start (){
		if (!rigibi)
			rigibi = GetComponent<Rigidbody> ();
				//position.x -=Vector3(0,0, 100.0f);
				size = (int)Random.Range (1, glob.blackHolePower * 100);
                size *= 10;

		Mode3SpeedX = 0;
		Mode3SpeedY = 0;
			
		rigibi.velocity = transform.forward * glob.Worldspeed;
				Vector3 pos = transform.position;
		pos.z = StageParams.distOfSpawn;
		float ang = Random.Range (0.0f, 360.0f);
				ang = ang / 57;
				if (glob.spawningMode == 1) {
			pos.y = glob.passY + size * Mathf.Sin (ang)*(1.0f-glob.blackHolePower) ;
			pos.x = glob.passX + size * Mathf.Cos (ang) *(1.0f-glob.blackHolePower);
				} else 
			if ((glob.spawningMode == 0) || (glob.spawningMode == 2)) {
		


				float	 dist = Random.Range (1.0f, 8.0f);
			pos.x = glob.passX + size * dist * Mathf.Cos (ang) *(1.0f-glob.blackHolePower);
			pos.y = glob.passY + size * dist * Mathf.Sin (ang)*(1.0f-glob.blackHolePower) ;
				} else 
		if (glob.spawningMode == 3) {
			float x=Mathf.Cos (ang);
			float y=Mathf.Sin (ang);
			int dir=(int)Random.Range (0.0f, 7.0f);
			if (dir==6){
			Mode3SpeedX=-y*100.0f;
			Mode3SpeedY=x*100.0f;
			} else{
				Mode3SpeedX=y*100.0f;
				Mode3SpeedY=-x*100.0f;
			}
			float distMp=5+Random.Range (0.0f, 4.0f);
			pos.x = glob.passX + (x)*size -Mode3SpeedX*distMp*300/glob.Worldspeed;
			pos.y = glob.passY + (y)*size -Mode3SpeedY*distMp*300/glob.Worldspeed;
		
		}


		glob.Xvelocity -= size * Mathf.Cos (ang)/8;
		glob.Yvelocity -= size * Mathf.Sin (ang)/8;

		transform.position = pos;
		//size = 1000;
		transform.localScale= new  Vector3 (size, size, size);

		rigibi.angularVelocity = Random.insideUnitSphere*90/size;
		SetState(true);
		//Debug.Log ("Spawning rock");
		glob.RocksToSpawn -= 1;
	}


}
