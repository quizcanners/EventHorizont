using UnityEngine;
using System.Collections;

[System.Serializable]

public static class glob {
	public const int SparklesMax=128;
	public static SoundCloud sCloud;

	public static randomSoundPlay[] Sparkles = new randomSoundPlay[SparklesMax];
	public static int SparklesCount=0;

	public static float passX, passY, Xvelocity, Yvelocity, deX, deY, deCompensationPower=0.0001f,
	centrelising=0.97f,accelDivision=30,

	Planecentrelising=0.94f, 
	playerValConstrain=0.86f, //  Planecentrelising=0.94f, playerValConstrain=0.86f,

	speedx, speedy, speedz, Worldspeed=240,
	cameraX, cameraY, cameraZ, blackHolePower=0.25f,
	maxBlackHoleCoef=0.982f, SideVelocity=0, 
	DifficultyAcceleration=0.01f,//0.005f;
	MaxMadness=0.2f, BestTime, boosted=0;
	public static int MeteorGap, TillVictory=10000, healthLoss=0, TotalHealth=0,//250000,
	spawningMode=0, modeChangeTime=10, PhotonsCollected=0, SpawnedRocks=0, SpawnedLights=0, MaxPhotonsCollected=0;
	public static bool UsingGeroscope=false;
	public static Vector3 GeroscopeStart;
	public static int MaxLights=200, MaxRocks=300;
	public static int RocksToSpawn=0;
	public static int LightsToSpawn=0;
    public static BackLights _backLight;

}


public static class StageParams {
	public const int NumberOfStages = 12;
	public static GameMode Mod;
    public static GameMode selectedMode_Menu = 0; 
    public static int selectedStage = 0, MaxGreed =0;
	public static float bestTime = 99999.0f, blackHoledisplayCamRot=0;
	public static bool TurboMode=true, AccelerateMode=false, DeAccelMode=true;
	public static bool BreakMode=true, GameFinished=false;
	public static bool[] StageUnlocked=new bool[NumberOfStages];
	public static int[] HealthLostOnStage=new int[NumberOfStages];
	public static int distOfSpawn = 2048;

	//public static float SideVelocity;

}




public static class MyMath {
	public static float SinCos45=0.70716f;
	public static float getDistance(float x1, float x2){
		return Mathf.Sqrt ((x1 * x1) + (x2 * x2));
	}
}
public class movementStuff : MonoBehaviour {
	
	public float TiltInertia;
	public float brakes;
	public float tilt;
	public float speed;
	public float KeySpeed;
	public float GeroSpeed;

	//public GameObject SoftExplosion;
	private float MoveHorisontal;
	private float MoveVertical;

	void Start(){
		rigbody = GetComponent<Rigidbody> ();
		tilting = new Quaternion (0,0,0,0);

		//TheVar.Si
		//rigidbody.constraints = RigidbodyConstraints.FreezePositionX;
	}


	

	void CalculateSideVelocity(){
		float SideVelocity = MyMath.getDistance (rigbody.velocity.x,rigbody.velocity.y);
		glob.Worldspeed = glob.Worldspeed - (SideVelocity - glob.SideVelocity)*2;
		glob.SideVelocity = SideVelocity;
	}

	void RegularMode(){
		Vector3 movement = new Vector3 (rigbody.velocity.x+MoveHorisontal*3*Time.deltaTime,
		                                rigbody.velocity.y+MoveVertical*3*Time.deltaTime, 0.0f);
		rigbody.velocity = movement*(1.05f+(0.05f)*Time.deltaTime);
	}

	void AccelerateMode(){
		bool boosting = (Input.GetMouseButton(1)) || (glob.UsingGeroscope);
		if ((boosting) && (glob.PhotonsCollected > 0)) {
			glob.Worldspeed = glob.Worldspeed*1.02f;
			glob.PhotonsCollected-=1;
			SetControlsToConstrained();
		}

	}

