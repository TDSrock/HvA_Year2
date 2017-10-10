using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapBehavior : MonoBehaviour {

    public float pullEffectWeight = 5f;
    float value = 10f;
    List<Ship> pulledTowardsList;
    Rigidbody rb;
    bool collected = false;


	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        pulledTowardsList = new List<Ship>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	}

    private void OnTriggerStay(Collider other)
    {
        var ship = other.gameObject.GetComponent<Ship>();
        if (other.gameObject.GetComponent<Ship>() != null)
        {
            if (ship.currentHealth > 0)
            {
                Vector3 dif = this.transform.position - ship.transform.position;
                var sqrDistance = dif.sqrMagnitude;
                rb.AddForce(dif.normalized * Time.fixedDeltaTime * -pullEffectWeight * (Mathf.Max(100 - sqrDistance, 1)));//TODO, make math better
                if (sqrDistance < 1)
                {
                    if (!collected)
                    {
                        ship.CollectScrap(this.value);
                        DestroyObject(this.transform.gameObject);
                        collected = !collected;
                    }
                }
            }
        }
    }
}
