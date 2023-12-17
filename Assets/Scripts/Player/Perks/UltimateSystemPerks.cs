using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateSystemPerks : BasePerk
{
    [SerializeField]
    private bool passiveEnergyIncrementAffected = false;
    [SerializeField]
    private float additivePassiveEnergyIncrement = 0;
    [SerializeField]
    private float multiplicativePassiveEnergyIncrement = 1;
    private float oldPassiveEnergyIncrement;

    [SerializeField]
    private bool passiveEnergyDecrementAffected = false;
    [SerializeField]
    private float additivePassiveEnergyDecrement = 0;
    [SerializeField]
    private float multiplicativePassiveEnergyDecrement = 1;
    private float oldPassiveEnergyDecrement;

    private UltimateSystem ultimateSystem;

    #region Properties
    public bool Active
    {
        get
        {
            return active;
        }
    }

    public bool PassiveEnergyIncrementAffected
    {
        get
        {
            return passiveEnergyIncrementAffected;
        }
        set
        {
            passiveEnergyIncrementAffected = value;
        }
    }
    public float AdditivePassiveEnergyIncrement
    {
        get
        {
            return additivePassiveEnergyIncrement;
        }
        set
        {
            additivePassiveEnergyIncrement = value;
        }
    }
    public float MultiplicativePassiveEnergyIncrement
    {
        get
        {
            return multiplicativePassiveEnergyIncrement;
        }
        set
        {
            multiplicativePassiveEnergyIncrement = value;
        }
    }

    public bool PassiveEnergyDecrementAffected
    {
        get
        {
            return passiveEnergyDecrementAffected;
        }
        set
        {
            passiveEnergyDecrementAffected = value;
        }
    }
    public float AdditivePassiveEnergyDecrement
    {
        get
        {
            return additivePassiveEnergyDecrement;
        }
        set
        {
            additivePassiveEnergyDecrement = value;
        }
    }
    public float MultiplicativePassiveEnergyDecrement
    {
        get
        {
            return multiplicativePassiveEnergyDecrement;
        }
        set
        {
            multiplicativePassiveEnergyDecrement = value;
        }
    }
    #endregion

    private void Start()
    {
        ultimateSystem = GameObject.FindGameObjectWithTag("Player").GetComponent<UltimateSystem>();
    }

    public override void EnableEffect()
    {
        if (passiveEnergyIncrementAffected)
        {
            oldPassiveEnergyIncrement = ultimateSystem.PassiveEnergyIncrement;
            ultimateSystem.PassiveEnergyIncrement += additivePassiveEnergyIncrement;
            ultimateSystem.PassiveEnergyIncrement *= multiplicativePassiveEnergyIncrement;
        }

        if (passiveEnergyDecrementAffected)
        {
            oldPassiveEnergyDecrement = ultimateSystem.PassiveEnergyDecrement;
            ultimateSystem.PassiveEnergyDecrement += additivePassiveEnergyDecrement;
            ultimateSystem.PassiveEnergyDecrement *= multiplicativePassiveEnergyDecrement;
        }

        active = true;
    }

    public override void DisableEffect()
    {
        if (passiveEnergyIncrementAffected)
            ultimateSystem.PassiveEnergyIncrement = oldPassiveEnergyIncrement;

        if (passiveEnergyDecrementAffected)
            ultimateSystem.PassiveEnergyDecrement = oldPassiveEnergyDecrement;

        active = false;
    }
}
