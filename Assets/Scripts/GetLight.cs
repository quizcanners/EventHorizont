using UnityEngine;
using System.Collections;

public class GetLight : MonoBehaviour {

    Light l;
    FlyBy fb;
    // Use this for initialization
    void Start () {

		//light.color.a = 

		Color col = new Color32( (byte)Random.Range (0, 255),
		                       (byte)Random.Range (0, 255),
		                       (byte)Random.Range (192, 255),
		                  255);
        if (!l) l = GetComponent<Light>();
        l.color = col;
        if (fb == null) fb = transform.parent.gameObject.transform.GetComponent<FlyBy>();
        fb.starlight.color = col;


	}
	

}
