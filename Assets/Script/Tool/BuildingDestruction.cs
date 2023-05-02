using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BuildingDestruction : MonoBehaviour
{
    [Header("Building Health")]
    public int _buildingHealth;


    [Header("Undamaged State")]
    [SerializeField] GameObject _undamaged;

    //Leave checked if you want the building to look undamaged,
    //uncheck it and lower the health below 3 if you want it to look damaged in any way
    public bool _isUndamaged;

    [Header("Damaged State")]
    [SerializeField] GameObject _damaged;
    private bool _isDamaged;
    public GameObject _smoke;

    //optional timer
    public bool _canExplodeOnTimer;
    [SerializeField] public float timeRemaining;




    [Header("Ruins State")]
    [SerializeField] GameObject _ruins;
    private bool _isRuined;
    public GameObject _fireFX;

    //Explosion effect
    public GameObject _explosionFX;
    //Optional explosion damage and damage area size
    public bool _canExplodeAndDamage = false;
    private SphereCollider _explosionSphere;
    //BuildingToDamage
    private BuildingDestruction buildingObject;

    //FixedUpdate Variable
    private float fixedUpdateCount = 0;

    private void Start()
    {
        _explosionSphere = GetComponent<SphereCollider>();
        _explosionSphere.enabled = false;
    }

    private void Update()
    {
        //For testing, this can manually change the health value by 1 int on all objects with
        //this script attached to it
        if (Input.GetKeyDown(KeyCode.A))
        {
            TakeDamage(1);
        }
    }


    private void FixedUpdate()
    {
        fixedUpdateCount += 1;

        HealthCheck();
    }

    //Health Check Function Below

    public void HealthCheck()
    {
        if (_buildingHealth >= 3 && _isUndamaged)
        {

            Debug.Log("Undamaged");
            _undamaged.SetActive(true);
            _damaged.SetActive(false);
            _ruins.SetActive(false);

            bool damagedBool = _isDamaged == false;
            bool ruinsBool = _isRuined == false;
        }
        else
        {
            _undamaged.SetActive(false);
        if (_buildingHealth >= 1)
        {
            _isUndamaged = false;
            _isDamaged = true;
            _isRuined = false;
            Debug.Log("Damaged");
            if (_isDamaged == true)
            {
                bool undamagedBool = _isDamaged == true;
                bool ruinsBool = _isRuined == false;
                _undamaged.SetActive(false);
                _damaged.SetActive(true);
                _ruins.SetActive(false);
                    if (_canExplodeOnTimer)
                    {
                        DamagedExplosionTimer();
                    }
            }
            else
            {
                _damaged.SetActive(false);
            }
        }

        if (_buildingHealth <=0)
        {
                if (_canExplodeAndDamage)
                {
                    //ExplosionDamage();
                    _canExplodeAndDamage = true;
                    StartCoroutine(BuildingExplosion());
                }
            _isUndamaged = false;
            _isDamaged = false;
            _isRuined = true;
            Debug.Log("Ruined");
            if (_isRuined == true)
            {
                bool undamagedBool = _isDamaged == false;
                bool damagedBool = _isRuined == true;
                _undamaged.SetActive(false);
                _damaged.SetActive(false);
                _ruins.SetActive(true);
            }
            else
            {
                _ruins.SetActive(false);
                    Debug.Log("Else Ruined");
            }
        }
        }

    }

    //Reduces Building Health

    void TakeDamage(int damage)
    {
        _buildingHealth -= damage;
    }

    //Function for changing building to 'Ruined' building mesh
    public void DamagedExplosionTimer()
    {
        if (_canExplodeOnTimer)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                _buildingHealth = 0;
                timeRemaining = 0;
                _canExplodeOnTimer = false;
            }
        }
    }

    //Coroutine for enabling the explosion damage radius for 2 seconds
    IEnumerator BuildingExplosion()
    {
        _explosionSphere.enabled = true;
        yield return new WaitForSeconds(2f);
        _explosionSphere.enabled = false;
        _canExplodeAndDamage = false;
        Debug.Log("BuildingExplosion");
    }


    //Explosion sphere collider detects if the object it overlaps has
    //'BuildingDestruction' script, like this building object
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out BuildingDestruction _building))
        {
            _building.TakeDamage(1);
        Debug.Log("TriggerDamage");
        }
    }
}

