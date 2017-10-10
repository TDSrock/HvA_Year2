using UnityEngine;
using System.Collections;

public class DestroyEffect : MonoBehaviour {

    [SerializeField] private float timeToLive = 5f;
    private float timeAlive = 0f;



    void Update ()
	{
        timeAlive += Time.deltaTime;
        if(timeToLive < timeAlive)
		   Destroy(transform.gameObject);
	
	}
}
