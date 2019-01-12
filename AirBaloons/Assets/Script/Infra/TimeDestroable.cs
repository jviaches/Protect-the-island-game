using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDestroable : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Invoke("cleanup", 2f);
	}
	
	private void cleanup() {
        Destroy(gameObject);
	}
}
