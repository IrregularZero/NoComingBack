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

    [SerializeField]
    private GameObject DeathScreen;

    [SerializeField]
    private Image healthChangeEffects;
    [SerializeField]
    private float alphaDecrement = 0.0125f;

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

    public void TakeDamage(float damage)
    {
        if (useUI)
            healthChangeEffects.color = new Color(1, 0, 0, 0.4f);

        health -= damage * damageResist;
        lerpTimer = 0;
    }
    public void RestoreHealth(float healAmount)
    {
        if (useUI)
            healthChangeEffects.color = new Color(0, 1, 0, 0.4f);

        health += healAmount * healingBonus;
        lerpTimer = 0f;
    }

    public void Death(bool isDead)
    {
        DeathScreen.SetActive(isDead);

        if (isDead)
        {
            // Here statistics should be updated
        }
    }
}
