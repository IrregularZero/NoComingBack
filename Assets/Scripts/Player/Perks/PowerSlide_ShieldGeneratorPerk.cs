using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSlide_ShieldGeneratorPerk : BasePerk
{
    private bool sliding = false;

    [SerializeField]
    private GameObject shieldPrefab;
    private GameObject activeShield;

    private PlayerMotor playerMotor;
    private PlayerUltimateMotor ultimatePlayerMotor;

    private void Start()
    {
        playerMotor = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMotor>();
        ultimatePlayerMotor = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerUltimateMotor>();

        type = "PowerSlide_ShieldGeneratorPerk";
    }
    private void Update()
    {
        if (active && !sliding)
        {
            if (playerMotor.IsSliding || ultimatePlayerMotor.IsSliding)
            {
                activeShield = Instantiate(shieldPrefab, playerMotor.transform);
                sliding = true;
            }
        }

        if (sliding)
        {
            if (!(playerMotor.IsSliding || ultimatePlayerMotor.IsSliding))
            {
                Destroy(activeShield);
                sliding = false;
            }
        }
    }

    public override void EnableEffect()
    {
        active = true;
    }
    public override void DisableEffect()
    {
        active = false;
        if (activeShield is not null)
            Destroy(activeShield);
    }
}
