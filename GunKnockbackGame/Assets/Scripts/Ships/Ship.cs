using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {
    public List<WeaponBehavior> weapons;
    public Vector3 velocity;
    [Range (0,1)]public float drag;
    public float thrusterPower = 0;
    public float health = 100;

	// Use this for initialization
	void Start () {
        var components = GetComponentsInChildren(typeof(WeaponBehavior));
        WeaponBehavior[] w = new WeaponBehavior[components.Length];
        for(int i = 0;i < components.Length; i++)
        {
            w[i] = (WeaponBehavior)components[i];
        }
        weapons = new List<WeaponBehavior>(w);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if(health <= 0)
        {
            DestroyObject(this.gameObject);
        }
        this.transform.position += velocity * Time.deltaTime;
        if (velocity.sqrMagnitude != 0)
        {
            velocity *= drag;
            
        }
	}

    public void InputVelocity(Vector3 input)
    {
        this.velocity += input * thrusterPower;
    }
}
