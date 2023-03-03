using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.UIElements;

public class VitalitySystem : MonoBehaviour
{
    [SerializeField]
    private float health;
    [SerializeField]
    private float maxHealth = 100;
    [SerializeField]
    private float damageResist = 1; 
    private float healingBonus = 1; 
    [SerializeField]
    private bool useUI;

    private float chipSpeed = 2f;
    private float lerpTimer;

    [SerializeField]
    private Image frontHealthBar;
    [SerializeField]
    private Image backHealthBar; // Shows change in player's health

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        health= Mathf.Clamp(health, 0, maxHealth);
        if (useUI)
            UpdateHealthUI();
    }

    public void UpdateHealthUI()
    {
        throw new NotImplementedException();
    }

    public void TakeDamage(float damage)
    {
        health -= damage * damageResist;
        lerpTimer = 0;
    }
    public void RestoreHealth(float healAmount)
    {
        health += healAmount * healingBonus;
        lerpTimer = 0f;
    }
}
