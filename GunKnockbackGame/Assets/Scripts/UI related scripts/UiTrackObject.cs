using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class UiTrackObject : MonoBehaviour {
    Canvas me;
    Vector3 offset;
    public GameObject toTrack;
	// Use this for initialization
	void Start () {
        me = GetComponent<Canvas>();
        offset = me.transform.position - toTrack.transform.position;
	}

    // Update is called once per frame
    private void FixedUpdate()
    {
        me.transform.position = toTrack.transform.position + offset;
    }
}
