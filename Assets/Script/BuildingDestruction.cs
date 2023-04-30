using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingDestruction : MonoBehaviour
{
    [Header("Building Stats")]
    //add building health

    [Header("Undamaged")]
    [SerializeField] private GameObject _undamaged;
    public bool _isUndamaged;

    [Header("Damaged")]
    [SerializeField] private GameObject _damaged;
    public bool _isDamaged;
    //add smoke particle resulted from damage
    //add optional timer to explode into ruins

    [Header("Ruins")]
    [SerializeField] private GameObject _ruins;
    public bool _isRuined;
    //add explosion particle
    //add explosion damage and damage radius
    //add fire particle resulted from explosion
}
