using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public enum GameMode { MainMenu = 0, Classic = 1, Journey = 2, Greed = 3 }

public class InitStage : MonoBehaviour {
	public void QuitButton(){
			Application.Quit();
	}
	public GameObject blackHole;
	public GameObject PhotonCounter;
	public AudioClip[] missionsOst;

	public static void UpdateStageData(int stageNo){
		StageParams.HealthLostOnStage[stageNo]=PlayerPrefs.GetInt("HealthLoss_"+stageNo);
		StageParams.StageUnlocked[stageNo]=(PlayerPrefs.GetInt("StageUnlocked_"+stageNo)==1) ? true : false;
	}

	static public void SaveScore (float time) {
		if (time < StageParams.bestTime) {
			PlayerPrefs.SetFloat ("BestTime", time);
			StageParams.bestTime=time;
			Debug.Log("Best Time saved");
		}
	}

	public void SaveLevelResult(int healthLoss, int stageNo){
		int no=stageNo+1;
		if ((healthLoss < StageParams.HealthLostOnStage [stageNo]) || ((no<StageParams.NumberOfStages) && (StageParams.StageUnlocked[no]==false)))
		PlayerPrefs.SetInt ("HealthLoss_"+stageNo, healthLoss);

		if (no < StageParams.NumberOfStages) {
						PlayerPrefs.SetInt ("StageUnlocked_" + no, 1);
				}
		UpdateStageData(stageNo);
	}

	static public void SaveGreedScore (int photons) {
		if (photons > StageParams.MaxGreed) {
			PlayerPrefs.SetInt ("Greed", photons);
			StageParams.MaxGreed=photons;
			Debug.Log("Best Greed saved");
            try
            {
                IndiexpoAPI_WebGL.SendScore(photons);
            } catch ( Exception ex)
            {
                Debug.Log(ex.ToString());
            }
		}
	}

	public void LoadScore (){
		StageParams.bestTime = PlayerPrefs.GetFloat ("BestTime");
		StageParams.MaxGreed = PlayerPrefs.GetInt ("Greed");
		for (int i=0; i< StageParams.NumberOfStages; i++) {
			StageParams.HealthLostOnStage[i]=PlayerPrefs.GetInt("HealthLoss_"+i);
			StageParams.StageUnlocked[i]=(PlayerPrefs.GetInt("StageUnlocked_"+i)==1) ? true : false;
		}
		int set = PlayerPrefs.GetInt ("ZeroInit");
		if (set!= 2)
						ZeroAllScores ();

	}

	public void ZeroAllScores(){
		PlayerPrefs.SetFloat ("BestTime", 99999);
		StageParams.bestTime = 99999;
		PlayerPrefs.SetInt ("Greed", 0);
		StageParams.MaxGreed = 0;
		for (int i=0; i< StageParams.NumberOfStages; i++) {
			PlayerPrefs.SetInt ("StageUnlocked_" + i, 0);
			PlayerPrefs.SetInt ("HealthLoss_"+i, 1024);
			UpdateStageData(i);
				}
		PlayerPrefs.SetInt ("StageUnlocked_0", 1);
		UpdateStageData(0);
		PlayerPrefs.SetInt ("ZeroInit", 2);

	}

	void SetgameInitials(){

		glob.speedx=0; glob.speedy=0; glob.speedz=0; 
		glob.Worldspeed=240; glob.healthLoss = 0; 
		glob.passX=0; glob.passY=0; glob.Xvelocity=0;
		glob.Yvelocity=0; glob.deX=0; glob.deY=0; 
		StageParams.GameFinished = false;

	}

	public void SetOriginal(){
		glob.sCloud.MusicStage = true;
		glob.blackHolePower = 0.05f;
		glob.DifficultyAcceleration = 0.015f;//0.005f;
		glob.MaxMadness = 0.2f; 
		glob.TillVictory = 200000;
		glob.spawningMode = 0; 
		glob.modeChangeTime = 30;
		glob.BestTime = Time.time;
		StageParams.BreakMode = true;
		StageParams.TurboMode = false;
		StageParams.AccelerateMode = false;
		StageParams.DeAccelMode = false;
        glob.sCloud.MusicStage = true;
        Radio.clip = missionsOst[UnityEngine.Random.Range(0,10)];
        Radio.Play();
	}

