using UnityEngine;
using System.Collections;

public class BlackHole : MonoBehaviour {
	Rigidbody rigib;
	// Use this for initialization
	void Start () {
		rigib = GetComponent<Rigidbody> ();
        /*rigib.position = new Vector3 
			(
				
				0.0f, 
				0.0f,
				-380.0f
				);*/

        transform.position = new Vector3(0,0,-800);
	}
	
	// Update is called once per frame
	void Update () {
		Quaternion rot = rigib.rotation;
		rot.y += 0.003f;
		rigib.rotation = rot;

		/*rigib.position = new Vector3 
			(
				0.0f, 
				0.0f,
				Mathf.Max(-1024.0f, rigib.position.z-0.1f)
				);*/
		if (StageParams.Mod == 0) {
		//	GetComponent<AudioSource>().Stop();
			Destroy(gameObject);		
		}
	}
}
