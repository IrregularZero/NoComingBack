using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingItem : Item
{
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
        backgroundColor = new Color(0.086f, 0.674f, 0.223f, 1f);
        type = "Healing Consumable";
    }
    public override void Use()
    {
        playerVitality = GameObject.FindGameObjectWithTag("Player").GetComponent<VitalitySystem>();
        if (amountOfUses > 0)
        {
            playerVitality.GetComponent<UltimateSystem>().AddUltimateCharge(Mathf.Clamp(playerVitality.Health + healAmount - playerVitality.MaxHealth, 0, playerVitality.GetComponent<UltimateSystem>().MaxEnergy));
            playerVitality.RestoreHealth(healAmount);

            amountOfUses--;
        }
    }
}
