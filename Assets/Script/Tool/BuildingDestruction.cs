using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BuildingDestruction : MonoBehaviour
{
    [Header("Building Health")]
    public int _buildingHealth;


    [Header("Undamaged State")]
    [SerializeField] GameObject _undamaged;
    public bool _isUndamaged;

    [Header("Damaged State")]
    [SerializeField] GameObject _damaged;
    private bool _isDamaged;
    public GameObject _smoke;

    //optional timer
    public bool _canExplodeOnTimer;


    [Header("Ruins State")]
    [SerializeField] GameObject _ruins;
    private bool _isRuined;
    public GameObject _explosion;
    //add explosion damage and damage radius
    public GameObject _fire;

    private float fixedUpdateCount = 0;


    private void Update()
    {
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

    //Single Bool Check Function Below

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
            }
            else
            {
                _damaged.SetActive(false);
            }
        }

        if (_buildingHealth <=0)
        {
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

    //Health

    void TakeDamage(int damage)
    {
        _buildingHealth -= damage;
    }

}
