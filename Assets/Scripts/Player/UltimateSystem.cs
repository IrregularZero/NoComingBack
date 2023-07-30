using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class UltimateSystem : MonoBehaviour
{
    [SerializeField]
    private float energy;
    [SerializeField]
    private float maxEnergy = 100;

    [SerializeField]
    private float recievedEnergyBonus = 1f;
    [SerializeField]
    private float passiveEnergyIncerement;

    [SerializeField]
    private float chipSpeed = 2f;
    private float lerpTimer;

    [SerializeField]
    private Image frontUltimateBar;
    [SerializeField]
    private Image backUltimateBar; // Shows change in player's health

    #region Properties
    public float Energy 
    {
        get
        {
            return energy;
        }
        set
        {
            if (value >= 0 && value <= maxEnergy)
            {
                energy = value;
            }
        }
    }
    public float MaxHealth 
    {
        get
        {
            return maxEnergy;
        }
        set
        {
            if (value > 0)
            {
                maxEnergy = value;
            }
        }
    }
    public float RecievedEnergyBonus 
    {
        get
        {
            return recievedEnergyBonus;
        }
        set
        {
            recievedEnergyBonus = value;
        }
    }
    public float PassiveEnergyIncrement 
    {
        get
        {
            return passiveEnergyIncerement;
        }
        set
        {
            passiveEnergyIncerement = value;
        }
    }
    #endregion

    // Start is called before the first frame update
    private void Start()
    {
        energy = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        AddUltimateCharge(passiveEnergyIncerement * Time.deltaTime);

        energy = Mathf.Clamp(energy, 0, maxEnergy);

        UpdateUltimateUI();
    }

    public void UpdateUltimateUI()
    {
        float fillf = frontUltimateBar.fillAmount;
        float fillb = backUltimateBar.fillAmount;

        float hFraction = energy / maxEnergy;

        if (fillb > hFraction)
        {
            frontUltimateBar.fillAmount = hFraction;
            backUltimateBar.color = Color.cyan;

            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;

            backUltimateBar.fillAmount = Mathf.Lerp(fillb, hFraction, percentComplete);
        }
        else if (fillf < hFraction)
        {
            backUltimateBar.fillAmount = hFraction;
            backUltimateBar.color = Color.cyan;

            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = Mathf.Pow(percentComplete, 2);

            frontUltimateBar.fillAmount = Mathf.Lerp(fillf, hFraction, percentComplete);
        }
    }
    public void AddUltimateCharge(float ultimateCharge)
    {
        energy += ultimateCharge * recievedEnergyBonus;
        lerpTimer = 0f;
    }
    public void TakeUltimateCharge(float ultimateCharge)
    {
        energy -= ultimateCharge;
        lerpTimer = 0f;
    }
}
