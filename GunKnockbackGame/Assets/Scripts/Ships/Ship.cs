using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ship : MonoBehaviour {
    public enum ShipType { Player, mob, miniBoss, Boss}

    public ShipType shipType;
    public List<WeaponBehavior> weapons;
    public Vector3 velocity;
    [Range (0,1)]public float drag;
    public float thrusterPower = 0;
    public float componentHealth = 100;
    public float fullHealth;
    public float currentHealth;
    public float displayHealth;

    [Header("UI related stuff(do not edit)")]
    public Image healthBar;

    public float _currentHealth
    {
        get
        {
            return currentHealth;
        }

        set
        {
            currentHealth = value;
            displayHealth = currentHealth / fullHealth;
            healthBar.fillAmount = displayHealth;
        }
    }

    // Use this for initialization
    void Start () {
        var weaponsComponents = GetComponentsInChildren(typeof(WeaponBehavior));
        WeaponBehavior[] w = new WeaponBehavior[weaponsComponents.Length];
        for(int i = 0;i < weaponsComponents.Length; i++)
        {
            w[i] = (WeaponBehavior)weaponsComponents[i];
        }
        weapons = new List<WeaponBehavior>(w);
        //TODO uncomment code below once ShipParts are implemented
        /*var bodyComponents = GetComponentsInChildren<ShipParts>();
        foreach(ShipParts c in bodyComponents){
            fullHealth += c.componentHealth;
        }

        */
        fullHealth += componentHealth;
        _currentHealth = fullHealth;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if(_currentHealth <= 0)
        {
            DestroyObject(this.transform.parent.gameObject);
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
