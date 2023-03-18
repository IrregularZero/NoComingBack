using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingCard : EventItemSystem
{
    [SerializeField]
    private GameObject particleSystemOnplatform;
    [SerializeField]
    private VitalitySystem playerVitalitySystem;

    [SerializeField]
    float MaxHealDelay = 0.5f;
    float healDelay = 0f;

    private void Update()
    {
        if (eventActive)
        {
            particleSystemOnplatform.SetActive(true);

            if (healDelay <= 0f)
            {
                playerVitalitySystem.RestoreHealth(5f);
                healDelay = MaxHealDelay;
            }

            healDelay -= Time.deltaTime;
        }
        else
        {
            particleSystemOnplatform.SetActive(false);
        }
    }

}
