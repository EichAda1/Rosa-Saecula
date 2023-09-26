using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Cooldown
{
    [SerializeField] private float _interactCooldownTime;
    private float _nextInteractTime;

    public bool isCoolingDown()
    {
        if( Time.time < _nextInteractTime)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void StartCooldown()
    {
        _nextInteractTime = Time.time + _interactCooldownTime;
    }
}
