using UnityEngine;
using System.Collections;

[System.Serializable]
public class StarLight
{
	public Color color; 
	
}

public class FlyBy : MonoBehaviour {


	public float HowLow;
	public StarLight starlight;
	public GameObject myChild;
	public bool AmActive;
	public GameObject SoftExplosion;

	void OnTriggerEnter(Collider other) {
				if (other.tag == "Player") {
                    glob._backLight.AddBrightness(glob._backLight.PhotonAddBrightness);

						if (StageParams.Mod != GameMode.Greed) {
								glob.Worldspeed = glob.Worldspeed + glob.blackHolePower+((StageParams.Mod==GameMode.Classic) ? 3 : 0);
								if (glob.Worldspeed < 40)
										glob.Worldspeed = 40;
								if (glob.Worldspeed < 128+256*glob.blackHolePower)
										glob.Worldspeed *= 1.15f;
						} 
		if (glob.SparklesCount>0){
				//Debug.Log("Reusing Particle "+TheVar.SparklesCount);
				glob.Sparkles[glob.SparklesCount-1].ReuseMe (transform.position);
				glob.SparklesCount--;
			}
			else
						Instantiate (SoftExplosion, transform.position, transform.rotation);
						//Destroy(other.gameObject);
			myChild.SetActive(false);
			AmActive=false;
						glob.PhotonsCollected += 1;
            glob.MaxPhotonsCollected = Mathf.Max(glob.PhotonsCollected, glob.MaxPhotonsCollected);
        
				}
		}


	Rigidbody rigy;
	void Update(){
		//gameObject.SetActive (true);
		if (AmActive) {
			rigy.velocity = new Vector3 (
			glob.speedx,
			glob.speedy,
			-1 * glob.Worldspeed
						);
						if (transform.position.z < -100) {
								myChild.SetActive (false);
								AmActive = false;
						}
						//Destroy (gameObject);
				} else
			if (glob.LightsToSpawn > 0)
						Start ();
	
		//TheVar.speedx
	}

	void Start (){
        if (rigy == null)
		rigy = GetComponent<Rigidbody> ();
		//position.x -=Vector3(0,0, 100.0f);
		Quaternion asd = transform.rotation;
		asd.x = 0;
		asd.y = 0;
		asd.z = 0;
		transform.rotation = asd;
		rigy.velocity = -transform.forward * glob.Worldspeed;
		Vector3 pos = transform.position;
		pos.z = StageParams.distOfSpawn;
		if (glob.spawningMode == 2) {
			float ang = Random.Range (0.0f, 360.0f);
			ang = ang / 57;
			float size = Random.Range (5, glob.blackHolePower * 125);
			pos.y = glob.passY + size * Mathf.Sin (ang) * (1.0f - glob.blackHolePower);
			pos.x = glob.passX + size * Mathf.Cos (ang) * (1.0f - glob.blackHolePower);
		} else {
						pos.y = HowLow + glob.passY;
						pos.x = glob.passX;
				}
		transform.position = pos;

		glob.passX *= glob.centrelising;
		glob.passY *= glob.centrelising;
		glob.Xvelocity *= glob.centrelising;
		glob.Yvelocity *= glob.centrelising;
		glob.passX += glob.Xvelocity*glob.blackHolePower;
		glob.passY += glob.Yvelocity*glob.blackHolePower;
	 	glob.Xvelocity -= glob.deX;
		glob.Yvelocity -= glob.deY;
		glob.deX = (glob.deX + glob.passX * glob.deCompensationPower);//+TheVar.deX*1.01f;
		glob.deY = (glob.deY + glob.passY * glob.deCompensationPower);//+TheVar.deY*1.01f;
		AmActive=true;
		myChild.SetActive(true);
		glob.LightsToSpawn -= 1;
		}

}
