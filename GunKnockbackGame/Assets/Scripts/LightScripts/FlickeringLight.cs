using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class FlickeringLight : MonoBehaviour {

    public WaveForm waveForm = WaveForm.sin;

    public float baseStart = 0.0f;
    public float amplitude = 1.0f;
    public float phase = 0.0f;
    public float frequency = 0.05f;
    [Range(0, 5)] public float polynomial = 2f;

    private Color orginalColor;
    private Light lightRef;

	// Use this for initialization
	void Start () {
        lightRef = GetComponent<Light>();
        orginalColor = lightRef.color;//save the OG color.
	}
	
	// Update is called once per frame
	void Update () {
        lightRef.color = orginalColor * WaveMathHelper.EvalWave(phase, frequency, amplitude, baseStart, waveForm, polynomial);
	}

    
}
