using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponBehavior : MonoBehaviour {

    //normal shooting vars
    [SerializeField] private static float rechargeTime = 5f;
    [SerializeField] private float timeTillNextAttack = rechargeTime;
    [SerializeField] private bool readyToFire = true;
    [SerializeField] [Range(1, 256)] private float projectileCount = 1;
    [SerializeField] private bool randomArc = false; //weither or not each pellet gets a random angle within the arc
    /*[HideInInspector]*/[SerializeField] public float arcAngle;
    /*[HideInInspector]*/[SerializeField] private float halfArcAngle;
    [SerializeField] private GameObject projectile;

    //Ammo and reload related vars
    [SerializeField] public static double maxAmmo = 30;
    [SerializeField] public double currentAmmo = maxAmmo;
    [SerializeField] public float reloadTime = 10f;
    [SerializeField] public float reloadTimer;//time left on current reload
    [SerializeField] public bool reloading = false;
    [SerializeField] public bool bottomlessClip = false;
    [SerializeField] private float ammoCost = 1;//per projectile
    [SerializeField] private bool autoReload = true;

    [SerializeField] public float _arcAngle
    {
        set
        {
            this.arcAngle = value;
            this.halfArcAngle = value / 2;
        }
        get { return this.arcAngle; }
    }

    [SerializeField] public bool _enoughAmmo
    {
        get { return this.currentAmmo < 0; }
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
        if (readyToFire && _enoughAmmo)
        {
            readyToFire = false;
            timeTillNextAttack = rechargeTime;
            var projectilesToFire = projectileCount * ammoCost / currentAmmo;
            for (int i = 0; i < projectilesToFire; i++)
            {
                float angle;
                if (randomArc)
                {
                    angle = Random.Range(-halfArcAngle, halfArcAngle);
                }
                else
                {
                    //TODO: Ensure the below angle works properly(this solution works for multiples of 2
                    angle = -halfArcAngle + ((float)i / projectileCount * arcAngle);
                }
                GameObject fired = Instantiate(projectile, this.transform);
                fired.transform.Rotate(DirFromAngle(angle, false));
                if(!bottomlessClip)
                {
                    currentAmmo -= ammoCost;
                }
            }
            if(currentAmmo < ammoCost)
            {
                if(autoReload)
                {
                    this.StartReload();
                }
            }
        }

    }

    public void StartReload()
    {
        reloading = true;
        reloadTimer = reloadTime;
        this.Reload();
    }

    public void Reload()
    {

    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if(!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
