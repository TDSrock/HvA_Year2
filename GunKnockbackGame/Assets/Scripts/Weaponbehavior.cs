using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weaponbehavior : MonoBehaviour {

    [SerializeField] private static float rechargeTime = 5f;
    [SerializeField] private float timeTillNextAttack = rechargeTime;
    [SerializeField] private bool readyToFire = true;
    [SerializeField][Range (1, int.MaxValue)] private float projectileCount = 1;
    [SerializeField] private bool randomArc = false; //weither or not each pellet gets a random angle within the arc
    [HideInInspector] [SerializeField] private float prevArcAngle;
    [HideInInspector] [SerializeField] private float arcAngle;
    [HideInInspector] [SerializeField] private float halfArcAngle;
    [SerializeField] private GameObject projectile;
    

    public float _arcAngle
    {
        set
        {
            this.arcAngle = value;
            this.halfArcAngle = value / 2;
        }
        get { return this.arcAngle; }
    }

    public void Update()
    {
        if (!readyToFire)
        {
            timeTillNextAttack -= Time.deltaTime;
            readyToFire = timeTillNextAttack < 0;
        }
        
    }

    public void AttemptFire()
    {
        if (readyToFire)
        {
            
            for (int i = 0; i < projectileCount; i++)
            {
                float angle;
                if (randomArc)
                {
                    angle = Random.Range(-halfArcAngle, halfArcAngle);
                }
                else
                {
                    angle = -halfArcAngle;
                }
            }            
        }

    }
}