	public void SetStageStory(){
		glob.blackHolePower = 0.05f+0.01f*StageParams.selectedStage;;
		glob.DifficultyAcceleration = 0.005f+0.003f*StageParams.selectedStage;//0.005f;
		glob.MaxMadness = 0.1f+0.01f*StageParams.selectedStage; 
		glob.TillVictory = 120000-StageParams.selectedStage*2500+((StageParams.selectedStage==10) ? 20000: 0);
		glob.spawningMode = 0; 
		glob.modeChangeTime = 30;
		glob.BestTime = Time.time;
		glob.TotalHealth=1024;
		for (int i=0; i<StageParams.selectedStage; i++) 
			glob.TotalHealth-=StageParams.HealthLostOnStage[i];

		StageParams.BreakMode = true;
		StageParams.TurboMode = ((StageParams.selectedStage>=2) && (StageParams.selectedStage!=11)) ? true : false;
		StageParams.AccelerateMode = false;
		StageParams.DeAccelMode = false;
	//	if (GameModes.selectedStage == GameModes.NumberOfStages - 1) {
	//					cam.GetComponent<AudioSource>().Stop ();
	//					FinalSong.GetComponent<AudioSource>().Play ();
	//			}
		//cam.GetComponent<AudioSource>().Stop ();       
		if ((missionsOst.Length > StageParams.selectedStage) && (missionsOst [StageParams.selectedStage] != null)) {
			glob.sCloud.MusicStage = true;
			Radio.clip = missionsOst [StageParams.selectedStage];
			Radio.Play ();
		} else {
			glob.sCloud.MusicStage = false;
			Radio.Stop();
		}
	}

	public void SetGreed(){
		glob.sCloud.MusicStage = true;
		glob.blackHolePower = 0.01f;
		glob.DifficultyAcceleration = 0.01f;//0.005f;
		glob.MaxMadness = 0.1f; 
		glob.TillVictory = 10000;//300000;
		glob.spawningMode = 0; 
		glob.modeChangeTime = 30;
		glob.PhotonsCollected = 0;
        glob.MaxPhotonsCollected = 0;

		StageParams.BreakMode = true;
		StageParams.TurboMode = false;
		StageParams.AccelerateMode = true;
		StageParams.DeAccelMode = false;
		PhotonCounter.SetActive (true);
        Radio.clip = missionsOst[UnityEngine.Random.Range(0, 10)];
        Radio.Play();
	}



	public GameObject hero;

	public void SetStage(){
		hero.SetActive (true);
		//Debug.Log ("Starting Game mode:" + GameModes.Mod);
		StageParams.Mod = StageParams.selectedMode_Menu;
		if (StageParams.Mod == GameMode.MainMenu )	StageParams.Mod = GameMode.Journey;

		if (StageParams.Mod == GameMode.Classic) 
						SetOriginal ();
				else
			if (StageParams.Mod ==GameMode.Journey) 
						SetStageStory ();
				else 
			if (StageParams.Mod == GameMode.Greed)
						SetGreed ();
		


		SetgameInitials ();


	}

	public void NextMission(){
		if (StageParams.selectedStage < StageParams.NumberOfStages-1)
						StageParams.selectedStage += 1;

        MyText._inst.UpdateMissionDescription();

	}

	public void previousMission(){
		if (StageParams.selectedStage > 0)
						StageParams.selectedStage -= 1;

        MyText._inst.UpdateMissionDescription();
    }

	public GameObject[] UItoClose;
	public void CloseMenu(){
		glob.TotalHealth=1024;
		for (int i=0; i<StageParams.selectedStage; i++) {
			glob.TotalHealth-=StageParams.HealthLostOnStage[i];
			
		}

		if ((StageParams.Mod == GameMode.Journey) && ((StageParams.StageUnlocked [StageParams.selectedStage] == false) || (glob.TotalHealth<=0)))
						return;
		for (int i=0; i<UItoClose.Length; i++) {
			UItoClose[i].SetActive (false);
				}
			SetStage();
	}
	
	public Camera cam;
	public MyText textDta;
	private int QuitDelay;


