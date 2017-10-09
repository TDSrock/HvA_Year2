using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour {
    [SerializeField] public float velocity;
    [SerializeField] public float damage = 20;
    [SerializeField] public WeaponBehavior creator;

    [SerializeField] public LayerMask targetMask;
    [SerializeField] private LayerMask wallMask;
    public Vector3 debug;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        debug = this.transform.forward;
	}

    void FixedUpdate()
    {
        var frameSpeed = velocity * Time.deltaTime;
        Collider[] targets = Physics.OverlapSphere(transform.position, this.transform.localScale.x, targetMask);
        if (targets.Length != 0)
        {
            Ship col = (Ship)targets[0].gameObject.GetComponentInParent(typeof(Ship));
            if(col == null)
            {
                col = (Ship)targets[0].gameObject.GetComponentInChildren(typeof(Ship));
            }
            col._currentHealth -= this.damage;
            DestroyObject(this.gameObject);
        }

        Collider[] walls = Physics.OverlapSphere(transform.position, this.transform.localScale.x, wallMask);
        if (walls.Length != 0)
        {
            DestroyObject(this.gameObject);
        }
            
        
        this.transform.position += this.transform.forward * frameSpeed;
    }
}
