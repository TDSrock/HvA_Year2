using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysShoot : MonoBehaviour {
    public Ship myShip;
	// Use this for initialization
	void Start () {
        myShip = (Ship)GetComponent(typeof(Ship));
    }
	
	// Update is called once per frame
	void Update () {
        foreach (WeaponBehavior weapon in myShip.weapons)
        {
            weapon.AttemptFire();
        }
    }
}