	public void GotoMenu(){
		PhotonCounter.SetActive (false);
		hero.SetActive (false);
		RenderSettings.fogDensity = 0.0009f; 
		RenderSettings.fog = true; 
		cam.clearFlags=CameraClearFlags.Color;

		for (int i=0; i<UItoClose.Length; i++) {
			UItoClose[i].SetActive (true);
		}
		StageParams.Mod = GameMode.MainMenu;
		glob.spawningMode = 0;
		StageParams.selectedMode_Menu = GameMode.Journey;
		textDta.UpdateMissionDescription ();
		StageParams.GameFinished = false;
		QuitDelay = 50;
		Radio.clip=defaultMusic;
		//Radio.Stop ();
		//RenderSettings.fog = false; 
	}

	public ChangableText PhotonsText;
	public AudioSource Radio;
	public AudioClip defaultMusic;
	public AudioClip VictoryMusic;
	void Start () {
		Radio = cam.GetComponent<AudioSource>();
		LoadScore ();
		GotoMenu ();
		Input.gyro.enabled = true;
		Physics.IgnoreLayerCollision (8, 9);
		Physics.IgnoreLayerCollision (9, 9);

	}
	void Update () {
		if (Input.GetKey (KeyCode.Escape) && (StageParams.Mod==0) && (QuitDelay<0)) Application.Quit();
		QuitDelay -= 1;

				if ((glob.TillVictory < 0) && (StageParams.Mod != 0) && (StageParams.Mod != GameMode.Greed)) {//RenderSettings.fog = false; 
					//	Debug.Log ("Level completed");

			//cam.GetComponent<AudioSource>().Play ();

						if ((StageParams.Mod == GameMode.Classic) || ((StageParams.Mod == GameMode.Journey) && (StageParams.selectedStage == StageParams.NumberOfStages - 1))) {
								RenderSettings.fogDensity -= 0.0000001f;
								RenderSettings.fogDensity *= 0.97f;
								//Debug.Log ("Fading fog");
								if ((RenderSettings.fogDensity < 0.0009f) && (StageParams.GameFinished == false)) {
					//Debug.Log ("Removing fog");
					StageParams.GameFinished = true;
					//if (VictoryMusic)
					Radio.clip=VictoryMusic;
					//FinalSong.GetComponent<AudioSource>().Stop();
					//if (missionsOst
					Instantiate (blackHole, transform.position, transform.rotation);
			
										RenderSettings.fog = false; 
										if (StageParams.Mod == GameMode.Journey)
												SaveLevelResult (glob.healthLoss, StageParams.selectedStage);
										cam.clearFlags = CameraClearFlags.Skybox;
					  
										//audio.Stop ();
										
								//		Debug.Log ("Creating black hole");
				
								}
						}
			else{
				Radio.clip=defaultMusic;
				//Debug.Log("Switching to default");
			}

						glob.Worldspeed = (glob.Worldspeed < 200) ? glob.Worldspeed + 1 : 200;
			//Debug.Log ("GameMod==" + GameModes.Mod + "Stage " + GameModes.selectedStage);
						if ((StageParams.Mod == GameMode.Journey) && (StageParams.selectedStage != StageParams.NumberOfStages - 1)) {
			//	Debug.Log ("HURRRAYYYY");
								SaveLevelResult (glob.healthLoss, StageParams.selectedStage);
						if (StageParams.selectedStage<StageParams.NumberOfStages-1)	StageParams.selectedStage += 1;
				UpdateStageData(StageParams.selectedStage);
								GotoMenu ();

				return;
						}



				}

			

			

		if (StageParams.Mod == GameMode.Classic)
		if (glob.Worldspeed < 30) {
			glob.Worldspeed -= 2;
			if (glob.Worldspeed < -570) {
				SetgameInitials ();
				SetOriginal ();
			}
		}


				if (((glob.TotalHealth <= 0) || (glob.Worldspeed < 30)) && (StageParams.Mod == GameMode.Journey) && (StageParams.GameFinished==false)) {
						glob.Worldspeed -= 3;
						if (glob.Worldspeed < -470) {
								SetgameInitials ();
								SetStageStory ();
						}
				}

		if (StageParams.Mod == GameMode.Greed) {
			PhotonsText.ChangeCountText("Photons (MAX: "+glob.MaxPhotonsCollected+")", glob.PhotonsCollected);
			if (glob.Worldspeed < 30) {
								glob.Worldspeed -= 2;
								if ((glob.Worldspeed < -570) && (StageParams.GameFinished == false)) {
										SaveGreedScore (glob.MaxPhotonsCollected);
										PhotonCounter.SetActive (false);
										StageParams.GameFinished = true;
										glob.Worldspeed = -570;
								}
						}
				}

		//
		}
}
