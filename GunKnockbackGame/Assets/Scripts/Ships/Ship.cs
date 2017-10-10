using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ship : MonoBehaviour {
    public enum ShipType { Player, mob, miniBoss, Boss }

    public ShipType shipType;
    public List<WeaponBehavior> weapons;
    public Vector3 velocity;
    [Range(0, 1)] public float drag;
    public float thrusterPower = 0;
    public float componentHealth = 100;
    public float fullHealth;
    public float currentHealth;

    [Header("Econmy related vars")]
    [SerializeField] private float resources = 0;
    public GameObject resourcesPrefab;
    

    [Header("UI related stuff(do not edit)")]
    public Image healthBar;
    private float dispHP;
    private float goalDispHP;
    public Text resourcesText;
    public GameObject weaponInfoPrefab;
    public Canvas MainUICanvas;

    public float _goalDispHP
    {
        get
        {
            return goalDispHP;
        }

        set
        {
            goalDispHP = value;
        }
    }

    public float _dispHP
    {
        get
        {
            return dispHP;
        }

        set
        {
            dispHP = value;
            healthBar.fillAmount = dispHP;
        }
    }

    public float _currentHealth
    {
        get
        {
            return currentHealth;
        }

        set
        {
            currentHealth = value;
            _goalDispHP = currentHealth / fullHealth;
        }
    }

    public float _resources
    {
        get { return this.resources; }
        set
        {
            this.resources = value;
            if (this.shipType == ShipType.Player)
            {
                resourcesText.text = "Scrap: " + this.resources;
            }
        }
    }

    // Use this for initialization
    void Start () {
        var weaponsComponents = GetComponentsInChildren(typeof(WeaponBehavior));
        WeaponBehavior[] w = new WeaponBehavior[weaponsComponents.Length];
        for(int i = 0;i < weaponsComponents.Length; i++)
        {
            w[i] = (WeaponBehavior)weaponsComponents[i];
            if(shipType == ShipType.Player)
            {
                //make the player's ship have ui elements
                var prefab = Instantiate(weaponInfoPrefab, MainUICanvas.transform);
                var prefabRect = prefab.GetComponent<RectTransform>().rect;
                prefab.transform.position += new Vector3(0, -i * prefabRect.height, 0);

                w[i].TieToUI(prefab);

            }
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

        this._resources = resources;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if(_dispHP != _goalDispHP)
        {
            if (Mathf.Abs(_dispHP - _goalDispHP) < 1)
            {
                _dispHP = Mathf.Lerp(_dispHP, _goalDispHP, 10f * Time.fixedDeltaTime);
            }
            else
            {
                _dispHP = _goalDispHP;
            }
        }
        if(_currentHealth <= 0)
        {
            DestroySelf();
        }
        this.transform.position += velocity * Time.deltaTime;
        if (velocity.sqrMagnitude != 0)
        {
            velocity *= drag;
            
        }

	}

    private void DestroySelf()
    {
        for(float i = this.resources; i > 0; i -= 10)
        {
            var scrap = Instantiate(resourcesPrefab, this.transform.position, Quaternion.identity);
            var direction = new Vector3(Random.Range(-20, 20), 0, Random.Range(-20, 20));
            scrap.GetComponent<Rigidbody>().AddForce(direction * 25);
        }
        DestroyObject(this.transform.parent.gameObject);
    }

    public void CollectScrap(float value)
    {
        this._resources += value;

    }

    public void InputVelocity(Vector3 input)
    {
        this.velocity += input * thrusterPower;
    }
}
