using UnityEngine;
using System.Collections;

public class BackLights : MonoBehaviour {
    public float MinVal;
    public float MaxVal;
    public float curVal;
    public Renderer _rendy;
    Quaternion zeroQuat;
    public float FadeSpeed;
    public float PhotonAddBrightness = 0.1f;
	// Use this for initialization
    void Awake() {
        glob._backLight = this;
        zeroQuat = transform.rotation;
    }
	
	// Update is called once per frame
    public void AddBrightness(float val) {
        curVal -= val;
    }

	void Update () {
        transform.rotation = zeroQuat;
        curVal += Time.deltaTime * FadeSpeed;
        curVal = Mathf.Clamp(curVal, MinVal, MaxVal);
        _rendy.material.SetFloat("_Stage", curVal);
	}
}
