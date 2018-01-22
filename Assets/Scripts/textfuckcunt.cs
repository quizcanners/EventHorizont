using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class textfuckcunt : MonoBehaviour {
	Text instruction;
	// Use this for initialization
	void Start () {
		instruction = GetComponent<Text>();
	}
  

    // Update is called once per frame
    void Update () {
		instruction.text = ((StageParams.Mod == GameMode.Classic) ? "Gravitational pull border: " + glob.TillVictory / 1000 + "-millions light years" : "")
						+ ((StageParams.Mod == GameMode.Journey) ? " Health: " + glob.TotalHealth : "")
						+ ((StageParams.Mod == GameMode.Greed) ? " Photons: " + glob.PhotonsCollected : "") +
						" RocksSpawned: " + glob.SpawnedRocks +
						" LightsSpawned: " + glob.SpawnedLights +
						" RocksToSpawn: " + glob.RocksToSpawn+
				" Max Hole Power: " + glob.MaxMadness;
			
	}
}
