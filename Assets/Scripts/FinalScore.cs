using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class FinalScore : MonoBehaviour {
	Text instruction;
	void Start () {
		instruction = GetComponent<Text>();
		instruction.enabled=false;
	}
	
	// Update is called once per frame
	void Update () {
		if ((StageParams.Mod!=GameMode.Classic) && (StageParams.Mod!=GameMode.Greed)) instruction.enabled=false;
		if ((glob.TillVictory / 1000 < 0) && (instruction.enabled == false) && (StageParams.Mod==GameMode.Classic)) {
					float score=Time.time-glob.BestTime;
					instruction.enabled = true;
					instruction.text = "Well done, your time: " + score;
					InitStage.SaveScore (score);
			
				}
		if ((StageParams.Mod == GameMode.Greed) && (StageParams.GameFinished == true) && (instruction.enabled == false)) {
			instruction.enabled = true;
			instruction.text = "Well done, your MAX photons collected is " + glob.MaxPhotonsCollected + "." ;
		}

		}
}
