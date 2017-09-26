using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponBehavior : MonoBehaviour {
    [SerializeField] private Ship owner;//place the ship instance here that "owns" the weapon
    [SerializeField] public List<BulletBehavior> activeProjectiles = new List<BulletBehavior>();

    //normal shooting vars
    [SerializeField] private float rechargeTime = 2f;
    [SerializeField] private float timeTillNextAttack = 0;
    [SerializeField] private bool readyToFire = true;
    [SerializeField] private bool randomArc = false; //weither or not each projectile gets a random angle within the arc
    /*[HideInInspector]*/[SerializeField] [Range(0, 360)] public float arcAngle;
    /*[HideInInspector]*/[SerializeField] private float halfArcAngle;
    [SerializeField] [Range(1, 256)] private float projectileCount = 1;
    [SerializeField] private float projectileSpeed = 3;
    [SerializeField] private BulletBehavior projectile;
    [SerializeField] private LayerMask projectileTargetMask;

    //Ammo and reload related vars
    [SerializeField] public static double maxAmmo = 30;
    [SerializeField] public double currentAmmo = maxAmmo;
    [SerializeField] public float reloadTime = 10f;
    [SerializeField] public float reloadTimer;//time left on current reload
    [SerializeField] public bool reloading = false;
    [SerializeField] public bool bottomlessClip = false;
    [SerializeField] private float ammoCost = 1;//per projectile
    [SerializeField] private bool autoReload = true;

    [SerializeField] private float recoil = 3f;

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
        get { return this.currentAmmo > ammoCost; }
    }

    public void Start()
    {
        owner = (Ship)GetComponentInParent(typeof(Ship));
    }

    public void Update()
    {
        _arcAngle = this.arcAngle;//TODO: Remove this from here once done with testing
        if (!readyToFire)
        {
            timeTillNextAttack -= Time.deltaTime;
            readyToFire = timeTillNextAttack < 0;
        }
        if (reloading)
        {
            this.Reload();
        }
        
    }

    public void AttemptFire()
    {
        if (readyToFire && _enoughAmmo)
        {
            readyToFire = false;
            timeTillNextAttack = rechargeTime;
            var projectilesToFire =  currentAmmo / ammoCost;
            projectilesToFire = projectileCount;
            Debug.Log(projectilesToFire);
            //projectilesToFire = (projectilesToFire < projectileCount) ? projectilesToFire : projectileCount;//make sure we don't create too many
            Debug.Log(projectilesToFire);
            for (int i = 0; i < projectilesToFire; i++)
            {
                float angle = 0;
                if (randomArc)
                {
                    angle = Random.Range(-arcAngle, arcAngle);
                }
                else
                {
                    //TODO: Ensure the below angle works properly(this solution works for multiples of 2
                    angle = -arcAngle + ((float)i / projectileCount * arcAngle);
                }
                BulletBehavior fired = Instantiate(projectile);
                fired.transform.position = this.transform.position;
                var bulletForward = (this.transform.forward + DirFromAngle(angle, false)).normalized;
                bulletForward.y = 0;
                fired.transform.forward = bulletForward;
                fired.velocity = projectileSpeed;
                fired.creator = this;
                fired.targetMask = projectileTargetMask;
                owner.velocity += -bulletForward * recoil;
                this.activeProjectiles.Add(fired);
                
                if(!bottomlessClip)//bottomless clip never loses ammo
                {
                    currentAmmo -= ammoCost;
                }
            }
            if(!_enoughAmmo)
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
        if (!reloading)
        {
            reloading = true;
            reloadTimer = reloadTime;
            this.Reload();
        }
    }

    public void Reload()
    {
        reloadTimer -= Time.deltaTime;
        if (reloadTimer <= 0)
        {
            reloading = false;
            currentAmmo = maxAmmo;
        }

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
