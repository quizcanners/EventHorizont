using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MyText : MonoBehaviour {

    public static MyText _inst;

    public GameObject playButton;
    public GameObject NextMission;
    public GameObject PreviousMission;
	public Text instruction;

	public void selectClassic(){
		instruction.text = "Original Game mode: Cover predetermined distance as fast as possible." +
						" Speed is gained by collecting photon packs, " +
						"lost over time and upon collision. " +
						"boost is disabled, but photons give more speed. Fastest Run: " + StageParams.bestTime;   
		StageParams.selectedMode_Menu = GameMode.Classic;
        playButton.gameObject.SetActive( true);
        NextMission.SetActive(false);
        PreviousMission.SetActive(false);

    }
	public void selectJourney(){
		instruction.text = "Journey Game mode: Set of short levels, no time limitation, but ship takes damage. " +
			"One helth bar to cover full campaign. Levels can be replayed to lower amount of health lost. " +
			"Later boost is unlocked (Right MB) to double your speed while loosing ability to steer the ship, but gained speed is lost after boost is over.";// + (Time.time - TheVar.BestTime);
		StageParams.selectedMode_Menu = GameMode.Journey;
		StageParams.selectedStage = 0;
        NextMission.SetActive(true);
        PreviousMission.SetActive(false);
        playButton.SetActive(true);
    }
	public void selectGreed(){
		instruction.text = "Greed Game mode: Infinite. " +
			"Collect as many photon packs as possible before rising black hole's gravity overpowers you." +
			"To boost you need to burn photons manually (Right MB). " +
				"Best score: " + StageParams.MaxGreed;
		StageParams.selectedMode_Menu = GameMode.Greed;
        playButton.SetActive(true);
        NextMission.SetActive(false);
        PreviousMission.SetActive(false);
    }
    // Use this for initialization

    private void OnEnable()
    {
        _inst = this;
    }

    public void UpdateMissionDescription(){
		int no = StageParams.selectedStage;

		if (!instruction) instruction = GetComponent<Text>();
		

        NextMission.SetActive(StageParams.selectedStage < StageParams.NumberOfStages - 1);
        PreviousMission.SetActive(no>0);

        if (StageParams.StageUnlocked[no] == false)
        {
            instruction.text = "Stage " + no + " LOCKED";
            playButton.gameObject.SetActive(false);
            return;
        }
        else playButton.gameObject.SetActive(true);


        instruction.text="Stage "+ no+ " ";
		switch (no) {
		case 0:  instruction.text += "Human space station. Taking your last drink before going to bed." +
			"Cargo is delivered, payment transfer is just a matter of time. When suddenly an explosion far away," +
			"so bright it burned eyes of few guys around. You go to your ship and meet a scientist and a girl on the way." +
			"Scientist says to you: this explosion created a black hole," +
			"I knew your father, if you are as good engineer as he was, you can construct an anti-gravity engine that can consume photons " +
			"and will help us to " +
			"escape the black hole. You have 17 minutes before all goes to hell... "; break;
		case 1:  instruction.text += "'I did my best, but it will need some adjustment.', - you say. Girl says:" +
			"'Please, I am to pretty to die, I will make out with you if you save me'." +
			"'Nice try, but I am a married man, my heart is set in it's fixed state and cannot be readjusted." +
			"Sorry I talk technical terms, i am a mechanic, this is what I am good at ... possibly the only thing I am good at.'." +
					"Doctor comes to you 'You are a good man, Bruce Steel, it is the most important.' You say: 'Let's get this thing going'"; break;
		case 2:  instruction.text +="'We must not take any damage, this ship will not hold it', - said the girl, - 'Shut your mouth, " +
			"I made it with my own two hands out of my dad's old carrier. This baby can hold it ... And the engine is done', - you said. " +
			"BOOST is now available, (touch with 2 fingers)"; break;

		case 3:  instruction.text +="'We did it!', - said the Doctor, - 'Now we have a chance'." +
			"'Not so fast', - you say while pushing Doctor against the wall, " +
			"- 'We are not going anywhere before you tell me what the hell is going on!'," +
			"- 'This is a secret i swore to keep', - said the Doctor, " +
			"- 'I rather die than brake my oath'. 'Fine', - you said, - 'let's all just stay here and die ... " +
					"I am not bluffing'. 'Watch out!' , - screams the girl, - 'Another asteroid feald ahead'"; break;
		case 4: instruction.text +="'Doctor, you better talk, or I will kill you!', - 'Okey, okey, I will talk." +
			"The truth is: there is a research facility that extracts holigenic matter and uses phislonian fusion " +
			"combuster to produce pure defragmative sulfatinated gonstricluciangesk onium. They found out that it is " +
			"possible to convert blastic conductor into lavicrostic blister and ionize it with cloudron foam'," +
			"- 'Okey Doc, but I don't know what is a research facility and other things', - 'I already said to much, I have betrayed my oath', - " +
			"Doctor pulls a gun and shoots himself in a face. "; break;
		case 5: instruction.text +="'The Doctor is dead, now it is only you and me Cindy. Do not worry, I will take care of you. " +
			"As a father figure, not a boyfriend, I hope we are clear on that' , - 'Ow, Bruce Steel," +
			"you are a truly kind man.', - suddenly doctor makes a noise, - 'kheeekhh', - 'He is still alive!'"; break;
		case 6: instruction.text +="'Getting out of black hole is not easy', - said the dying Doctor, - 'no one has ever done this, we do not know what awaits us inside," +
			"it can be anything, literally anything! But one is certain, we must not stop for no reason, DON'T STOP'. You see a broken " +
			"spaceship outside, he was damaged by the blast, people inside will die. 'We must help them', - you said, - 'No!', - said Cindy, " +
			"- 'you heard what the Doc said, we need all the chances to survive this, we can't waste time, we can't take more cargo.', -" +
			"you disagree, - 'No! We are taking them in!'. "; break;
		case 7: instruction.text +="Now there are more people on the board. You saved them. 'You are such a good man. You have just one problem: " +
			"you are too perfect', - says Cindy, - 'You don't know me'. Doctor is still in pain, he is suffering. " +
			"You come over to a Doctor and say 'There are two kinds of pain in the world: the one that make you stronger, and the other kind, " +
			"that is just suffering',- you strangle the Doctor, - 'I don't have the tolerance for the second one'. Doctor is finally dead." +
			"'You are more complicated than I sought', - said Cindy."; break;
		case 8: instruction.text +="'We are not going to make it!!!', - few men start to panic, - " +
			"'there are too many of us on this ship, I'm not dying here', - he says. He attacks you and takes over the ship. " +
			"You are bleeding from your head, he comes close to finish you off. Suddenly Cindy picks up your laser cutter and cuts him in half." +
			"'Listen, people',-she said,-'This man is the only one who can get us out, I'm putting my life in his hands." +
			"If anyone has problem with this, you come to me. Bruce, the ship is your's, captain.'"; break;
		case 9: instruction.text +="'We are almost there', - just one last push and this is all going to end, " +
			"- 'We are all with you, Bruce.'. Suddenly, engine explodes, ship is losing power... 'I can do this'," +
			"- you say, - 'End of this black hole should be close. I can do this.'. Cindy: 'Im not so sure, there are too " +
			"many of us, we may have to do something about it.'. Few older people coming forward 'We are ready to leave this ship'. " +
			"You say: 'Nobody is going anywhere, we are going to make it!'. Cindy: 'Sorry, Bruce, but this is not up to you to decide'. "; break;
		case 10: instruction.text +="Ship almost stopped, it cannot keep up. Engine can't fight pull no longer.Cindy and most of " +
			"the people come into hatch, You scream: " +
			"'Noooo', but they eject themselves out of the ship, leaving only young and children behind, - 'It was " +
			"nice knowing you, tell my parents I loved them', - said Cindy."; break;
		case 11: instruction.text +="Nothingness ... just cries of children and darkness. Do you still have a chance...? suddenly you see the light." +
			"Far far away, this might be it, end of black hole. 'NO!', - you say to yourself. i am not leaving no one behind. " +
			"You turn your ship, go back and pick up Cindy and others - thanks to time dilations everybody is still alive." +
			"Now it is time to go home. Due to engine overload BOOST is disabled."; break;



		default: instruction.text += "stage in production. "; break;
		}
		int hp = 1024;
		for (int i=0; i<no; i++) {
			hp-=StageParams.HealthLostOnStage[i];

		}

		instruction.text = instruction.text +  " UNLOCKED"
		                                       + (((no<StageParams.NumberOfStages-1)&&(StageParams.StageUnlocked[no+1]==true)) 
		                                       ? "  Health lost: " + StageParams.HealthLostOnStage[no] : ".");
		instruction.text += " Health Left: " + hp;
	}

	// Update is called once per frame
	void Update () {
	
	}

	void Start () {
        if (instruction == null)
		instruction = GetComponent<Text>();
		selectJourney ();
	}
}
