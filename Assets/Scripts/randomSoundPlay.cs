using UnityEngine;
using System.Collections;




public class randomSoundPlay : MonoBehaviour {

	//public AudioClip[] clips;
	public AudioSource tmp;
	ParticleSystem effect;
	// Use this for initialization
	void Start(){
		glob.sCloud.SitarCount += 1;
		if (effect == null)
		    effect = GetComponent<ParticleSystem> ();
		playing = false;


		if (glob.sCloud.MusicStage) 
			PlayGet ();

        //Debug.Log("Sound Play");
	}

	float PoolDelay;

public void ReuseMe(Vector3 location){

		playing = false;
		glob.sCloud.SitarCount += 1;
		transform.position = location;
		effect.Clear ();

		tmp.Stop ();
		gameObject.SetActive (true);
		if (glob.sCloud.MusicStage) 
			PlayGet ();

	}

void PlayGet(){
		if (glob.sCloud.MusicStage) {
			tmp.volume = 1;
		}
		else
			tmp.volume = 0.15f;

		playing=true;
		tmp.clip=glob.sCloud.GetNote();

		tmp.Play ();
		effect.Play ();
		PoolDelay = 2;

	}

	bool playing=false;
	void Update(){
		if (!playing) {
			if ((glob.sCloud.SitarDelay <= 0) || (glob.sCloud.MusicStage)) 
			PlayGet ();
		} else {
			PoolDelay-=Time.deltaTime;
			if (PoolDelay<0){

				if (glob.SparklesCount<glob.SparklesMax)
				{
					glob.Sparkles[glob.SparklesCount]=this;
					glob.SparklesCount++;
				}
				else {
					Destroy(gameObject);
					Debug.Log("Out of sparcles");
				}
				tmp.Stop ();
				gameObject.SetActive(false);
			}

		}
	}
	

}
