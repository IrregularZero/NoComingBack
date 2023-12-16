using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSystemPerks : BasePerk
{
    private bool active = false;

    [SerializeField]
    private bool meleeDamageAffected = false;
    [SerializeField]
    private float additiveMeleeDamage = 0;
    [SerializeField]
    private float multiplicativeMeleeDamage = 1;
    private float oldMeleeDamage;

    [SerializeField]
    private bool finisherHealAffected = false;
    [SerializeField]
    private float additiveFinisherHeal = 0;
    [SerializeField]
    private float multiplicativeFinisherHeal = 1;
    private float oldFinisherHeal;

    private Melee embededMelee;

    #region Properties
    public bool Active
    {
        get
        {
            return active;
        }
    }

    public bool MeleeDamageAffected
    {
        get
        {
            return meleeDamageAffected;
        }
        set
        {
            meleeDamageAffected = value;
        }
    }
    public float AdditiveMeleeDamage
    {
        get
        {
            return additiveMeleeDamage;
        }
        set
        {
            additiveMeleeDamage = value;
        }
    }
    public float MultiplicativeMeleeDamage
    {
        get
        {
            return multiplicativeMeleeDamage;
        }
        set
        {
            multiplicativeMeleeDamage = value;
        }
    }

    public bool FinisherHealAffected
    {
        get
        {
            return finisherHealAffected;
        }
        set
        {
            finisherHealAffected = value;
        }
    }
    public float AdditiveFinisherHeal
    {
        get
        {
            return additiveFinisherHeal;
        }
        set
        {
            additiveFinisherHeal = value;
        }
    }
    public float MultiplicativeFinisherHeal
    {
        get
        {
            return multiplicativeFinisherHeal;
        }
        set
        {
            multiplicativeFinisherHeal = value;
        }
    }
    #endregion

    private void Start()
    {
        embededMelee = GameObject.FindGameObjectWithTag("EmbededMelee").GetComponent<Melee>();
    }

    public override void EnableEffect()
    {
        if (meleeDamageAffected)
        {
            oldMeleeDamage = embededMelee.DamageModificator;
            embededMelee.DamageModificator += additiveMeleeDamage;
            embededMelee.DamageModificator *= multiplicativeMeleeDamage;
        }

        if (finisherHealAffected)
        {
            oldFinisherHeal = embededMelee.FinisherHPBonus;
            embededMelee.FinisherHPBonus += additiveFinisherHeal;
            embededMelee.FinisherHPBonus *= multiplicativeFinisherHeal;
        }

        active = true;
    }

    public override void DisableEffect()
    {
        if (meleeDamageAffected)
            embededMelee.DamageModificator = oldMeleeDamage;

        if (finisherHealAffected)
            embededMelee.FinisherHPBonus = oldFinisherHeal;

        active = false;
    }
}
