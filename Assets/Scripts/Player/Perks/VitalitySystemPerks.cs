using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VitalitySystemPerks : BasePerk
{
    [SerializeField]
    private bool damageMultiplierAffected = false;
    [SerializeField]
    private float additiveBonusDamageMultiplier = 0;
    [SerializeField]
    private float multiplicativeBonusDamageMultiplier = 1;
    private float oldDamageMultiplier;

    [SerializeField]
    private bool healingBonusAffected = false;
    [SerializeField]
    private float additiveHealingBonus = 0;
    [SerializeField]
    private float multiplicativeHealingBonus = 1;
    private float oldHealingBonus;

    private VitalitySystem vitalitySystem;

    #region Properties
    public bool Active 
    {
        get
        {
            return active;
        }
    }

    public bool DamageMultiplierAffected 
    {
        get
        {
            return damageMultiplierAffected;
        }
        set
        {
            damageMultiplierAffected = value;
        }
    }
    public float AdditiveBonusDamageMultiplier 
    {
        get
        {
            return additiveBonusDamageMultiplier;
        }
        set
        {
            additiveBonusDamageMultiplier = value;
        }
    }
    public float MultiplicativeBonusDamageMultiplier 
    {
        get
        {
            return multiplicativeBonusDamageMultiplier;
        }
        set
        {
            multiplicativeBonusDamageMultiplier = value;
        }
    }

    public bool HealingBonusAffected 
    {
        get
        {
            return healingBonusAffected;
        }
        set
        {
            healingBonusAffected = value;
        }
    }
    public float AdditiveHealingBonus 
    {
        get
        {
            return additiveHealingBonus;
        }
        set
        {
            additiveHealingBonus = value;
        }
    }
    public float MultiplicativeHealingBonus 
    {
        get
        {
            return multiplicativeHealingBonus;
        }
        set
        {
            multiplicativeHealingBonus = value;
        }
    }
    #endregion

    private void Start()
    {
        vitalitySystem = GameObject.FindGameObjectWithTag("Player").GetComponent<VitalitySystem>();
        type = "VitalitySystemPerk";
    }

    public override void EnableEffect()
    {
        // Enabling effect for DamageMultiplier (DM)
        if (damageMultiplierAffected)
        {
            oldDamageMultiplier = vitalitySystem.DamageMultiplier; // Save old DM
            vitalitySystem.DamageMultiplier += additiveBonusDamageMultiplier; // Apply additive bonus
            vitalitySystem.DamageMultiplier *= multiplicativeBonusDamageMultiplier; // In the end apply multiplicative bonus
        }

        // Enabling effect for DamageMultiplier (DM)
        if (healingBonusAffected)
        {
            oldHealingBonus = vitalitySystem.HealingBonus;
            vitalitySystem.HealingBonus += additiveHealingBonus;
            vitalitySystem.HealingBonus *= multiplicativeHealingBonus;
        }

        active = true;
    }

    public override void DisableEffect()
    {
        if (damageMultiplierAffected)
            vitalitySystem.DamageMultiplier = oldDamageMultiplier;

        if (healingBonusAffected)
            vitalitySystem.HealingBonus = oldHealingBonus;

        active = false;
    }
}
