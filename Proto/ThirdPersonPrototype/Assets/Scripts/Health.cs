using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _CurrentHealth = 1;                // current/initial health
    private int _MaxHealth = 10;                                    // max health

    [SerializeField] private List<GameObject> _HealthPots;               // health potions
    private int currHealthPots;                                      // current amount of health potions

    //Make current health percentage visible
    public float HealthPercentage => (float)_CurrentHealth / _MaxHealth;

    public void UpdatePots(bool obtained)
    {
        if(obtained && currHealthPots < 3)
            currHealthPots++;
        
        if(!obtained)
            currHealthPots--;

        UpdateVisualPots();
    }

    private void UpdateVisualPots()
    {
        for(int i = 0; i < _HealthPots.Count; i++){
            if(i < currHealthPots)
                _HealthPots[i].SetActive(true);
            else
                _HealthPots[i].SetActive(false);
        }
    }

    //Deal damage to Health
    public void Damage(int amount)
    {
        //Deal damage
        _CurrentHealth -= amount;

        //Check if still alive
        if (_CurrentHealth <= 0)
            Death();
    }

    private void Death()
    {
        throw new NotImplementedException();
    }
}
