using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyBehaviour : MonoBehaviour
{
    private NPCVitality vitality;

    [SerializeField]
    private float maxHealthRestoreDelay = 0.5f;
    private float healthRestoreDelay;

    private float previousHealth;

    // Start is called before the first frame update
    void Start()
    {
        vitality = GetComponent<NPCVitality>();

        previousHealth = vitality.Health;
    }

    // Update is called once per frame
    void Update()
    {
        if (previousHealth != vitality.Health)
        {
            previousHealth = vitality.Health;

            healthRestoreDelay = maxHealthRestoreDelay;
        }

        if (healthRestoreDelay > 0)
        {
            healthRestoreDelay -= Time.deltaTime;

            if (healthRestoreDelay <= 0)
            {
                vitality.RestoreHealth(vitality.MaxHealth - vitality.Health);

                healthRestoreDelay = -1;
            }
        }
    }
}