	void DeAccelerateMode(){
		bool Deboosting = Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.RightControl);
		if ((Deboosting) && (glob.PhotonsCollected > 0)) {
			glob.Worldspeed = glob.Worldspeed*0.98f;
			glob.PhotonsCollected-=1;
			SetControlsToFree();
		}
	}

    public float boostBackEffect=1; 

	void TurboMode(){
		bool boosting = Input.GetMouseButton (1) || (glob.UsingGeroscope);
		if (!boosting) {
						RegularMode ();
						glob.Worldspeed -= glob.boosted * 0.1f;
						glob.boosted *= 0.9f;
		} else {
			float boost=(glob.Worldspeed-glob.boosted*1.5f)*0.3f;
			if (boost>0){
                glob._backLight.AddBrightness(boostBackEffect * Time.deltaTime);
				glob.Worldspeed+=boost;
				glob.boosted+=boost;
				SetControlsToConstrained();
			}
		
		}
	}

	void BreakMode(){
		bool brake = Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.RightControl);
		if (brake) {
			Vector3 movement = new Vector3 (rigbody.velocity.x+MoveHorisontal*5*Time.deltaTime,
			                                rigbody.velocity.y+MoveVertical*5*Time.deltaTime, 0.0f);
			rigbody.velocity = movement;	
			glob.Worldspeed=glob.Worldspeed*0.98f;
			SetControlsToFree();
		}
	}



	private bool usingToch=false;
	private Vector2 PreTouch;
	private Vector2 PreSpeed;

	private void SetControlsToFree(){
		glob.Planecentrelising=0.94f;
		glob.playerValConstrain=0.86f;
	}

	private void SetControlsToConstrained(){
		glob.Planecentrelising=0.99f;
		glob.playerValConstrain=0.60f;
		}

	public float CentralIsingSpeed;
	Rigidbody rigbody;
	public InitStage initStage;
	public int MaxSpeed;




	void Update(){
	

		if (glob.Worldspeed > MaxSpeed)
			glob.Worldspeed *= 0.99f;
		rigbody.rotation=Quaternion.Lerp(rigbody.rotation, tilting,Time.deltaTime*100);
		//rigbody.position = Vector3.Lerp (rigbody.position, despPos, Time.deltaTime * 100);
		SetControlsToFree ();

	//	MoveVertical = -(Input.GetAxisRaw ("Vertical") * KeySpeed) - transform.position.y * CentralIsingSpeed;
	//	MoveHorisontal = (Input.GetAxisRaw ("Horizontal") * KeySpeed) - transform.position.x * CentralIsingSpeed;

		if (Input.GetMouseButton (0)) {

			var mousePos = Input.mousePosition;
			mousePos.x -= (Screen.width / 2);// + transform.position.x * 10);
			mousePos.y -= (Screen.height / 2);// + transform.position.y * 10);
			mousePos/=Screen.width;

			MoveVertical =  (mousePos.y*20-transform.position.y)* speed;
			MoveHorisontal =  (mousePos.x*20 -transform.position.x)* speed;
			SetControlsToFree ();
		} else 
			if (Input.GetMouseButton (1)) {
			SetControlsToConstrained ();
           
            MoveHorisontal = rigbody.velocity.x - (transform.position.x * speed * Time.deltaTime);
            MoveVertical = rigbody.velocity.y - (transform.position.y * speed * Time.deltaTime);
			
		}



		if (Input.touchCount > 1) {
			if (glob.UsingGeroscope == false) {
				glob.GeroscopeStart = Input.acceleration;
				PreSpeed.x = glob.speedx;
				PreSpeed.y = glob.speedy;
			}
			glob.UsingGeroscope = true;
			SetControlsToConstrained ();
	
			Vector3 cur = Input.acceleration;
			float x = cur.x - glob.GeroscopeStart.x;
			float y = cur.y - glob.GeroscopeStart.y;

		

			glob.speedx = PreSpeed.x - x * GeroSpeed;
			glob.speedy = PreSpeed.y + y * GeroSpeed;

			MoveVertical = rigbody.velocity.y - (y * speed);
			MoveHorisontal = rigbody.velocity.x + (x * speed);
		} else {
			if (Input.touchCount > 0) {
				Vector2 touch = Input.GetTouch (0).position;
				if (usingToch == false) 
					PreTouch = touch;
				SetControlsToFree ();
				usingToch = true;
				glob.UsingGeroscope = false;
						
				touch.x = (touch.x - PreTouch.x);
				touch.y = (touch.y - PreTouch.y);
				touch/=Screen.width;
				/*	mousePos.x -= (Screen.width / 2);// + transform.position.x * 10);
			mousePos.y -= (Screen.height / 2);// + transform.position.y * 10);
			mousePos/=Screen.width;
			
			MoveVertical =  (mousePos.y*20-transform.position.y)* speed;
			MoveHorisontal =  (mousePos.x*20 -transform.position.x)* speed;*/

				MoveVertical =  (touch.y*20-transform.position.y) * speed;
				MoveHorisontal = (touch.x*20-transform.position.x) * speed;
			}
		}

		if (Input.touchCount != 1)
			usingToch = false;


		
	}
	public float MaxTouchControl;
	Quaternion tilting;
	void FixedUpdate(){

	
		if (Input.GetKey (KeyCode.Escape)){
			if (StageParams.Mod==0) 
			Application.Quit();
			else
				initStage.GotoMenu();
		}



		//RegularMode ();
		if (StageParams.TurboMode==true)
		TurboMode ();
		else
			RegularMode ();

		if (StageParams.BreakMode)
						BreakMode ();

		if (StageParams.AccelerateMode)
			AccelerateMode();

		if (StageParams.DeAccelMode)
						DeAccelerateMode ();

		CalculateSideVelocity ();

		 tilting = rigbody.rotation;
		tilting.z = Mathf.Clamp (tilting.z*TiltInertia + (rigbody.velocity.x * -tilt), -0.8f, 0.8f);
		tilting.x = Mathf.Clamp (tilting.x*TiltInertia + (rigbody.velocity.y * -tilt), -0.8f, 0.8f);
		//tilting.y = Mathf.Clamp (tilting.y*TiltInertia + (rigidbody.velocity.x * tilt), -0.8f, 0.8f);

		float dx = rigbody.velocity.x+rigbody.velocity.x * (1.0f - glob.playerValConstrain);
		float dy = rigbody.velocity.y+rigbody.velocity.y * (1.0f - glob.playerValConstrain);
		float tang=Mathf.Sqrt(dx*dx+dy*dy);
				if (tang>MaxTouchControl){
					dx=dx*MaxTouchControl/tang;
					dy=dy*MaxTouchControl/tang;
				}

		glob.speedx = (glob.speedx -(dx
		                                 ))*glob.Planecentrelising;
		glob.speedy = (glob.speedy- (dy
		                                 ))*glob.Planecentrelising;

	

		rigbody.position  = new Vector3 
			(
			
				rigbody.position.x* glob.Planecentrelising, 
				rigbody.position.y* glob.Planecentrelising,
				0.0f
				);
		rigbody.velocity = new Vector3 
			(
				rigbody.velocity.x*glob.playerValConstrain,  
				rigbody.velocity.y*glob.playerValConstrain,  
				0.0f
				);
		glob.cameraX = -rigbody.position.y/2;
		glob.cameraY = -rigbody.position.x/2;
		glob.cameraZ = -rigbody.rotation.z;
		
		glob.Worldspeed = glob.Worldspeed - glob.blackHolePower*BlackHoleSlowing*Time.deltaTime;

	}
//	Vector3 despPos;
	public float BlackHoleSlowing;
}
