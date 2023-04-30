using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Shootable : MonoBehaviour
{
    public UnityEvent<int> Shot;

    public void Shoot(int damageAmount)
    {
        Shot?.Invoke(damageAmount);
    }
}
