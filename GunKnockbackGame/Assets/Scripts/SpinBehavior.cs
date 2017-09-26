using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinBehavior : MonoBehaviour {
    public float spinSpeed = 60f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        this.transform.Rotate(Vector3.up * spinSpeed * Time.deltaTime);
	}
}
