using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class BuildingDestruction : MonoBehaviour
{
    [Header("Building State")]
    //add building health (use ints)
    //[SerializeField] private BuildingType _buildingType = BuildingType.None;
    public int _buildingHealth;


    [Header("Undamaged")]
    [SerializeField] GameObject _undamaged;
    public bool _isUndamaged;

    [Header("Damaged")]
    [SerializeField] GameObject _damaged;
    public bool _isDamaged;
    public GameObject _smoke;
    public bool _canExplodeOnTimer;
    //add optional timer to explode into ruins

    [Header("Ruins")]
    [SerializeField] GameObject _ruins;
    public bool _isRuined = false;
    public GameObject _explosion;
    //add explosion damage and damage radius
    public GameObject _fire;

    private float fixedUpdateCount = 0;

    public enum BuildingType
    {
        None = 0,
        Undamaged,
        Damaged,
        Ruined
    }

    private void Update()
    {
        /*
            //set building states according to health

            SetUndamagedCheck();
            SetDamagedCheck();
            SetRuinedCheck();
        */

        if (Input.GetKeyDown(KeyCode.A))
        {
            TakeDamage(1);
        }

    }


    private void FixedUpdate()
    {
        fixedUpdateCount += 1;
        /*
                if (_isUndamaged && !_isDamaged && !_isRuined)
                {
                    _isUndamaged = true;
                    _isDamaged = false;
                    _isRuined = false;
                    SetUndamagedCheck();
                    Debug.Log("Undamaged");
                }

                if (_isDamaged && !_isUndamaged && !_isRuined)
                {
                    _isUndamaged = false;
                    _isDamaged = true;
                    _isRuined = false;
                    SetDamagedCheck();
                    Debug.Log("Damaged");
                }

                if (_isRuined && !_isUndamaged && _isRuined)
                {
                    _isUndamaged = false;
                    _isDamaged = false;
                    _isRuined = true;
                    SetRuinedCheck();
                    Debug.Log("Ruins");
                }
        */

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
        if (_buildingHealth <= 2 ||  _buildingHealth >= 1)
        {
            _isUndamaged = false;
            _isDamaged = true;
            _isRuined = false;
            Debug.Log("Damaged");
            if (_isDamaged == true)
            {
                bool undamagedBool = _isDamaged == false;
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

        if (_buildingHealth <= 0)
        {
            _isUndamaged = false;
            _isDamaged = false;
            _isRuined = true;
            Debug.Log("Ruined");
            if (_isRuined == true)
            {
                bool undamagedBool = _isDamaged == false;
                bool damagedBool = _isRuined == false;
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

    //Separate Bool functions below

    public void SetUndamagedCheck()
    {
        if (_buildingHealth >= 3)
        {
            if (_isUndamaged == true)
            {
                bool damagedBool = _isDamaged == false;
                bool ruinsBool = _isRuined == false;
                _undamaged.SetActive(true);
                _damaged.SetActive(false);
                _ruins.SetActive(false);
            }
            else
            {
                _undamaged.SetActive(false);
            }
        }
    }

    public void SetDamagedCheck()
    {
        if (_buildingHealth >= 1 || _buildingHealth <= 2)
        {
            if (_isDamaged == true)
            {
                bool undamagedBool = _isDamaged == false;
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
    }

    public void SetRuinedCheck()
    {
        if (_buildingHealth <= 0)
        {
            if (_isRuined == true)
            {
                bool undamagedBool = _isDamaged == false;
                bool damagedBool = _isRuined == false;
                _undamaged.SetActive(false);
                _damaged.SetActive(false);
                _ruins.SetActive(true);
            }
            else
            {
                _ruins.SetActive(false);
            }
        }
    }

    //Health

    void TakeDamage(int damage)
    {
        _buildingHealth -= damage;
    }

    /*
    //Enum functions below

    public void UndamagedType()
    {
        if(_buildingType == BuildingType.Undamaged)
        {
            _undamaged.SetActive(true);
            _damaged.SetActive(false);
            _ruins.SetActive(false);
        }
        else
        {
            _undamaged.SetActive(false);
        }
    }

    public void DamagedType()
    {
        if (_buildingType == BuildingType.Damaged)
        {
            _undamaged.SetActive(false);
            _damaged.SetActive(true);
            _ruins.SetActive(false);
        }
        else
        {
            _damaged.SetActive(false);
        }
    }

    public void RuinedType()
    {
        if (_buildingType == BuildingType.Ruined)
        {
            _undamaged.SetActive(false);
            _damaged.SetActive(false);
            _ruins.SetActive(true);
        }
        else
        {
            _ruins.SetActive(false);
        }
    }
    */
}
