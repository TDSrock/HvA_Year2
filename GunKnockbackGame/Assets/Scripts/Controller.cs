using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {
    public Ship myShip;
	public float moveSpeed = 6;
    public KeyCode fireButton = KeyCode.Mouse0;
    Camera viewCamera;
	Vector3 velocity;

	void Start () {
		viewCamera = Camera.main;
        myShip = (Ship)GetComponent(typeof(Ship));
	}

	void Update () {
		Vector3 mousePos = viewCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, viewCamera.transform.position.y));
		transform.LookAt (mousePos + Vector3.up * transform.position.y);
		velocity = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical")).normalized * moveSpeed;
        if (Input.GetKey(fireButton))
        {
            foreach(WeaponBehavior weapon in myShip.weapons)
            {
                weapon.AttemptFire();
            }
        }
	}

    private void FixedUpdate()
    {
        myShip.InputVelocity(velocity);
    }
}