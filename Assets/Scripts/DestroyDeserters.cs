using UnityEngine;
using System.Collections;

public class DestroyDeserters : MonoBehaviour {

	void OnTriggerExit(Collider other)
	{
		Destroy(other.gameObject);
	}

}
