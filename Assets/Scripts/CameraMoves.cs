using UnityEngine;
using System.Collections;

public class CameraMoves : MonoBehaviour {
	public GameObject PassingLight;
	public GameObject PassingMeteor1;
	public GameObject PassingMeteor2;
	public GameObject PassingMeteor3;
	public GameObject TinyMeteor;

	public float nextFire;
	public float fireRate;

	private void UpdateBlackHoleData(){
		glob.blackHolePower=glob.blackHolePower+glob.DifficultyAcceleration;
		glob.blackHolePower*=glob.maxBlackHoleCoef;

		if (glob.blackHolePower>glob.MaxMadness) {
			glob.blackHolePower-=glob.MaxMadness*(float)Random.Range (250.0f, 500.0f)/1000.0f;
			glob.MaxMadness+=0.01f;
		}
	}
	private int RockRoster=0;

	private void Spawner(){
		if (glob.LightsToSpawn > 10)
						glob.LightsToSpawn = 0;
		if (glob.RocksToSpawn > 10)
				glob.RocksToSpawn = 0;
		if ((glob.LightsToSpawn > 0) && (glob.SpawnedLights<glob.MaxLights)) {
			glob.SpawnedLights+=1;
						Instantiate (PassingLight, transform.position, transform.rotation);
				}
	if ((glob.RocksToSpawn > 0) && (glob.SpawnedRocks<glob.MaxRocks)){
			glob.SpawnedRocks+=1;
			if (RockRoster == 0)
								Instantiate (PassingMeteor1, transform.position, transform.rotation);
		
			if (RockRoster == 1)
								Instantiate (PassingMeteor2, transform.position, transform.rotation);
		
			if (RockRoster > 1) {
								Instantiate (PassingMeteor3, transform.position, transform.rotation);
				RockRoster=-1;
			}
			RockRoster+=1;
				}
	
	}

	private void BasicSpawn10(){
		if (glob.Worldspeed < 30)
						return; 
		glob.LightsToSpawn += 1;
		if (glob.MeteorGap < 0) {
						glob.RocksToSpawn += 1;
			glob.MeteorGap=3;
				}
	}
	// Use this for initialization
	Camera camzy;
	void Start () {
		MyRadio = GetComponent<AudioSource> ();
		camzy = GetComponent<Camera> ();
	}
	
	// Update is called once per frame
	void Update () {




		//Quaternion rot = new Quaternion (11.62f, 0.0f, 0.0f,  );
		camzy.fieldOfView = 70+glob.Worldspeed / 20;
		Spawner ();

		if (Time.time > nextFire) {



			glob.TillVictory-=(int)glob.Worldspeed;
			if ((glob.TillVictory < 15000) && (StageParams.Mod==GameMode.Journey))
				nextFire = Time.time + fireRate;
				else
			if (StageParams.GameFinished==false)
			{
			if (StageParams.Mod==GameMode.Greed)
					nextFire = Time.time + fireRate*300/glob.Worldspeed;
					else
				nextFire = Time.time + fireRate;//*300/TheVar.Worldspeed);

				// Modes Begin
				if (glob.spawningMode==0){

					glob.RocksToSpawn+=1;
					if (StageParams.Mod==0){
					glob.speedx=0;glob.speedy=0;
					glob.deX=0; glob.deY=0;
						glob.Worldspeed=200;
						glob.TillVictory=10000;
					}else
						glob.LightsToSpawn+=1;
						//Instantiate (PassingLight, transform.position, transform.rotation);
					glob.modeChangeTime-=1;
					if ((glob.modeChangeTime<0) && (StageParams.Mod!=0)) {
						glob.spawningMode=1;
						glob.modeChangeTime=30;
					}
				}
				else
				if (glob.spawningMode==1){

					BasicSpawn10();

			if (glob.MeteorGap==1)
						UpdateBlackHoleData();

					glob.modeChangeTime-=1;
						if (glob.modeChangeTime<0) {
						glob.spawningMode=Random.Range(2,4);
							glob.modeChangeTime=30+(glob.spawningMode==3 ? 10 : 0);
						}
	



				}

				else
				if (glob.spawningMode==2){
					glob.modeChangeTime-=1;
					glob.LightsToSpawn+=2;
					if (glob.modeChangeTime<0) {
						glob.spawningMode=0;
						glob.modeChangeTime=15;
					}
					BasicSpawn10();
				}
				else
				if (glob.spawningMode==3){

					glob.modeChangeTime-=1;
					if (glob.modeChangeTime<0) {
						glob.spawningMode=0;
						glob.modeChangeTime=15;
					}
				
					BasicSpawn10();
					glob.RocksToSpawn+=1;

					if (glob.MeteorGap==1)	UpdateBlackHoleData();

				}
				// Modes End
				glob.MeteorGap=glob.MeteorGap-1;

			}


		}
		MyRadio.pitch=Mathf.Clamp (glob.Worldspeed/40.0f, 0.6f, 1.0f);
		Quaternion asd = transform.rotation;
		asd.x = glob.cameraX/32;
		asd.y = -glob.cameraY/16;
		//asd.z = TheVar.cameraZ/8;
		if ((StageParams.GameFinished == true) && (StageParams.Mod==GameMode.Journey)){
						asd.y = asd.y + StageParams.blackHoledisplayCamRot;
			StageParams.blackHoledisplayCamRot+=0.0005f;
				}
	//	else

		transform.rotation = asd;



		if  ((glob.sCloud.MusicStage) && (!MyRadio.isPlaying)){
			//Debug.Log("Radio was off");
			MyRadio.Play();
			//MyRadio.Stop();
		}

	}
	AudioSource MyRadio;
}
