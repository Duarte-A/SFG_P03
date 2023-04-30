using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingDestruction : MonoBehaviour
{
    [Header("Building Stats")]
    //add building health

    [Header("Undamaged")]
    [SerializeField] GameObject _undamaged;
    public bool _isUndamaged;

    [Header("Damaged")]
    [SerializeField] GameObject _damaged;
    public bool _isDamaged;
    //add smoke particle resulted from damage
    //add optional timer to explode into ruins

    [Header("Ruins")]
    [SerializeField] GameObject _ruins;
    public bool _isRuined;
    //add explosion particle
    //add explosion damage and damage radius
    //add fire particle resulted from explosion


    private void Update()
    {
        SetUndamaged();
        SetDamaged();
        SetRuined();
    }
    public void SetUndamaged()
    {
        if(_isUndamaged == true)
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

    public void SetDamaged()
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

    public void SetRuined()
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
