using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingItem : Item
{
    [SerializeField]
    private VitalitySystem playerVitality;
    [SerializeField]
    private float healAmount;
    [SerializeField]
    private int maxAmountOfUses = 3;
    private int amountOfUses = 3;

    #region Properties
    public VitalitySystem PlayerVitality 
    {
        get
        {
            return playerVitality;
        }
    }
    public float HealAmount 
    {
        get
        {
            return healAmount;
        }
        set
        {
            healAmount = value;
        }
    }
    public int AmountOfUses 
    {
        get
        {
            return amountOfUses;
        }
        set
        {
            if (value >= 0 && value <= maxAmountOfUses)
            {
                amountOfUses = value;
            }
        }
    }
    #endregion

    private void Start()
    {
        backgroundColor = new Color(22, 172, 57, 1);
        type = "Healing Consumable";
    }
    public override void Use()
    {
        if (amountOfUses > 0)
        {
            playerVitality.RestoreHealth(healAmount);

            amountOfUses--;
        }
    }
}
