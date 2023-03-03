using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.UI;

public class VitalitySystem : MonoBehaviour
{
    [SerializeField]
    private float health;
    [SerializeField]
    private float maxHealth = 100;
    [SerializeField]
    private float damageResist = 1; // Multiplier to decrease incoming damage
    [SerializeField]
    private float healingBonus = 1; // Multiplier to increase incoming heal
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
        health = Mathf.Clamp(health, 0, maxHealth);

        if (useUI)
            UpdateHealthUI();
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
