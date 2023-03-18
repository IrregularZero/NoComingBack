using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingCard : EventItemSystem
{
    [SerializeField]
    private GameObject particleSystemOnplatform;
    [SerializeField]
    private VitalitySystem playerVitalitySystem;

    [SerializeField]
    float MaxDamageDelay = 0.5f;
    float damageDelay = 0f;

    private void Update()
    {
        if (eventActive)
        {
            particleSystemOnplatform.SetActive(true);

            if (damageDelay <= 0f)
            {
                playerVitalitySystem.TakeDamage(5f);
                damageDelay = MaxDamageDelay;
            }

            damageDelay -= Time.deltaTime;
        }
        else
        {
            particleSystemOnplatform.SetActive(false);
        }
    }

}
