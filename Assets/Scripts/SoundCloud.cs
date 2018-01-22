using UnityEngine;
using System.Collections;

[System.Serializable]
public class Chain {
	public AudioClip clip;
	public int[] followChance;
	public int sum;
}

public class SoundCloud : MonoBehaviour {
	public bool MusicStage;
	public int SitarNo;
	public int SitarPrevNo;
	public float SitarGap;
	public float SitarDelay;
	public float SitarCount;
	public int MissedTheBeat;
	public bool missedTheBeat=false;
	public Chain[] chains;
	public AudioClip[] PassiveSounds;
	public float initialGap;
	// Use this for initialization
	void Start () {
		for (int i=0; i<chains.Length; i++) {
			Chain tmp=chains[i];
			tmp.sum=0;
			for (int j=0; j<tmp.followChance.Length; j++)
				tmp.sum+=tmp.followChance[j];
		}

		glob.sCloud = this;
		SitarGap = initialGap;
	}

	float tempoChangeDelay;
	
	void SitarManagement(){
		SitarDelay -= Time.deltaTime;
		
		if (missedTheBeat) {
			SitarDelay += SitarGap;
			MissedTheBeat+=1;
		}
		
		if (SitarDelay <= 0)
			missedTheBeat = true;
		else {
			missedTheBeat = false;
	
		}
		
		if (SitarCount >= 2) {
			if (tempoChangeDelay<=0){
				SitarGap/=2;
				if (SitarDelay>SitarGap) SitarDelay-=SitarGap;
				tempoChangeDelay=0.6f;
				
			} else 
				tempoChangeDelay-=Time.deltaTime;
			//return;
		}
		
		if ((MissedTheBeat > 1) && (SitarGap<0.4f)) {
			
			if (tempoChangeDelay<=0){
				SitarGap*=2;
				tempoChangeDelay=0.6f;
				
			} else {
				
				tempoChangeDelay-=Time.deltaTime;
			}
		}
		
		
	}

	public AudioClip GetNote(){
		SitarCount-=1;
		if (MusicStage) {
			int p=Random.Range(0, PassiveSounds.Length);
				return PassiveSounds[p];
		}


		MissedTheBeat=0;
		SitarDelay=SitarGap;
		glob.sCloud.MissedTheBeat = 0;
		//TheVar.SitarNo+=1;
		//if (SitarNo>=chains.Length) SitarNo=0;
		int no, a;
		Chain tmp = chains [SitarNo];
		no = Random.Range (0, tmp.sum)+1;
		a = 0;
		while (no>tmp.followChance[a]) {
			no-=tmp.followChance[a];
			a++;
		}
		//do {no=Random.Range (0, chains.Length);} while (no==SitarNo);// || (no==SitarPrevNo));
		//SitarPrevNo=SitarNo;
	//	Debug.Log (" "+ a);
		SitarNo=a;
		return chains [a].clip;//[TheVar.SitarNo];//
	}

	// Update is called once per frame
	void Update () {
		SitarManagement ();
	}
}
