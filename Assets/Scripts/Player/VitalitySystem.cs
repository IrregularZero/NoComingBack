using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class VitalitySystem : MonoBehaviour
{
    [SerializeField]
    protected float health;
    [SerializeField]
    protected float maxHealth = 100;

    [SerializeField]
    protected float damageResist = 1; // Multiplier to decrease incoming damage
    [SerializeField]
    protected float healingBonus = 1; // Multiplier to increase incoming heal
    [SerializeField]
    protected bool useUI;

    protected float chipSpeed = 2f;
    protected float lerpTimer;

    [SerializeField]
    protected Image frontHealthBar;
    [SerializeField]
    protected Image backHealthBar; // Shows change in player's health

    [SerializeField]
    protected GameObject DeathScreen;

    [SerializeField]
    protected Image healthChangeEffects;
    [SerializeField]
    protected float alphaDecrement = 0.45f;

    #region Properties
    public float Health 
    {
        get
        {
            return health;
        }
        set
        {
            if (value >= 0 && value <= maxHealth)
            {
                health = value;
            }
        }
    }
    public float MaxHealth 
    {
        get
        {
            return maxHealth;
        }
        set
        {
            if (value > 0)
            {
                maxHealth = value;
            }
        }
    }
    public float DamageResist 
    {
        get
        {
            return damageResist;
        }
        set
        {
            if (value > 0 && value <= 1)
            {
                damageResist = value;
            }
        }
    }
    public float HealingBonus 
    {
        get
        {
            return healingBonus;
        }
        set
        {
            healingBonus = value;
        }
    }
    public bool UseUI 
    {
        get
        {
            return useUI;
        }
        set
        {
            useUI = value;
        }
    }
    #endregion

    // Start is called before the first frame update
    protected virtual void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);

        UpdateHealthUI();

        Death(health <= 0);

        if (healthChangeEffects.color.a - alphaDecrement * Time.deltaTime >= 0)
            healthChangeEffects.color = new Color(healthChangeEffects.color.r, healthChangeEffects.color.g, healthChangeEffects.color.b, healthChangeEffects.color.a - alphaDecrement * Time.deltaTime);
    }

    public void UpdateHealthUI()
    {
        float fillf = frontHealthBar.fillAmount;
        float fillb = backHealthBar.fillAmount;

        float hFraction = health / maxHealth;

        if (fillb > hFraction) // if yes, meaning player got damaged
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.red;

            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;

            backHealthBar.fillAmount = Mathf.Lerp(fillb, hFraction, percentComplete);
        }
        else if (fillf < hFraction) // if true, player got healed
        {
            backHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.green;

            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = Mathf.Pow(percentComplete, 2);

            frontHealthBar.fillAmount = Mathf.Lerp(fillf, hFraction, percentComplete);
        }
    }

    public virtual void TakeDamage(float damage)
    {
        if (useUI)
            healthChangeEffects.color = new Color(1, 0, 0, 0.4f);

        health -= damage * damageResist;
        lerpTimer = 0;
    }
    public virtual void RestoreHealth(float healAmount)
    {
        if (useUI)
            healthChangeEffects.color = new Color(0, 1, 0, 0.4f);

        health += healAmount * healingBonus;
        lerpTimer = 0f;
    }

    public virtual void Death(bool isDead)
    {
        DeathScreen.SetActive(isDead);

        if (isDead)
        {
            // Here statistics should be updated
        }
    }
}
